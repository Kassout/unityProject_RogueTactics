using System;
using Model;
using UnityEngine;

[Serializable]
public class WorldTile
{
    public GameObject content;
    public Vector2 position;
    public WorldTileType worldTileType;
    public bool doCollide;
    public int distanceFromStartTile;
    public WorldTile parent;

    public WorldTile(Vector2 position)
    {
        content = null;
        this.position = position;
        worldTileType = null;
        doCollide = false;
        distanceFromStartTile = 0;
        parent = null;
    }

    public WorldTile(Vector2 position, WorldTileType worldTileWorldTileType)
    {
        content = null;
        this.position = position;
        worldTileType = worldTileWorldTileType;
        doCollide = worldTileType.tileTypeName.Equals(WorldTileType.TileTypeEnum.Coast);
        distanceFromStartTile = 0;
        parent = null;
    }
}