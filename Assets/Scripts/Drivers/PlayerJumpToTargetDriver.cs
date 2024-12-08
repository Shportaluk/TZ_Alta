using System;
using System.Collections;
using UnityEngine;

public class PlayerJumpToTargetDriver
{
    private readonly Func<IEnumerator, Coroutine> _funcCreateCor;
    private readonly Player _player;
    private readonly Transform _target;
    private readonly float _minDistance;
    private readonly Action _onCompleted;

    public PlayerJumpToTargetDriver(Func<IEnumerator, Coroutine> funcCreateCor, Player player, Transform target, float minDistance, Action onCompleted)
    {
        _funcCreateCor = funcCreateCor;
        _player = player;
        _target = target;
        _minDistance = minDistance;
        _onCompleted = onCompleted;
    }


    public void Run()
    {
        _funcCreateCor(Cor());

        IEnumerator Cor()
        {
            while (true)
            {
                _player.Jump();

                yield return new WaitForSeconds(0.5f);

                yield return new WaitUntil(() => _player.IsGrounded);

                var distance = Vector3.Distance(_player.transform.position, _target.position);
                if(distance < _minDistance)
                {
                    break;
                }
            }

            _onCompleted?.Invoke();
        }
    }
}