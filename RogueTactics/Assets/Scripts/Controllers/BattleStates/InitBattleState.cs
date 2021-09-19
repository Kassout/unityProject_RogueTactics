using System.Collections;
using Model;
using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public class InitBattleState : BattleState
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    private IEnumerator Init()
    {
        var initPosition = new Vector2(0, 0);
        SelectTile(initPosition);
        SpawnTestUnits();
        AddVictoryCondition();
        
        yield return null;
            
        this.PostNotification(BattleController.BattleBeganNotification);
        this.PostNotification(BattleController.TurnBeganNotification);
            
        owner.ChangeState<SelectUnitState>();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void SpawnTestUnits()
    {
        string[] recipes = new string[]
        {
            "Alaois",
            "Hania",
            "Kamau",
            "EnemySwordsman",
            "EnemyPikeman",
            "EnemyMonk"
        };
            
        for (int i = 0; i < recipes.Length; ++i)
        {
            int level = Random.Range(9, 12);
            GameObject instance = UnitFactory.Create(recipes[i], level);
                
            WorldTile spawnWorldTile;
            do
            {
                var spawnPos = new Vector2(Random.Range(0, 16), Random.Range(0, 16));
                spawnWorldTile = Board.GetTile(spawnPos);
            } while (spawnWorldTile.doCollide ||
                     spawnWorldTile.worldTileType.tileTypeName.Equals(WorldTileType.TileTypeEnum.Water) || spawnWorldTile.content != null);
                
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(spawnWorldTile);
            unit.Match();

            spawnWorldTile.content = instance;
                
            units.Add(unit);
        }
    }
    
    private void AddVictoryCondition()
    {
        DefeatTargetVictoryCondition vc = owner.gameObject.AddComponent<DefeatTargetVictoryCondition>();
        Unit enemy = units[units.Count - 1];
        vc.target = enemy;
        Health health = enemy.GetComponent<Health>();
        health.MinHP = 10;
    }
}