using System.Collections;
using Model;
using UnityEngine;
using ViewModelComponent;

namespace BattleStates
{
    public class InitBattleState : BattleState
    {
        public override void Enter()
        {
            base.Enter();
            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            Vector2 initPosition = new Vector2(0, 0);
            SelectTile(initPosition);
            SpawnTestUnits(); 
            yield return null;
            owner.ChangeState<SelectUnitState>();
        }

        void SpawnTestUnits ()
        {
            //System.Type[] components = new System.Type[]{typeof(WalkMovement)};
            for (int i = 0; i < 3; ++i)
            {
                GameObject instance = Instantiate(owner.heroPrefab);

                TileDefinitionData spawnTile;
            
                do
                {
                    Vector2 spawnPos = new Vector2(Random.Range(0, 16), Random.Range(0, 16));
                    spawnTile = Board.GetTile(spawnPos);
                } while (spawnTile.doCollide || spawnTile.tileType.tileTypeName.Equals(TileTypeObject.TileTypeEnum.Water));

                Unit unit = instance.GetComponent<Unit>();
                unit.Place(spawnTile);
                unit.Match();

                if (instance.AddComponent(typeof(WalkMovement)) is UnitMovement m)
                {
                    m.range = 5;
                }
            }
        }
    }
}
