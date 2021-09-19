using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class WalkMovement : UnitMovement
{
    [SerializeField] private float walkTime = 2f;
        
    protected override bool ExpandSearch(WorldTile from, WorldTile to)
    {
        // Skip if the tile is occupied by an enemy
        if ((to.content != null && to.content.GetComponent<Alliance>().type != GetComponent<Alliance>().type) || to.doCollide) return false;

        return base.ExpandSearch(from, to);
    }

    public override IEnumerator Traverse(WorldTile targetWorldTile)
    {
        bool tileIsTarget = targetWorldTile.content;
        var path = new Bresenham(unitInstance.tile.position, targetWorldTile.position);
        
        // Build a list of way points from the unit's 
        // starting tile to the destination tile
        var tilePath = new List<WorldTile>();
        foreach (Vector2 point in path) tilePath.Add(Board.GetTile(point));
        
        if (tileIsTarget)
        {
            tilePath.RemoveAt(tilePath.Count - 1);
            targetWorldTile = Board.GetTile(tilePath[tilePath.Count - 1].position);
        }
        
        unitInstance.Place(targetWorldTile);
        
        for (var i = 1; i < tilePath.Count; ++i)
        {
            unitInstance.animator.SetBool("isWalking", true);
            var from = tilePath[i - 1];
            var to = tilePath[i];
            yield return StartCoroutine(Walk(to));
        }

        unitInstance.animator.SetBool("isWalking", false);
        yield return null;
    }

    private IEnumerator Walk(WorldTile target)
    {
        var timeToStart = Time.fixedTime;
        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position =
                Vector3.Lerp(transform.position, target.position,
                    (Time.fixedTime - timeToStart) *
                    walkTime); //Here speed is the 1 or any number which decides the how fast it reach to one to other end.

            yield return null;
        }

        Debug.Log("Reached the target.");
        transform.localPosition = target.position;
        yield return null;
    }
}