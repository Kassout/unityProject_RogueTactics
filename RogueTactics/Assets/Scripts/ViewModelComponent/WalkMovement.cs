using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkMovement : UnitMovement
{
    protected override bool ExpandSearch (Tile from, Tile to)
    {
        // Skip if the tile is occupied by an enemy
        if (to.sprite != null) {
            return false;
        }
        
        return base.ExpandSearch(from, to);
    }

    public override IEnumerator Traverse (Tile tile)
    {
        unit.Place(tile.gameObject.transform.position);
        // Build a list of way points from the unit's 
        // starting tile to the destination tile
        List<Tile> targets = new List<Tile>();
        while (tile != null)
        {
            targets.Insert(0, tile);
        }
        // Move to each way point in succession
        for (int i = 1; i < targets.Count; ++i)
        {
            Tile from = targets[i-1];
            Tile to = targets[i];
            yield return StartCoroutine(Walk(to));
        }
        yield return null;
    }

    IEnumerator Walk (Tile target)
    {
        transform.Translate(target.gameObject.transform.position);
        yield return null;
    }
}
