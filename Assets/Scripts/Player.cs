using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action onScale;

    public bool IsGrounded => _groundTrigger.IsExistAnyThinkAtTrigger;
    public float Scale => transform.localScale.x;

    [SerializeField] private float _bulletSizeStep = 0.01f;
    [SerializeField] private float _bulletSizeMultiplier = 10;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private GroundTrigger3D _groundTrigger;

    private Func<Bullet> _funcGetBullet;
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
            onScale?.Invoke();
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

    public void Jump()
    {
        _rigidBody.linearVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);
        var force = Vector3.Scale(new Vector3(transform.forward.x, 1, transform.forward.z), _jumpForce);
        _rigidBody.AddForce(force, ForceMode.Impulse);
    }
}