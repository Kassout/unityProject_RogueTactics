using System.Collections.Generic;
using Random = System.Random;

public class AttackOption
{
    private class Mark
    {
        private WorldTile _worldTile;
        public readonly bool isMatch;

        public Mark(WorldTile worldTile, bool isMatch)
        {
            this._worldTile = worldTile;
            this.isMatch = isMatch;
        }
    }

    public WorldTile target;
    public List<WorldTile> areaTargets = new List<WorldTile>();
    public bool isCasterMatch;
    public WorldTile bestMoveWorldTile { get; private set; }
    public int bestScore { get; private set; }
    private readonly List<Mark> _marks = new List<Mark>();
    private readonly List<WorldTile> _moveTargets = new List<WorldTile>();

    public void AddMoveTarget(WorldTile worldTile)
    {
        // Dont allow moving to a tile that would negatively affect the caster
        if (!isCasterMatch && areaTargets.Contains(worldTile))
        {
            return;
        }
        
        _moveTargets.Add(worldTile);
    }

    public void AddMark(WorldTile worldTile, bool isMatch)
    {
        _marks.Add(new Mark(worldTile, isMatch));
    }

    public int GetScore()
    {
        GetBestMoveTarget();
        if (bestMoveWorldTile == null)
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

        if (isCasterMatch && areaTargets.Contains(bestMoveWorldTile))
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

        bestMoveWorldTile = _moveTargets[new Random().Next(_moveTargets.Count)];
    }
}
