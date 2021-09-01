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
            //System.Type[] components = new System.Type[]{typeof(WalkMovement)};
            for (var i = 0; i < 3; ++i)
            {
                var instance = Instantiate(owner.heroPrefab);

                TileDefinitionData spawnTile;

                do
                {
                    var spawnPos = new Vector2(Random.Range(0, 16), Random.Range(0, 16));
                    spawnTile = Board.GetTile(spawnPos);
                } while (spawnTile.doCollide ||
                         spawnTile.tileType.tileTypeName.Equals(TileTypeObject.TileTypeEnum.Water));

                var unit = instance.GetComponent<Unit>();
                unit.Place(spawnTile);
                unit.Match();

                if (instance.AddComponent(typeof(WalkMovement)) is UnitMovement m) m.range = 5;

                units.Add(unit);
            }
        }
    }
}