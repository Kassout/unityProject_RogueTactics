using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManagerState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        
        StartCoroutine(ManageTurn());
    }

    private IEnumerator ManageTurn()
    {
        if (Turn.actor != null)
        {
            EndActorTurn();
        }
        
        yield return null;
        
        if (Units.All(unit => unit.hasEndTurn))
        {
            this.PostNotification(BattleController.TurnCompletedNotification);
            SetupNextTurn();
        } 
        else if (!Units.Any(unit => !unit.hasEndTurn && unit.GetComponent<Driver>().Current == Drivers.Human))
        {
            SetupComputerTurn();
        }
        
        owner.ChangeState<SelectUnitState>();
    }

    private void SetupNextTurn()
    {
        Turn.currentDriver = Drivers.Human;
            
        foreach (var unit in Units)
        {
            unit.hasEndTurn = false;
            unit.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        this.PostNotification(BattleController.TurnBeganNotification);
    }

    private void SetupComputerTurn()
    {
        Turn.currentDriver = Drivers.Computer;
    }
    
    private void EndActorTurn()
    {
        Turn.actor.hasEndTurn = true;
        Turn.actor.GetComponentInChildren<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f);
        Turn.ability = null;
        Turn.targets = new List<WorldTile>();
    }
}
