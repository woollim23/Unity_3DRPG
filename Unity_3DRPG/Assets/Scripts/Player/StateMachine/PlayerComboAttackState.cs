using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedCombo;
    private bool alreadyApplyForce;

    AttackInfoData attackInfoData;
    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyAppliedCombo = false;
        alreadyApplyForce = false;

        int comboindex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboindex);
        stateMachine.Player.Animator.SetInteger("Combo", comboindex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);


        if(!alreadyAppliedCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizeTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizeTime < 1f)
        {
            if(normalizeTime >= attackInfoData.ComboTransitionTime)
            {
                // ƒﬁ∫∏ Ω√µµ
                TryComboAttack();
            }

            if(normalizeTime >= attackInfoData.ForceTransitionTime)
            {
                // ¥Ô«Œ Ω√µµ
                TryApplyForce();
            }

            stateMachine.Player.Weapon.SetAttack(attackInfoData.Damage, attackInfoData.Force);
            stateMachine.Player.Weapon.gameObject.SetActive(true);
        }
        else
        {
            if(alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                stateMachine.Player.Weapon.gameObject.SetActive(false);
            }
        }
    }

    void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyAppliedCombo = true;
    }

    void TryApplyForce()
    {
        if(alreadyApplyForce) return;
        alreadyApplyForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(Vector3.forward * attackInfoData.Force);
    }
}
