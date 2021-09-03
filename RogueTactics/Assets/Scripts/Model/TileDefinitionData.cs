using System;
using Model;
using UnityEngine;

[Serializable]
public class TileDefinitionData
{
    public GameObject content;

    public Vector2 position;

    public TileTypeObject tileType;

    public bool doCollide;
    
    public int distanceFromStartTile;

    public TileDefinitionData previousTile;

    public TileDefinitionData(Vector2 position)
    {
        content = null;
        this.position = position;
        tileType = null;
        doCollide = false;
        distanceFromStartTile = 0;
        previousTile = null;
    }

    public TileDefinitionData(Vector2 position, TileTypeObject tileTileType)
    {
        content = null;
        this.position = position;
        tileType = tileTileType;
        doCollide = tileType.tileTypeName.Equals(TileTypeObject.TileTypeEnum.Coast);
        distanceFromStartTile = 0;
        previousTile = null;
    }
}