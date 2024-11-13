using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition experiencePoin { get { return uiCondition.experiencePoint; } }

    public event Action onDie;

    public bool IsDie = false;


    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    
    public void Doping(float value, float duration)
    {
        // ĳ���� ���� ���� �������� ��� : ����� �ӵ� ���� ������, ȣ���ϳ��� ����
        // TODO : ���� ȿ���� ������ ������ �� �ְ� ����Ʈ ����, ������ ���� ���� ����
        StartCoroutine(DopingDuration(value, duration));
    }

    IEnumerator DopingDuration(float value, float duration)
    {
        float tempSpeed = CharacterManager.Instance.Player.stateMachine.MovementSpeed;
        // value ��ŭ �̼� ����
        CharacterManager.Instance.Player.stateMachine.MovementSpeed += value;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            yield return null; // �� ������ ���
        }

        CharacterManager.Instance.Player.stateMachine.MovementSpeed = tempSpeed;
    }
    
    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }

    public void Die()
    {
        Debug.Log("�׾���");
        IsDie = true;

        onDie?.Invoke();

        enabled = false;
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
    }
}
