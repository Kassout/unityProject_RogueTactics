using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileType", order = 1)]
public class TileType : ScriptableObject
{
    public TileTypeName tileName;

    public Modifier[] modifiers;

    [Serializable]
    public enum TileTypeName {
        Tree,
        Grass,
        Dirt,
        Water,
        Coast
    }

    [Serializable]
    public struct Modifier {
        public string property;
        public int value;
    }
}
