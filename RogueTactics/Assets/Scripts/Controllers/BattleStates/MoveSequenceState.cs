using System.Collections;
using UnityEngine.InputSystem;
using ViewModelComponent;

namespace BattleStates
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public class MoveSequenceState : BattleState
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        public override void Enter()
        {
            base.Enter();
            StartCoroutine("Sequence");
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <returns>TODO: comments</returns>
        private IEnumerator Sequence()
        {
            var m = owner.turn.actor.GetComponent<UnitMovement>();
            yield return StartCoroutine(m.Traverse(currentSelectedTile));
            owner.ChangeState<CommandSelectionState>();
        }
    }
}