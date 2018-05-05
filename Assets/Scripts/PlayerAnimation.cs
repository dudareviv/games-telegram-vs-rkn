using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnImmortalStart()
    {
        _animator.SetBool("IsImmortal", true);
    }

    public void OnImmortalCancelled()
    {
        _animator.Rebind();
    }

    public void OnImmortalFinish()
    {
        _animator.SetBool("IsImmortal", false);
    }
}