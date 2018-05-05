using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public class HealthManager : Singleton<HealthManager>
{
    [Range(1, 5)]
    public int MaxHealth = 3;

    public int CurrentHealth = 3;

    public float ImmortalCooldown = 3f;
    public bool IsImmortal;

    public IntUnityEvent OnHealthUpdate = new IntUnityEvent();

    public UnityEvent OnGetDamage;

    public UnityEvent OnImmortalStart;
    public UnityEvent OnImmortalCancelled;
    public UnityEvent OnImmortalFinish;

    private Object thisLock = new Object();

    private void Awake()
    {
        Reset();
    }

    public void GetHeal(int value = 1)
    {
        SetHealth(CurrentHealth + value);
    }

    public void GetDamage(int value = 1)
    {
        if (IsImmortal)
            return;

        lock (thisLock) {
            OnGetDamage.Invoke();
            
            SetHealth(CurrentHealth - value);

            if (CurrentHealth <= 0) {
                GameManager.Instance.GameOver();
            }
        }

        StartCoroutine(GetImmortalAfterDamage());
    }

    public void Reset()
    {
        SetHealth(MaxHealth);

        StopCoroutine(GetImmortalAfterDamage());
        OnImmortalCancelled.Invoke();
    }

    public void SetHealth(int value)
    {
        CurrentHealth = value;

        OnHealthUpdate.Invoke(CurrentHealth);
    }

    private IEnumerator GetImmortalAfterDamage()
    {
        IsImmortal = true;

        OnImmortalStart.Invoke();

        yield return new WaitForSeconds(ImmortalCooldown);

        IsImmortal = false;

        OnImmortalFinish.Invoke();
    }

    private void OnValidate()
    {
        if (CurrentHealth < 1) {
            CurrentHealth = 1;
        }

        if (CurrentHealth > 5) {
            CurrentHealth = 5;
        }
    }
}