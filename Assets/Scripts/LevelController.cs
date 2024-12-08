using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Target _target;
    [SerializeField] private Pool<Bullet> _poolBullets;
    [SerializeField] private Pool<ExplotionEffect> _poolExplotions;
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private float _minDistanceToTarget = 5f;
    [SerializeField] private float _minScalePlayer = 0.15f;
    [SerializeField] private float _delayToShowCompleteLevel = 1f;
    [SerializeField] private LevelUI _ui;

    private readonly int _obstacleLayerMask = 1 << 7;
    private bool _isStartedMoveToTarget = false;
    private bool _isLevelEnded = false;

    private void Start()
    {
        _ui.onClickRestart += OnClickRestart;

        _player.Init(_poolBullets.Get);
        _player.onScale += OnScalePlayer;

        GameManager.InputHandler.onScreenPointerDown += InputHandler_onScreenPointerDown;
        GameManager.InputHandler.onScreenPointerUp += InputHandler_onScreenPointerUp;

        foreach (var obstacle in GetObstacles())
        {
            obstacle.onDestroyed += OnDestroyedObstacle;
        }
    }

    private void OnClickRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void OnScalePlayer()
    {
        if(_player.Scale <= _minScalePlayer)
        {
            _isLevelEnded = true;
            _player.EndBulletPreparation();
            _ui.Show(LevelEndStatus.GameOver);
        }
    }

    private void OnDestroyedObstacle(Obstacle obj)
    {
        var explotion = _poolExplotions.Get();
        explotion.transform.position = obj.transform.position;

        if (_isLevelEnded == false &&
            _isStartedMoveToTarget == false &&
            IsExistObstaclesToTarget() == false)
        {
            _isStartedMoveToTarget = true;

            new PlayerJumpToTargetDriver(
                this.StartCoroutine,
                _player,
                _target.transform,
                _minDistanceToTarget,
                OnPlayerNearTarget)
            .Run();
        }
    }

    private void OnPlayerNearTarget()
    {
        _target.OpenDoor();
        _isLevelEnded = true;

        this.DoAction(_delayToShowCompleteLevel, () =>
        {
            _ui.Show(LevelEndStatus.LevelCompleted);
        });
    }

    private bool IsExistObstaclesToTarget()
    {
        return PhysicUtility.Check(_player.transform.position, _target.transform.position, _player.Scale, _obstacleLayerMask, out RaycastHit[] hits);
    }

    public IReadOnlyList<Obstacle> GetObstacles()
    {
        return _obstacles;
    }

    private void InputHandler_onScreenPointerDown()
    {
        if (_isLevelEnded)
            return;

        _player.StartBulletPreparation();
    }

    private void InputHandler_onScreenPointerUp()
    {
        if (_isLevelEnded)
            return;

        _player.Fire();
    }
}