using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        SpawnTestUnits(); 
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }

    void SpawnTestUnits ()
    {
        System.Type[] components = new System.Type[]{typeof(WalkMovement)};
        for (int i = 0; i < 3; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            Vector2 spawnPos = new Vector2(Random.Range(0, 16), Random.Range(0, 16));
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(spawnPos);
            UnitMovement m = instance.AddComponent(typeof(WalkMovement)) as UnitMovement;
            m.range = 5;
            m.jumpHeight = 1;
        }
    }
}
