using System.Collections;
using Model;
using UnityEngine;
using ViewModelComponent;

namespace BattleStates
{
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
            yield return null;
            owner.ChangeState<CutSceneState>();
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private void SpawnTestUnits()
        {
            string[] classes = new string[]{"Swordsman", "Pikeman", "Monk"};
            for (int i = 0; i < classes.Length; ++i)
            {
                GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
                
                Stats s = instance.AddComponent<Stats>();
                s[StatTypes.LVL] = 1;
                
                GameObject jobPrefab = Resources.Load<GameObject>( "Classes/" + classes[i] );
                
                GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
                
                jobInstance.transform.SetParent(instance.transform);
                Class classObject = jobInstance.GetComponent<Class>();
                classObject.Promote();
                classObject.LoadDefaultStats();
                
                TileDefinitionData spawnTile;

                do
                {
                    var spawnPos = new Vector2(Random.Range(0, 16), Random.Range(0, 16));
                    spawnTile = Board.GetTile(spawnPos);
                } while (spawnTile.doCollide ||
                         spawnTile.tileType.tileTypeName.Equals(TileTypeObject.TileTypeEnum.Water));
                
                Unit unit = instance.GetComponent<Unit>();
                unit.Place(spawnTile);
                unit.Match();
                
                instance.AddComponent<WalkMovement>();
                
                units.Add(unit);
            }
        }
    }
}