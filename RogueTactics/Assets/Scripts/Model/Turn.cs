using System.Collections.Generic;
using Model;
using UnityEngine;

public class Turn
{
    public Unit actor;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public bool lockMove;
    public Ability ability;
    public List<TileDefinitionData> targets;
    public PlanOfAttack plan;
    public Drivers currentDriver;

    private TileDefinitionData _startTile;

    public void Change(Unit current)
    {
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        _startTile = actor.TileDefinition;
        plan = null;
    }

    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(_startTile);
        actor.Match();
    }
}
