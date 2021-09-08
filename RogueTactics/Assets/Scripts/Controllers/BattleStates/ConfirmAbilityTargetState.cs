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
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.TileDefinition.position);

        if (turn.targets.Count > 0)
        {
            if (_driver.Current == Drivers.Human)
            {
                hitSuccessIndicator.Show();
            }
            SetTarget(0);
        }

        if (_driver.Current == Drivers.Computer)
        {
            StartCoroutine(ComputerDisplayAbilitySelection());
        }
        
        if (_driver.Current == Drivers.Human)
        {
            inputManager.Cursor.Selection.performed += OnSelection;
            inputManager.Cursor.Selection.Enable();
        
            Cursor.visible = false;
        }
    }

    private IEnumerator ComputerDisplayAbilitySelection()
    {
        owner.battleMessageController.Display(turn.ability.name);
        yield return new WaitForSeconds(2f);
        owner.ChangeState<PerformAbilityState>();
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
        hitSuccessIndicator.Hide();
        
        inputManager.Cursor.Selection.performed -= OnSelection;
        inputManager.Cursor.Selection.Disable();
        
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

        inputManager.Cursor.Selection.Disable();

        yield return new WaitForSeconds(0.2f);
        
        inputManager.Cursor.Selection.Enable();
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
        AbilityEffectTarget[] targeters = turn.actor.GetComponentsInChildren<AbilityEffectTarget>();
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
            UpdateHitSuccessIndicator();
        }
    }

    void UpdateHitSuccessIndicator()
    {
        TileDefinitionData target = turn.targets[_index];

        int chanceAttacker = CalculateHitRate(turn.actor, target);
        int amoutAttacker = EstimateDamage(turn.actor, target);
        hitSuccessIndicator.SetAttackerStats(chanceAttacker, amoutAttacker);
        
        int chanceDefender = CalculateHitRate(target.content.GetComponent<Unit>(), turn.actor.TileDefinition);
        int amoutDefender = EstimateDamage(target.content.GetComponent<Unit>(), turn.actor.TileDefinition);
        hitSuccessIndicator.SetDefenderStats(chanceDefender, amoutDefender);
    }

    int CalculateHitRate(Unit attacker, TileDefinitionData target)
    {
        HitRate hr = attacker.GetComponentInChildren<HitRate>();
        return hr.Calculate(target);
    }

    int EstimateDamage(Unit attacker, TileDefinitionData target)
    {
        BaseAbilityEffect effect = attacker.GetComponentInChildren<BaseAbilityEffect>();
        return effect.Predict(target);
    }
}
