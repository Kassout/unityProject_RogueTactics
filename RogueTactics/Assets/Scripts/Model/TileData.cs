using System;
using UnityEngine;

[Serializable]
public class TileData
{   
    public GameObject content;

    public Vector2 position;

    public TileType type;

    public bool isCollider;

    public TileData(Vector2 position) {
        content = null;
        this.position = position;
        type = null;
        isCollider = false;
    }

    public TileData(Vector2 position, TileType tileType) {
        content = null;
        this.position = position;
        type = tileType;
        isCollider = false;
    }
}
