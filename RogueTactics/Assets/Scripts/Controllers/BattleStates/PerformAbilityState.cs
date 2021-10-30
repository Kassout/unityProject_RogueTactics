using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformAbilityState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Turn.hasUnitActed = true;
        Turn.lockMove = true;

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        Turn.actor.animator.SetTrigger("attack");

        yield return new WaitForSeconds(Turn.actor.animator.GetCurrentAnimatorStateInfo(0).length);
        
        ApplyAbility(Turn.ability, Turn.targets);
        
        foreach (WorldTile target in Turn.targets)
        {
            if (target.content.GetComponentInChildren<AbilityRange>().range >=
                Vector3.Magnitude(Turn.actor.transform.position - target.content.transform.position))
            {
                target.content.GetComponent<Unit>().animator.SetTrigger("attack");
            
                yield return new WaitForSeconds(target.content.GetComponent<Unit>().animator.GetCurrentAnimatorStateInfo(0).length);
            
                ApplyAbility(target.content.GetComponentInChildren<Ability>(), new List<WorldTile>() {Board.GetTile(Turn.actor.tile.position)});
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
