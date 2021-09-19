using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LDtkUnity;
using Model;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    public static List<WorldTile> tileBoard { get; private set; }
        
    public static Vector2 min => s_min;

    public static Vector2 max => s_max;
    
    public static readonly Vector2Int[] Dirs = new Vector2Int[4]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };

    [SerializeField] private GameObject levelObject;

    [SerializeField] private TileTypeManager tileTypeManager;
    
    [SerializeField] private Sprite movableTileSprite;

    [SerializeField] private Sprite attackableTileSprite;

    [SerializeField] private Sprite targetedTileSprite;

    [SerializeField] private Color defaultTileColor = new Color(0, 1, 1, 0.7f);

    private static Vector2 s_min;

    private static Vector2 s_max;
        
    private static Tilemap s_boardTileMap;
        
    private static Dictionary<Vector2, string> s_levelTilesDefinition;

    private static Dictionary<TileEnumDefinition, List<string>> s_tileSetDefinition;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);

        Instance = this;
        //DontDestroyOnLoad(gameObject);

        s_boardTileMap = GetComponent<Tilemap>();
        var levelData = levelObject.GetComponentInChildren<LDtkComponentProject>();

        s_min = new Vector2(int.MaxValue, int.MaxValue);
        s_max = new Vector2(int.MinValue, int.MinValue);
            
        ReadTileSetDefinitions(levelData);
        ReadLevelTilesDefinition(levelData);

        BuildBoardMap();
    }

    private void ReadTileSetDefinitions(LDtkComponentProject levelData)
    {
        s_tileSetDefinition = new Dictionary<TileEnumDefinition, List<string>>();


        var definitionElement = levelData.FromJson().Defs;
        var tilesetsDefinition = new List<TilesetDefinition>(definitionElement.Tilesets);
        foreach (var tileset in tilesetsDefinition)
        {
            var enumTagsElement = new List<Dictionary<string, object>>(tileset.EnumTags);
            var tilesetUid = tileset.Uid;
            foreach (var enumDefinition in enumTagsElement)
                if (enumDefinition.TryGetValue("enumValueId", out var enumValue))
                {
                    var tileEnumDefinition =
                        (TileEnumDefinition)Enum.Parse(typeof(TileEnumDefinition), enumValue.ToString());
                    if (s_tileSetDefinition.ContainsKey(tileEnumDefinition))
                    {
                        if (enumDefinition.TryGetValue("tileIds", out var tileIds))
                            foreach (var tileId in (IEnumerable)tileIds)
                            {
                                var tileName = tilesetUid + "_" + tileId;
                                if (!s_tileSetDefinition[tileEnumDefinition].Contains(tileName))
                                    s_tileSetDefinition[tileEnumDefinition].Add(tileName);
                            }
                    }
                    else
                    {
                        if (enumDefinition.TryGetValue("tileIds", out var tileIds))
                        {
                            var tileIdentifiers = new List<string>();
                            foreach (var tileId in (IEnumerable)tileIds)
                            {
                                var tileName = tilesetUid + "_" + tileId;
                                tileIdentifiers.Add(tileName);
                            }

                            s_tileSetDefinition.Add(tileEnumDefinition, tileIdentifiers);
                        }
                    }
                }
        }
    }

    private void ReadLevelTilesDefinition(LDtkComponentProject levelData)
    {
        s_levelTilesDefinition = new Dictionary<Vector2, string>();

        var levels = new List<Level>(levelData.FromJson().Levels);
        foreach (var level in levels)
        {
            IEnumerable<LayerInstance> layerInstances = new List<LayerInstance>(level.LayerInstances);
            foreach (var layerInstance in layerInstances.Reverse())
            {
                var autoLayerTiles = new List<TileInstance>(layerInstance.AutoLayerTiles);
                if (autoLayerTiles.Any())
                {
                    string tilesetDefinitionId = null;
                    if (layerInstance.TilesetDefUid != null)
                        tilesetDefinitionId = layerInstance.TilesetDefUid.ToString();

                    foreach (var autoLayerTile in autoLayerTiles)
                    {
                        var tileDefinitionId = tilesetDefinitionId + "_" + autoLayerTile.T;

                        var tilePosition = new Vector2(autoLayerTile.Px[0] / layerInstance.CHei,
                            (level.PxHei - (layerInstance.CHei + autoLayerTile.Px[1])) / layerInstance.CHei);

                        if (s_levelTilesDefinition.ContainsKey(tilePosition))
                            s_levelTilesDefinition[tilePosition] = tileDefinitionId;
                        else
                            s_levelTilesDefinition.Add(tilePosition, tileDefinitionId);
                    }
                }
            }
        }
    }

    private void BuildBoardMap()
    {
        tileBoard = new List<WorldTile>();

        foreach (var levelTileDefinition in s_levelTilesDefinition)
        {
            var tileTypeName = GetTileEnumDefinitionByTileId(levelTileDefinition.Value);

            if (null != tileTypeName)
            {
                tileBoard.Add(new WorldTile(levelTileDefinition.Key,
                    GetTileTypeFromName(tileTypeName.ToString())));

                s_min.x = Mathf.Min(s_min.x, levelTileDefinition.Key.x);
                s_min.y = Mathf.Min(s_min.y, levelTileDefinition.Key.y);
                s_max.x = Mathf.Min(s_max.x, levelTileDefinition.Key.x);
                s_max.y = Mathf.Min(s_max.y, levelTileDefinition.Key.y);
            }
        }
    }

    private TileEnumDefinition? GetTileEnumDefinitionByTileId(string levelTileId)
    {
        foreach (var tileSetDefinition in s_tileSetDefinition)
            if (tileSetDefinition.Value.Contains(levelTileId))
                return tileSetDefinition.Key;
        return null;
    }

    private WorldTileType GetTileTypeFromName(string tileTypeName)
    {
        foreach (var tileType in tileTypeManager.tileTypes)
            if (tileType.tileTypeName.ToString().Equals(tileTypeName))
                return tileType;
        return null;
    }

    public List<WorldTile> Search(WorldTile startWorldTile,
        Func<WorldTile, WorldTile, bool> addTile)
    {
        var retValue = new List<WorldTile> { startWorldTile };

        ClearSearch();
        var checkNext = new Queue<WorldTile>();
        var checkNow = new Queue<WorldTile>();
        
        startWorldTile.distanceFromStartTile = 0;
        checkNow.Enqueue(startWorldTile);

        while (checkNow.Count > 0)
        {
            var t = checkNow.Dequeue();

            for (var i = 0; i < 4; ++i)
            {
                var cellPosition = s_boardTileMap.WorldToCell(t.position);
                var next = GetTile((Vector2Int)cellPosition + Dirs[i]);

                if (next == null || next.distanceFromStartTile <= t.distanceFromStartTile + 1) continue;

                if (addTile(t, next))
                {
                    next.distanceFromStartTile = t.distanceFromStartTile + 1;
                    next.parent = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }

                if (checkNow.Count == 0)
                {
                    SwapReference(ref checkNow, ref checkNext);
                }
            }
        }

        return retValue;
    }
    
    void ClearSearch ()
    {
        foreach (WorldTile t in tileBoard)
        {
            t.parent = null;
            t.distanceFromStartTile = int.MaxValue;
        }
    }

    private void SwapReference(ref Queue<WorldTile> a, ref Queue<WorldTile> b)
    {
        (a, b) = (b, a);
    }

    public void SelectTiles(List<WorldTile> tiles)
    {
        var defaultTile = ScriptableObject.CreateInstance<Tile>();
        defaultTile.sprite = movableTileSprite;
        for (var i = tiles.Count - 1; i >= 0; --i)
        {
            var tilePosition = new Vector3Int((int)tiles[i].position.x, (int)tiles[i].position.y, 0);
            s_boardTileMap.SetTile(tilePosition, defaultTile);
            s_boardTileMap.SetTileFlags(tilePosition, TileFlags.None);
            s_boardTileMap.SetColor(tilePosition, defaultTileColor);
        }
    }
    
    public void SelectAbilityTiles(List<WorldTile> tiles)
    {
        var defaultTile = ScriptableObject.CreateInstance<Tile>();
        defaultTile.sprite = attackableTileSprite;
        for (var i = tiles.Count - 1; i >= 0; --i)
        {
            var tilePosition = new Vector3Int((int)tiles[i].position.x, (int)tiles[i].position.y, 0);
            s_boardTileMap.SetTile(tilePosition, defaultTile);
            s_boardTileMap.SetTileFlags(tilePosition, TileFlags.None);
            s_boardTileMap.SetColor(tilePosition, defaultTileColor);
        }
    }

    public void SelectAreaTargetTiles(List<WorldTile> tiles)
    {
        var defaultTile = ScriptableObject.CreateInstance<Tile>();
        defaultTile.sprite = targetedTileSprite;
        for (var i = tiles.Count - 1; i >= 0; --i)
        {
            var tilePosition = new Vector3Int((int)tiles[i].position.x, (int)tiles[i].position.y, 0);
            s_boardTileMap.SetTile(tilePosition, defaultTile);
            s_boardTileMap.SetTileFlags(tilePosition, TileFlags.None);
            s_boardTileMap.SetColor(tilePosition, defaultTileColor);
        }
    }

    public void DeSelectTiles(List<WorldTile> tiles)
    {
        for (var i = tiles.Count - 1; i >= 0; --i)
        {
            var tilePosition = new Vector3Int((int)tiles[i].position.x, (int)tiles[i].position.y, 0);
            s_boardTileMap.SetTile(tilePosition, null);
        }
    }

    public static WorldTile GetTile(Vector2 position)
    {
        foreach (var tile in tileBoard)
            if (tile.position.Equals(position))
                return tile;
        return null;
    }

    private enum TileEnumDefinition
    {
        Grass,
        Water,
        Coast,
        Building,
        Tree,
        Mountain,
        River
    }
}