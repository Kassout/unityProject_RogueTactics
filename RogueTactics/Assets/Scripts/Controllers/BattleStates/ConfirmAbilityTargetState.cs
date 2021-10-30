using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfirmAbilityTargetState : BattleState
{
    private List<WorldTile> _tiles;
    private AbilityArea _aa;
    private int _index = 0;
    
    public override void Enter()
    {
        base.Enter();
        
        FindTargets();
        RefreshPrimaryStatPanel(Turn.actor.tile.position);

        if (Turn.targets.Count > 0)
        {
            if (driver.Current == Drivers.Human)
            {
                HitSuccessIndicator.Show();
            }
            SetTarget(0);
        }

        if (driver.Current == Drivers.Computer)
        {
            StartCoroutine(ComputerDisplayAbilitySelection());
        }
        
        if (driver.Current == Drivers.Human)
        {
            inputManager.Cursor.Selection.performed += OnSelection;
            inputManager.Cursor.Selection.Enable();
        
            Cursor.visible = false;
        }
    }

    private IEnumerator ComputerDisplayAbilitySelection()
    {
        owner.battleMessageController.Display(Turn.ability.name);
        yield return new WaitForSeconds(0.5f);
        if (null != Turn.ability)
        {
            owner.ChangeState<PerformAbilityState>();
        }
        else
        {
            owner.ChangeState<TurnManagerState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        StatPanelController.HidePrimary();
        StatPanelController.HideSecondary();
        HitSuccessIndicator.Hide();
        
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
        Turn.targets = new List<WorldTile>() { Turn.targets[_index] };
        if (Turn.targets.Count > 0)
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
        Turn.ability = null;
        Turn.targets = new List<WorldTile>();
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
        if (Turn.currentDriver == Drivers.Computer || Turn.targets.Count != 0)
        {
            var ar = Turn.ability.GetComponent<AbilityRange>().GetTilesInRange();
            Turn.targets = Units.Where(unit => ar.Any(tile => tile.position.Equals(unit.tile.position)) && !unit.Equals(Turn.actor)).Select(unit => unit.tile).ToList();
        }

        var allowedTargets = new List<WorldTile>();
        AbilityEffectTarget[] targeters = Turn.actor.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < Turn.targets.Count; ++i)
            if (IsTarget(Turn.targets[i], targeters))
                allowedTargets.Add(Turn.targets[i]);

        Turn.targets = allowedTargets;
    }
  
    bool IsTarget (WorldTile worldTile, AbilityEffectTarget[] list)
    {
        for (int i = 0; i < list.Length; ++i)
            if (list[i].IsTarget(worldTile))
                return true;
    
        return false;
    }
    
    void SetTarget (int target)
    {
        _index = target;
        if (_index < 0)
            _index = Turn.targets.Count - 1;
        if (_index >= Turn.targets.Count)
            _index = 0;
        if (Turn.targets.Count > 0)
        {
            RefreshSecondaryStatPanel(Turn.targets[_index].position);
            TileSelectionCursor.localPosition = Turn.targets[_index].position;
            UpdateHitSuccessIndicator();
        }
    }

    void UpdateHitSuccessIndicator()
    {
        WorldTile target = Turn.targets[_index];

        string chanceAttacker = CalculateHitRate(Turn.actor, target).ToString();
        string amountAttacker = EstimateAbilityDamage(target).ToString();
        HitSuccessIndicator.SetAttackerStats(chanceAttacker, amountAttacker);

        string chanceDefender = "-";
        string amountDefender = "-";
        
        if (target.content.GetComponentInChildren<AbilityRange>().range >=
            Vector3.Magnitude(Turn.actor.transform.position - target.content.transform.position))
        {
            chanceDefender = CalculateHitRate(target.content.GetComponent<Unit>(), Turn.actor.tile).ToString();
            amountDefender = EstimateFightBackDamage(target.content.GetComponent<Unit>(), Turn.actor.tile)
                .ToString();
        }
        HitSuccessIndicator.SetDefenderStats(chanceDefender, amountDefender);
    }

    int CalculateHitRate(Unit attacker, WorldTile target)
    {
        HitRate hr = attacker.GetComponentInChildren<HitRate>();
        return hr.Calculate(target);
    }

    int EstimateAbilityDamage(WorldTile target)
    {
        BaseAbilityEffect effect = Turn.ability.GetComponentInChildren<BaseAbilityEffect>();
        return effect.Predict(target);
    }
    
    int EstimateFightBackDamage(Unit attacker, WorldTile target)
    {
        BaseAbilityEffect effect = attacker.GetComponentInChildren<BaseAbilityEffect>();
        return effect.Predict(target);
    }
}
