using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMovement : MonoBehaviour
{
    private int range => _unitStats[UnitStatTypes.MOV];

    private UnitStats _unitStats;
        
    protected Unit unitInstance;

    protected virtual void Awake()
    {
        unitInstance = GetComponent<Unit>();
    }

    protected virtual void Start()
    {
        _unitStats = GetComponent<UnitStats>();
    }

    public virtual List<WorldTile> GetTilesInRange()
    {
        var retValue = Board.Instance.Search(unitInstance.tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    protected virtual bool ExpandSearch(WorldTile from, WorldTile to)
    {
        return (from.distanceFromStartTile + 1) <= range;
    }

    protected virtual void Filter(List<WorldTile> tiles)
    {
        for (var i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].content != null)
                tiles.RemoveAt(i);
    }

    public abstract IEnumerator Traverse(WorldTile targetWorldTile);
}