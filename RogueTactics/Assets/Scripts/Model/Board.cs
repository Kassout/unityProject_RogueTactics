using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LDtkUnity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Model
{
    public class Board : MonoBehaviour
    {
        private static Board s_instance;

        public static Board Instance => s_instance;

        [SerializeField] private GameObject levelObject;
    
        [SerializeField] private TileTypeManager tileTypeManager;
        
        [SerializeField] private Color selectedTileColor = new Color(0, 1, 1, 0.7f);

        [SerializeField] private Sprite defaultSprite;
        
        [SerializeField] private Color defaultTileColor = new Color(0, 0, 1, 0.7f);

        private static Tilemap s_boardTileMap;
    
        private static List<TileDefinitionData> s_tileBoard;
    
        private static Dictionary<Vector2, string> s_levelTilesDefinition;
    
        private static Dictionary<TileEnumDefinition, List<string>> s_tileSetDefinition;

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
        
        private readonly Vector2Int[] _dirs = new Vector2Int[4]
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0)
        };

        private void Awake()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
            }

            s_instance = this;
            //DontDestroyOnLoad(gameObject);

            s_boardTileMap = GetComponent<Tilemap>();
            LDtkComponentProject levelData = levelObject.GetComponentInChildren<LDtkComponentProject>();
        
            ReadTileSetDefinitions(levelData);
            ReadLevelTilesDefinition(levelData);
        
            BuildBoardMap();
        }

        private void Start()
        { 
            // LDtkComponentProject levelData = levelObject.GetComponentInChildren<LDtkComponentProject>();
            //
            // ReadTileSetDefinitions(levelData);
            // ReadLevelTilesDefinition(levelData);
            //
            // BuildBoardMap();
        }

        private void ReadTileSetDefinitions(LDtkComponentProject levelData)
        {
            s_tileSetDefinition = new Dictionary<TileEnumDefinition, List<string>>();

            
            Definitions definitionElement = levelData.FromJson().Defs;
            List<TilesetDefinition> tilesetsDefinition = new List<TilesetDefinition>(definitionElement.Tilesets);
            foreach (TilesetDefinition tileset in tilesetsDefinition)
            {
                List<Dictionary<string, object>> enumTagsElement = new List<Dictionary<string, object>>(tileset.EnumTags);
                long tilesetUid = tileset.Uid;
                foreach (Dictionary<string, object> enumDefinition in enumTagsElement)
                {
                    if (enumDefinition.TryGetValue("enumValueId", out var enumValue))
                    {
                        TileEnumDefinition tileEnumDefinition = (TileEnumDefinition) Enum.Parse(typeof(TileEnumDefinition), enumValue.ToString());
                        if (s_tileSetDefinition.ContainsKey(tileEnumDefinition))
                        {
                            if (enumDefinition.TryGetValue("tileIds", out var tileIds))
                            {
                                foreach (var tileId in (IEnumerable) tileIds)
                                {
                                    string tileName = tilesetUid + "_" + tileId;
                                    if (!s_tileSetDefinition[tileEnumDefinition].Contains(tileName))
                                    {
                                        s_tileSetDefinition[tileEnumDefinition].Add(tileName);    
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (enumDefinition.TryGetValue("tileIds", out var tileIds))
                            {
                                List<string> tileIdentifiers = new List<string>();
                                foreach (var tileId in (IEnumerable) tileIds)
                                {
                                    string tileName = tilesetUid + "_" + tileId;
                                    tileIdentifiers.Add(tileName);
                                }
                                s_tileSetDefinition.Add(tileEnumDefinition, tileIdentifiers);
                            }
                        }
                    }
                }
            }
        }

        private void ReadLevelTilesDefinition(LDtkComponentProject levelData)
        {
            s_levelTilesDefinition = new Dictionary<Vector2, string>();

            List<Level> levels = new List<Level>(levelData.FromJson().Levels);
            foreach (Level level in levels)
            {
                IEnumerable<LayerInstance> layerInstances = new List<LayerInstance>(level.LayerInstances);
                foreach (LayerInstance layerInstance in layerInstances.Reverse())
                {
                    List<TileInstance> autoLayerTiles = new List<TileInstance>(layerInstance.AutoLayerTiles);
                    if (autoLayerTiles.Any())
                    {
                        string tilesetDefinitionId = null;
                        if (layerInstance.TilesetDefUid != null)
                        {
                            tilesetDefinitionId = layerInstance.TilesetDefUid.ToString();
                        }

                        foreach (TileInstance autoLayerTile in autoLayerTiles)
                        {
                            string tileDefinitionId = tilesetDefinitionId + "_" + autoLayerTile.T;

                            Vector2 tilePosition = new Vector2(autoLayerTile.Px[0] /  layerInstance.CHei,
                                (level.PxHei - (layerInstance.CHei + autoLayerTile.Px[1])) / layerInstance.CHei);

                            if (s_levelTilesDefinition.ContainsKey(tilePosition))
                            {
                                s_levelTilesDefinition[tilePosition] = tileDefinitionId;
                            }
                            else
                            {
                                s_levelTilesDefinition.Add(tilePosition, tileDefinitionId);
                            }
                        }
                    }
                }
            }
        }

        private void BuildBoardMap()
        {
            s_tileBoard = new List<TileDefinitionData>();
        
            foreach (KeyValuePair<Vector2, string> levelTileDefinition in s_levelTilesDefinition)
            {
                TileEnumDefinition? tileTypeName = GetTileEnumDefinitionByTileId(levelTileDefinition.Value);

                if (null != tileTypeName)
                {
                    s_tileBoard.Add(new TileDefinitionData(levelTileDefinition.Key, GetTileTypeFromName(tileTypeName.ToString())));
                }
            }
        }

        private TileEnumDefinition? GetTileEnumDefinitionByTileId(string levelTileId)
        {
            foreach (KeyValuePair<TileEnumDefinition, List<string>> tileSetDefinition in s_tileSetDefinition)
            {
                if (tileSetDefinition.Value.Contains(levelTileId))
                {
                    return tileSetDefinition.Key;
                }
            }
            return null;
        }

        private TileTypeObject GetTileTypeFromName(string tileTypeName)
        {
            foreach (TileTypeObject tileType in tileTypeManager.tileTypes)
            {
                if (tileType.tileTypeName.ToString().Equals(tileTypeName))
                {
                    return tileType;
                }
            }
            return null;
        }
    
        public List<TileDefinitionData> Search(TileDefinitionData startTile, int maxDistance, Func<TileDefinitionData, TileDefinitionData, bool> addTile)
        {
            List<TileDefinitionData> retValue = new List<TileDefinitionData> { startTile };

            Queue<TileDefinitionData> checkNext = new Queue<TileDefinitionData>();
            Queue<TileDefinitionData> checkNow = new Queue<TileDefinitionData>();
            int distance = 0;
            checkNow.Enqueue(startTile);
            
            while (checkNow.Count > 0)
            {
                TileDefinitionData t = checkNow.Dequeue();
            
                for (int i = 0; i < 4; ++i)
                {
                    Vector3Int cellPosition = s_boardTileMap.WorldToCell(t.position);
                    TileDefinitionData next = GetTile((Vector2Int) cellPosition + _dirs[i]);

                    if (next == null || distance + 1 >= maxDistance || retValue.Contains(next))
                    {
                        continue;
                    }
            
                    if (addTile(startTile, next))
                    {
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

        private void SwapReference(ref Queue<TileDefinitionData> a, ref Queue<TileDefinitionData> b)
        {
            (a, b) = (b, a);
        }

        public void SelectTiles(List<TileDefinitionData> tiles)
        {
            Tile defaultTile = ScriptableObject.CreateInstance<Tile>();
            defaultTile.sprite = defaultSprite;
            for (int i = tiles.Count - 1; i >= 0; --i)
            {
                Vector3Int tilePosition = new Vector3Int((int) tiles[i].position.x, (int) tiles[i].position.y, 0);
                s_boardTileMap.SetTile(tilePosition, defaultTile);
                s_boardTileMap.SetTileFlags(tilePosition, TileFlags.None);
                s_boardTileMap.SetColor(tilePosition, defaultTileColor);
            }
        }
        public void DeSelectTiles(List<TileDefinitionData> tiles)
        {
            for (int i = tiles.Count - 1; i >= 0; --i)
            {
                Vector3Int tilePosition = new Vector3Int((int) tiles[i].position.x, (int) tiles[i].position.y, 0);
                s_boardTileMap.SetTile(tilePosition, null);
            }
        }

        public static TileDefinitionData GetTile(Vector2 position)
        {
            foreach (TileDefinitionData tile in s_tileBoard)
            {
                if (tile.position.Equals(position))
                {
                    return tile;
                }
            }
            return null;
        }
    }
}