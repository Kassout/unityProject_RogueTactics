using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AttackOption
{
    private class Mark
    {
        private TileDefinitionData _tile;
        public readonly bool isMatch;

        public Mark(TileDefinitionData tile, bool isMatch)
        {
            this._tile = tile;
            this.isMatch = isMatch;
        }
    }

    public TileDefinitionData target;
    public List<TileDefinitionData> areaTargets = new List<TileDefinitionData>();
    public bool isCasterMatch;
    public TileDefinitionData bestMoveTile { get; private set; }
    public int bestScore { get; private set; }
    private readonly List<Mark> _marks = new List<Mark>();
    private readonly List<TileDefinitionData> _moveTargets = new List<TileDefinitionData>();

    public void AddMoveTarget(TileDefinitionData tile)
    {
        // Dont allow moving to a tile that would negatively affect the caster
        if (!isCasterMatch && areaTargets.Contains(tile))
        {
            return;
        }
        
        _moveTargets.Add(tile);
    }

    public void AddMark(TileDefinitionData tile, bool isMatch)
    {
        _marks.Add(new Mark(tile, isMatch));
    }

    public int GetScore()
    {
        GetBestMoveTarget();
        if (bestMoveTile == null)
        {
            return 0;
        }

        int score = 0;
        for (int i = 0; i < _marks.Count; ++i)
        {
            if (_marks[i].isMatch)
            {
                score++;
            }
            else
            {
                score--;
            }
        }

        if (isCasterMatch && areaTargets.Contains(bestMoveTile))
        {
            score++;
        }

        return score;
    }

    private void GetBestMoveTarget()
    {
        if (_moveTargets.Count == 0)
        {
            return;
        }

        bestMoveTile = _moveTargets[new Random().Next(_moveTargets.Count)];
    }
}
