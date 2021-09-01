using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "TileTypeInstance", menuName = "ScriptableObjects/TileType", order = 1)]
    public class TileTypeObject : ScriptableObject
    {
        [Serializable]
        public enum TileTypeEnum
        {
            Tree,
            Grass,
            Dirt,
            Water,
            Coast
        }

        public TileTypeEnum tileTypeName;

        public Modifier[] propertyModifiers;

        [Serializable]
        public struct Modifier
        {
            public string property;
            public int value;
        }
    }
}