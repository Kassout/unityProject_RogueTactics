using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : BattleState 
{
    public override void Enter ()
    {
        base.Enter ();
        StartCoroutine("Sequence");
    }
  
    IEnumerator Sequence ()
    {
        UnitMovement m = owner.currentUnit.GetComponent<UnitMovement>();
        yield return StartCoroutine(m.Traverse(owner.CurrentTile));
        owner.ChangeState<SelectUnitState>();
    }
}
