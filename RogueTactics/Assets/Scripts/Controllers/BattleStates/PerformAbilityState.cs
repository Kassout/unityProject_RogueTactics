using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerformAbilityState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        turn.hasUnitActed = true;
        turn.lockMove = true;

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        turn.actor.animator.SetTrigger("attack");

        yield return new WaitForSeconds(turn.actor.animator.GetCurrentAnimatorStateInfo(0).length);
        
        ApplyAbility();

        if (IsBattleOver())
        {
            owner.ChangeState<CutSceneState>();
        }
        else
        {
            owner.ChangeState<TurnManagerState>();
        }
    }

    private void ApplyAbility()
    {
        turn.ability.Perform(turn.targets);
    }
}
