using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Func<Bullet> _funcGetBullet;

    [SerializeField] private float _bulletSizeStep = 0.01f;
    [SerializeField] private float _bulletSizeMultiplier = 10;
    private float _bulletSize = 0f;
    private bool _isStartedBulletPreparation = false;


    public void Init(Func<Bullet> funcGetBullet)
    {
        _funcGetBullet = funcGetBullet;
    }

    private void FixedUpdate()
    {
        if(_isStartedBulletPreparation)
        {
            transform.localScale -= Vector3.one * _bulletSizeStep;
            if (transform.localScale.x < 0.2f)
            {
                throw new Exception();
            }
            _bulletSize += _bulletSizeStep;
        }
    }

    public void StartBulletPreparation()
    {
        _isStartedBulletPreparation = true;
        _bulletSize = 0f;
    }



    public void Fire()
    {
        _isStartedBulletPreparation = false;
        var bullet = _funcGetBullet();
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;

        bullet.transform.localScale = Vector3.one * _bulletSize * _bulletSizeMultiplier;
    }
}