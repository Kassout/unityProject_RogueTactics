using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewModelComponent
{
    public class WalkMovement : UnitMovement
    {
        protected override bool ExpandSearch (TileDefinitionData from, TileDefinitionData to)
        {
            // Skip if the tile is occupied by an enemy
            if (to.content != null || to.doCollide) {
                return false;
            }
        
            return base.ExpandSearch(from, to);
        }

        public override IEnumerator Traverse (TileDefinitionData targetTile)
        {
            Bresenham path = new Bresenham(unitInstance.TileDefinition.position, targetTile.position);
            
            unitInstance.Place(targetTile);
            // Build a list of way points from the unit's 
            // starting tile to the destination tile
            List<TileDefinitionData> tilePath = new List<TileDefinitionData>();
            foreach (Vector2 point in path)
            {
                tilePath.Add(Board.GetTile(point));
            }
            
            for (int i = 1; i < tilePath.Count; ++i)
            {
                TileDefinitionData from = tilePath[i-1];
                TileDefinitionData to = tilePath[i];
                yield return StartCoroutine(Walk(to));
            }
            yield return null;
        }

        IEnumerator Walk (TileDefinitionData target)
        {
            float timeToStart = Time.time;
            while(Vector3.Distance(transform.position, target.position) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, (Time.time - timeToStart ) * 0.5f ); //Here speed is the 1 or any number which decides the how fast it reach to one to other end.
         
                yield return null;
            }
     
            Debug.Log("Reached the target.");
            transform.localPosition = target.position;
            yield return null;
        }
    }
}
