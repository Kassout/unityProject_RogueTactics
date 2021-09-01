using Model;

public class Turn
{
    public Unit actor;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public bool lockMove;
    
    private TileDefinitionData _startTile;

    public void Change(Unit current)
    {
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        _startTile = actor.TileDefinition;
    }

    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(_startTile);
        actor.Match();
    }
}