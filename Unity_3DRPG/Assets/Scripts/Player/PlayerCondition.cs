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
    Condition experiencePoin { get { return uiCondition.experiencePoint; } }

    public event Action onDie;

    public bool IsDie = false;


    void Update()
    {

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
        // 캐릭터 스펙 증가 아이템일 경우 : 현재는 속도 증가 아이템, 호박하나만 있음
        // TODO : 음식 효과를 여러게 저장할 수 있게 리스트 구현, 데이터 관리 로직 구현
        StartCoroutine(DopingDuration(value, duration));
    }

    IEnumerator DopingDuration(float value, float duration)
    {
        float tempSpeed = CharacterManager.Instance.Player.stateMachine.MovementSpeed;
        // value 만큼 이속 증가
        CharacterManager.Instance.Player.stateMachine.MovementSpeed += value;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            yield return null; // 한 프레임 대기
        }

        CharacterManager.Instance.Player.stateMachine.MovementSpeed = tempSpeed;
    }

    public void Die()
    {
        Debug.Log("죽었다");
        IsDie = true;

        onDie?.Invoke();

        enabled = false;
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
    }
}
