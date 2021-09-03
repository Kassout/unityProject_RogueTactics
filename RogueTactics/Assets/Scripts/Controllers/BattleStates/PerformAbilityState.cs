using System.Collections;
using System.Collections.Generic;
using BattleStates;
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
        // TODO: play animations, etc

        yield return null;
        
        // TODO: apply ability effects, etc
        TemporaryAttackExample();

        EndUnitTurn();
        
        owner.ChangeState<SelectUnitState>();
    }

    private void EndUnitTurn()
    {
        turn.actor.hasEndTurn = true;
        turn.actor.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
    }

    void TemporaryAttackExample()
    {
        for (int i = 0; i < turn.targets.Count; ++i)
        {
            GameObject obj = turn.targets[i].content;
            Stats stats = obj != null ? obj.GetComponentInChildren<Stats>() : null;
            if (stats != null)
            {
                stats[StatTypes.HP] -= 50;
                if (stats[StatTypes.HP] <= 0)
                    Debug.Log("KO'd Uni!", obj);
            }
        }
    }
}
