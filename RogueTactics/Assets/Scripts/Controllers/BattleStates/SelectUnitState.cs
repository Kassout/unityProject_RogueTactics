using Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleStates
{
    public class SelectUnitState : BattleState
    {
        protected override void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            tileSelectionCursor.position = new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
        }
    
        protected override void OnInteraction(InputAction.CallbackContext context)
        {
            GameObject content = null;

            RaycastHit2D hit = Physics2D.Raycast(tileSelectionCursor.position, Vector2.zero);
            if (hit.transform != null) 
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);
                content = hit.transform.gameObject;
            } else {
                Debug.Log("No hit");
            }
            
            if (content != null)
            {
                owner.currentUnit = content.GetComponent<Unit>();
                owner.ChangeState<MoveTargetState>();
            }
        }
    }
}
