using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfirmAbilityTargetState : BattleState
{
    private List<TileDefinitionData> _tiles;
    private AbilityArea _aa;
    private int _index = 0;

    public override void Enter()
    {
        base.Enter();
        //_aa = turn.ability.GetComponent<AbilityArea>();
        //_tiles = _aa.GetTilesInArea(tileSelectionCursor.position);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.TileDefinition.position);
        SetTarget(0);
        
        _inputManager.Cursor.Selection.performed += OnSelection;
        _inputManager.Cursor.Selection.Enable();
        
        Cursor.visible = false;
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
        
        _inputManager.Cursor.Selection.performed -= OnSelection;
        _inputManager.Cursor.Selection.Disable();
        
        Cursor.visible = true;
    }

    protected void OnSelection(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>().normalized * Time.fixedDeltaTime;

        if (context.performed)
        {
            StartCoroutine(nameof(ChangeSelection), mouseDelta.y);
        }
    }

    private IEnumerator ChangeSelection(float axis)
    {
        if (axis > 0)
        {
            SetTarget(_index + 1);
        } 
        else if (axis < 0)
        {
            SetTarget(_index - 1);
        }

        _inputManager.Cursor.Selection.Disable();

        yield return new WaitForSeconds(0.2f);
        
        _inputManager.Cursor.Selection.Enable();
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        if (turn.targets.Count > 0)
        {
            owner.ChangeState<PerformAbilityState>();
        }
        else
        {
            owner.ChangeState<AbilityTargetState>();
        }
    }

    protected override void OnCancel(InputAction.CallbackContext context)
    {
        if (owner.GetPreviousState().Equals("MoveTargetState"))
        {
            owner.ChangeState<MoveTargetState>();
        }
        else
        {
            owner.ChangeState<CommandSelectionState>();
        }

    }

    void FindTargets ()
    {
        var allowedTargets = new List<TileDefinitionData>();
        AbilityEffectTarget[] targeters = turn.ability.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < turn.targets.Count; ++i)
            if (IsTarget(turn.targets[i], targeters))
                allowedTargets.Add(turn.targets[i]);

        turn.targets = allowedTargets;
    }
  
    bool IsTarget (TileDefinitionData tile, AbilityEffectTarget[] list)
    {
        for (int i = 0; i < list.Length; ++i)
            if (list[i].IsTarget(tile))
                return true;
    
        return false;
    }
    
    void SetTarget (int target)
    {
        _index = target;
        if (_index < 0)
            _index = turn.targets.Count - 1;
        if (_index >= turn.targets.Count)
            _index = 0;
        if (turn.targets.Count > 0)
        {
            RefreshSecondaryStatPanel(turn.targets[_index].position);
            tileSelectionCursor.localPosition = turn.targets[_index].position;
        }
    }
}
