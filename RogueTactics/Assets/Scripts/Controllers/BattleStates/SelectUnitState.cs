using System.Collections;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleStates
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public class SelectUnitState : BattleState
    {
        public override void Enter()
        {
            base.Enter();
            if (units.All(unit => unit.hasEndTurn))
            {
                SetupNextTurn();
            }
        }

        private void SetupNextTurn()
        {
            foreach (var unit in units)
            {
                unit.hasEndTurn = false;
                unit.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            tileSelectionCursor.position =
                new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnInteraction(InputAction.CallbackContext context)
        {
            GameObject content = null;

            var hit = Physics2D.Raycast(tileSelectionCursor.position, Vector2.zero);
            if (hit.transform != null)
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);
                content = hit.transform.gameObject;
            }
            else
            {
                Debug.Log("No hit");
            }

            if (content != null && !content.GetComponent<Unit>().hasEndTurn)
            {
                StartCoroutine("ChangeCurrentUnit", content.GetComponent<Unit>());
            }
        }

        private IEnumerator ChangeCurrentUnit(Unit target)
        {
            turn.Change(units[units.FindIndex(unit => unit.Equals(target))]);
            yield return null;
            owner.ChangeState<MoveTargetState>();
        }
    }
}