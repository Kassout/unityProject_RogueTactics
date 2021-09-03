using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewModelComponent
{
    public abstract class UnitMovement : MonoBehaviour
    {
        public int range
        {
            get { return _stats[StatTypes.MOV]; }
        }

        protected Stats _stats;
        
        protected Unit unitInstance;

        protected virtual void Awake()
        {
            unitInstance = GetComponent<Unit>();
        }

        protected virtual void Start()
        {
            _stats = GetComponent<Stats>();
        }

        public virtual List<TileDefinitionData> GetTilesInRange()
        {
            var retValue = Board.Instance.Search(unitInstance.TileDefinition, range, ExpandSearch);
            Filter(retValue);
            return retValue;
        }

        protected virtual bool ExpandSearch(TileDefinitionData from, TileDefinitionData to)
        {
            return (from.distanceFromStartTile + 1) <= range;
        }

        protected virtual void Filter(List<TileDefinitionData> tiles)
        {
            for (var i = tiles.Count - 1; i >= 0; --i)
                if (tiles[i].content != null)
                    tiles.RemoveAt(i);
        }

        public abstract IEnumerator Traverse(TileDefinitionData targetTile);
    }
}