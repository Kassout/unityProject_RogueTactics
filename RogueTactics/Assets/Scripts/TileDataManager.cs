using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileDataManager", order = 1)]
public class TileDataManager : ScriptableObject
{
    public List<TileType> tileTypes;

    public TileType GetTileTypeFromName(string name)
    {
        foreach (TileType tileType in tileTypes)
        {
            if (tileType.tileName.ToString().Equals(name))
            {
                return tileType;
            }
        }
        return null;
    }
}
