using UnityEngine;

public class Bullet : BasePoolElement
{
    [SerializeField] private float _speed;

    public override void SetUse()
    {
        base.SetUse();
        gameObject.SetActive(true);
    }

    public override void SetUnUse()
    {
        base.SetUnUse();
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(IsUsing)
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }
    }
}