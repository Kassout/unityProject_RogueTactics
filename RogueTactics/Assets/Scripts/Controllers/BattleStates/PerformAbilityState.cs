using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        ApplyAbility(turn.ability, turn.targets);
        
        foreach (WorldTile target in turn.targets)
        {
            if (target.content.GetComponentInChildren<AbilityRange>().range >=
                Vector3.Magnitude(turn.actor.transform.position - target.content.transform.position))
            {
                target.content.GetComponent<Unit>().animator.SetTrigger("attack");
            
                yield return new WaitForSeconds(target.content.GetComponent<Unit>().animator.GetCurrentAnimatorStateInfo(0).length);
            
                ApplyAbility(target.content.GetComponentInChildren<Ability>(), new List<WorldTile>() {Board.GetTile(turn.actor.tile.position)});
            }
        } 

        if (IsBattleOver())
        {
            owner.ChangeState<CutSceneState>();
        }
        else
        {
            owner.ChangeState<TurnManagerState>();
        }
    }

    private void ApplyAbility(Ability ability, List<WorldTile> targets)
    {
        ability.Perform(targets);
    }
}
