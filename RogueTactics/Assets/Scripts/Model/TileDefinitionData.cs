using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class TileDefinitionData
    {
        public GameObject content;

        public Vector2 position;

        public TileTypeObject tileType;

        public bool doCollide;

        public TileDefinitionData(Vector2 position)
        {
            content = null;
            this.position = position;
            tileType = null;
            doCollide = false;
        }

        public TileDefinitionData(Vector2 position, TileTypeObject tileTileType)
        {
            content = null;
            this.position = position;
            tileType = tileTileType;
            doCollide = tileType.tileTypeName.Equals(TileTypeObject.TileTypeEnum.Coast);
        }
    }
}