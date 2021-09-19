using System.Collections.Generic;
using Model;
using UnityEngine;

[CreateAssetMenu(fileName = "TileTypeManager", menuName = "ScriptableObjects/TileTypeManager", order = 1)]
public class TileTypeManager : ScriptableObject
{
    public List<WorldTileType> tileTypes;
}