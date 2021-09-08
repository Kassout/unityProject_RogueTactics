using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.ChangeState<SelectUnitState>();
    }
}
