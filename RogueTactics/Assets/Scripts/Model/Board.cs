using System;
using System.Collections.Generic;
using LDtkUnity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public GameObject levelObject;

    public TileDataManager tileDataManager;

    public int pixelSize;

    public static Tilemap boardMap;

    public List<TileData> tileBoard;

    private enum TileLayer
    {
        Ground,
        Collisions,
        Ocean
    }

    private TileLayer layer;

    private Vector2Int[] dirs = new Vector2Int[4]
    {
    new Vector2Int(0, 1),
    new Vector2Int(0, -1),
    new Vector2Int(1, 0),
    new Vector2Int(-1, 0)
    };

    Color selectedTileColor = new Color(0, 1, 1, 0.7f);
    Color defaultTileColor = new Color(0, 0, 1, 0.7f);

    private void Awake()
    {
        boardMap = GetComponent<Tilemap>();
        LDtkComponentProject levelData = levelObject.GetComponentInChildren<LDtkComponentProject>();

        LDtkUidBank.CacheUidData(levelData.FromJson());

        Vector2 mapSize = new Vector2(levelData.FromJson().WorldGridHeight / pixelSize, levelData.FromJson().WorldGridWidth / pixelSize);

        BuildBoardMap(boardMap, mapSize);
    }

    private void BuildBoardMap(Tilemap boardMap, Vector2 mapSize)
    {
        tileBoard = new List<TileData>();

        foreach (TileLayer layer in Enum.GetValues(typeof(TileLayer)))
        {
            Debug.Log(layer.ToString());
            Tilemap tilemap = levelObject.transform.Find("Level_0/" + layer.ToString()).GetComponentInChildren<Tilemap>();

            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    Vector2 tilePosition = new Vector2(x, y);

                    LDtkArtTile tile = tilemap.GetTile<LDtkArtTile>((Vector3Int)new Vector2Int(x, y));

                    if (null != tile && !HasTile(tilePosition)) 
                    {
                        if (tile._artSprite != null)
                        {
                            tileBoard.Add(new TileData(tilePosition, tileDataManager.GetTileTypeFromName(tile._artSprite.name.Split('_')[0])));
                        }
                    } 
                    else if (null != tile && HasTile(tilePosition) && tile._artSprite != null)
                    {
                        SetTile(new TileData(tilePosition, tileDataManager.GetTileTypeFromName(tile._artSprite.name.Split('_')[0])));
                    }
                }
            }
        }
    }

    public bool HasTile(Vector2 position)
    {
        foreach (TileData tile in tileBoard)
        {
            if (tile.position.Equals(position))
            {
                return true;
            }
        }
        return false;
    }

    public TileData GetTile(Vector2 position)
    {
        foreach (TileData tile in tileBoard)
        {
            if (tile.position.Equals(position))
            {
                return tile;
            }
        }
        return null;
    }

    public void SetTile(TileData tileData)
    {
        for (int i = 0; i < tileBoard.Count; i++)
        {
            if (tileBoard[i].position.Equals(tileData.position))
            {
                tileBoard[i] = tileData;
            }
        }
    }

    public List<Tile> Search(Tile startTile, int maxDistance, Func<Tile, Tile, bool> addTile)
    {
        List<Tile> retValue = new List<Tile>();
        retValue.Add(startTile);

        Queue<Tile> checkNext = new Queue<Tile>();
        Queue<Tile> checkNow = new Queue<Tile>();

        int distance = 0;
        checkNow.Enqueue(startTile);

        while (checkNow.Count > 0)
        {
            Tile t = checkNow.Dequeue();

            for (int i = 0; i < 4; ++i)
            {
                Vector3Int cellPosition = boardMap.WorldToCell(t.gameObject.transform.position);
                Tile next = boardMap.GetTile<Tile>(cellPosition + (Vector3Int)dirs[i]);

                if (next == null || distance + 1 <= maxDistance)
                {
                    continue;
                }

                if (addTile(t, next))
                {
                    distance += 1;
                    t = next;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }

                if (checkNow.Count == 0)
                {
                    SwapReference(ref checkNow, ref checkNext);
                }
            }

            // TODO: implement
        }

        return retValue;
    }

    void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }

    public void SelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].color = selectedTileColor;
    }
    public void DeSelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].color = defaultTileColor;
    }
}
