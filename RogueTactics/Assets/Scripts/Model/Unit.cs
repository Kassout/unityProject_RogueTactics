using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour
{
    public Vector2 position { get; protected set; }

    public Tilemap collisionMap;

    public Tilemap groundMap;

    public void Place(Vector2 position)
    {
        // // Make sure old tile location is not still pointing to this unit
        // if (position != null && tile.content == gameObject) 
        // {
        //     tile.content = null;
        // }
    
        // // Link unit and tile references
        // tile = target;
    
        // if (target != null)
        //     target.content = gameObject;
    }
}
