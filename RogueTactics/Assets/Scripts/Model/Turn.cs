using System.Collections.Generic;

public class Turn
{
    public Unit actor;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public bool lockMove;
    public Ability ability;
    public List<WorldTile> targets;
    public PlanOfAttack plan;
    public Drivers currentDriver;

    private WorldTile _startWorldTile;

    public void Change(Unit current)
    {
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        _startWorldTile = actor.tile;
        plan = null;
    }

    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(_startWorldTile);
        actor.Match();
    }
}
