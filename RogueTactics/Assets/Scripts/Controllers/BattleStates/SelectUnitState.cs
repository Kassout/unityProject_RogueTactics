using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit) 
        {
            Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            content = hitInfo.transform.gameObject;
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
