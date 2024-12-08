using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _target;
    [SerializeField] private Pool<Bullet> _poolBullets;
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private float _minDistanceToTarget = 5f;

    private readonly int _obstacleLayerMask = 1 << 7;
    private bool _isStartedMoveToTarget = false;

    private void Start()
    {
        _player.Init(_poolBullets.Get);

        GameManager.InputHandler.onScreenPointerDown += InputHandler_onScreenPointerDown;
        GameManager.InputHandler.onScreenPointerUp += InputHandler_onScreenPointerUp;

        foreach (var obstacle in GetObstacles())
        {
            obstacle.onDestroyed += OnDestroyedObstacle;
        }
    }

    private void OnDestroyedObstacle(Obstacle obj)
    {
        if (_isStartedMoveToTarget == false &&
            IsExistObstaclesToTarget() == false)
        {
            _isStartedMoveToTarget = true;

            new PlayerJumpToTargetDriver(
                this.StartCoroutine,
                _player,
                _target,
                _minDistanceToTarget,
                OnPlayerNearTarget)
            .Run();
        }
    }

    private void OnPlayerNearTarget()
    {

    }

    private bool IsExistObstaclesToTarget()
    {
        return PhysicUtility.Check(_player.transform.position, _target.position, _player.Scale, _obstacleLayerMask, out RaycastHit[] hits);
    }

    public IReadOnlyList<Obstacle> GetObstacles()
    {
        return _obstacles;
    }

    private void InputHandler_onScreenPointerDown()
    {
        _player.StartBulletPreparation();
    }

    private void InputHandler_onScreenPointerUp()
    {
        _player.Fire();
    }
}