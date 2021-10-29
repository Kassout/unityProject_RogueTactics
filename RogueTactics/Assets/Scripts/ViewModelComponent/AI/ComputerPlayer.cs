using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class ComputerPlayer : MonoBehaviour
{
    private BattleController _bc;
    private Unit actor => _bc.turn.actor;

    private Alliance _alliance => actor.GetComponent<Alliance>();

    private Unit _neareastFoe;

    private void Awake()
    {
        _bc = GetComponent<BattleController>();
    }

    public PlanOfAttack Evaluate()
    {
        // Create and fill out a plan of attack
        PlanOfAttack poa = new PlanOfAttack();
        
        // Step 1: Decide what ability to use
        AttackPattern pattern = actor.GetComponentInChildren<AttackPattern>();
        if (pattern)
        {
            pattern.Pick(poa);
        }
        else
        {
            DefaultAttackPattern(poa);
        }

        if (IsPositionIndependent(poa))
        {
            PlanPositionIndependent(poa);
        } 
        else
        {
            PlanDirectionIndependent(poa);
        }

        if (poa.ability == null)
        {
            MoveTowardOpponent(poa);
        }
        
        // Return the complete plan
        return poa;
    }

    private bool IsPositionIndependent(PlanOfAttack poa)
    {
        AbilityRange range = poa.ability.GetComponent<AbilityRange>();
        return range.positionOriented == false;
    }

    private void PlanPositionIndependent(PlanOfAttack poa)
    {
        List<WorldTile> moveOptions = GetMoveOptions();
        WorldTile worldTile = moveOptions[new Random().Next(moveOptions.Count)];
        poa.moveLocation = poa.fireLocation = worldTile.position;
    }

    private void PlanDirectionIndependent(PlanOfAttack poa)
    {
        WorldTile startWorldTile = actor.tile;
        Dictionary<WorldTile, AttackOption> map = new Dictionary<WorldTile, AttackOption>();
        AbilityRange ar = poa.ability.GetComponent<AbilityRange>();
        List<WorldTile> moveOptions = GetMoveOptions();

        for (int i = 0; i < moveOptions.Count; ++i)
        {
            WorldTile moveWorldTile = moveOptions[i];
            actor.Place(moveWorldTile);
            List<WorldTile> fireOptions = ar.GetTilesInRange();

            for (int j = 0; j < fireOptions.Count; ++j)
            {
                WorldTile fireWorldTile = fireOptions[j];
                AttackOption ao = null;
                if (map.ContainsKey(fireWorldTile))
                {
                    ao = map[fireWorldTile];
                }
                else
                {
                    ao = new AttackOption();
                    map[fireWorldTile] = ao;
                    ao.target = fireWorldTile;
                    RateFireLocation(poa, ao);
                }

                ao.AddMoveTarget(moveWorldTile);
            }
        }

        actor.Place(startWorldTile);
        List<AttackOption> list = new List<AttackOption>(map.Values);
        PickBestOptions(poa, list);
    }

    List<WorldTile> GetMoveOptions()
    {
        return actor.GetComponent<UnitMovement>().GetTilesInRange();
    }

    private void RateFireLocation(PlanOfAttack poa, AttackOption option)
    {
        AbilityArea area = poa.ability.GetComponent<AbilityArea>();
        List<WorldTile> tiles = area.GetTilesInArea(option.target.position);
        option.areaTargets = tiles;
        option.isCasterMatch = IsAbilityTargetMatch(poa, actor.tile);

        for (int i = 0; i < tiles.Count; ++i)
        {
            WorldTile worldTile = tiles[i];
            if (actor.tile == tiles[i] || !poa.ability.IsTarget(worldTile))
            {
                continue;
            }

            bool isMatch = IsAbilityTargetMatch(poa, worldTile);
            option.AddMark(worldTile, isMatch);
        }
    }

    private bool IsAbilityTargetMatch(PlanOfAttack poa, WorldTile worldTile)
    {
        bool isMatch = false;
        if (poa.target == Targets.Tile)
        {
            isMatch = true;
        }
        else if (poa.target != Targets.None)
        {
            Alliance other = worldTile.content.GetComponentInChildren<Alliance>();
            if (other != null && _alliance.IsMatch(other, poa.target))
            {
                isMatch = true;
            }
        }

        return isMatch;
    }

    private void PickBestOptions(PlanOfAttack poa, List<AttackOption> list)
    {
        int bestScore = 1;
        List<AttackOption> bestOptions = new List<AttackOption>();

        for (int i = 0; i < list.Count; ++i)
        {
            AttackOption option = list[i];
            int score = option.GetScore();
            if (score > bestScore)
            {
                bestScore = score;
                bestOptions.Clear();
                bestOptions.Add(option);
            }
            else if (score == bestScore)
            {
                bestOptions.Add(option);
            }
        }

        if (bestOptions.Count == 0)
        {
            poa.ability = null; // Clear ability as a sign not to perform it
            return;
        }

        List<AttackOption> finalPicks = new List<AttackOption>();
        bestScore = 0;

        for (int i = 0; i < bestOptions.Count; ++i)
        {
            AttackOption option = bestOptions[i];
            int score = option.bestScore;
            if (score > bestScore)
            {
                bestScore = score;
                finalPicks.Clear();
                finalPicks.Add(option);
            }
            else if (score == bestScore)
            {
                finalPicks.Add(option);
            }
        }

        AttackOption choice = finalPicks[new Random().Next(finalPicks.Count)];
        poa.fireLocation = choice.target.position;
        poa.moveLocation = choice.bestMoveWorldTile.position;
    }

    private void FindNearestFoe()
    {
        _neareastFoe = null;
        Board.Instance.Search(actor.tile, delegate(WorldTile arg1, WorldTile arg2)
        {
            if (_neareastFoe == null && arg2.content != null)
            {
                Alliance other = arg2.content.GetComponentInChildren<Alliance>();
                if (other != null && _alliance.IsMatch(other, Targets.Foe))
                {
                    Unit unit = other.GetComponent<Unit>();
                    UnitStats unitStats = unit.GetComponent<UnitStats>();
                    if (unitStats[UnitStatTypes.HP] > 0)
                    {
                        _neareastFoe = unit;
                        return true;
                    }
                }
            }

            return _neareastFoe == null;
        });
    }

    private void MoveTowardOpponent(PlanOfAttack poa)
    {
        List<WorldTile> moveOptions = GetMoveOptions();
        FindNearestFoe();
        if (_neareastFoe != null)
        {
            WorldTile toCheck = _neareastFoe.tile;

            while (toCheck != null)
            {
                if (moveOptions.Contains(toCheck))
                {
                    poa.moveLocation = toCheck.position;
                    return;
                }

                toCheck = toCheck.parent;
            }
        }

        poa.moveLocation = actor.tile.position;
    }
    
    private void DefaultAttackPattern (PlanOfAttack poa)
    {
        // Just get the first "Attack" ability
        poa.ability = actor.GetComponentInChildren<Ability>();
        poa.target = Targets.Foe;
    }
}
