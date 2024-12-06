using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Pool<Bullet> _poolBullets;
    [SerializeField] private List<Obstacle> _obstacles;


    private void Start()
    {
        _player.Init(_poolBullets.Get);

        GameManager.InputHandler.onScreenPointerDown += InputHandler_onScreenPointerDown;
        GameManager.InputHandler.onScreenPointerUp += InputHandler_onScreenPointerUp;
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
