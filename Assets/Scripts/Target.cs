using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void OpenDoor()
    {
        _animator.SetTrigger("OpenDoor");
    }
}