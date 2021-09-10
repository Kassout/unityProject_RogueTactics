using System.Collections;
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
        if (turn.actor != null)
        {
            EndActorTurn();
        }
        
        yield return null;
        
        if (units.All(unit => unit.hasEndTurn))
        {
            this.PostNotification(BattleController.TurnCompletedNotification);
            SetupNextTurn();
        } 
        else if (!units.Any(unit => !unit.hasEndTurn && unit.GetComponent<Driver>().Current == Drivers.Human))
        {
            SetupComputerTurn();
        }
        
        owner.ChangeState<SelectUnitState>();
    }

    private void SetupNextTurn()
    {
        turn.currentDriver = Drivers.Human;
            
        foreach (var unit in units)
        {
            unit.hasEndTurn = false;
            unit.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        
        this.PostNotification(BattleController.TurnBeganNotification);
    }

    private void SetupComputerTurn()
    {
        turn.currentDriver = Drivers.Computer;
    }
    
    private void EndActorTurn()
    {
        turn.actor.hasEndTurn = true;
        turn.actor.GetComponentInChildren<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f);
    }
}
