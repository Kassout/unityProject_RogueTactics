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
        RefreshPrimaryStatPanel(turn.actor.tile.position);

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
        yield return new WaitForSeconds(0.5f);
        if (null != turn.ability)
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
        turn.targets = new List<WorldTile>() { turn.targets[_index] };
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
        turn.ability = null;
        turn.targets = new List<WorldTile>();
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
        if (turn.currentDriver == Drivers.Computer || turn.targets.Count != 0)
        {
            var ar = turn.ability.GetComponent<AbilityRange>().GetTilesInRange();
            turn.targets = units.Where(unit => ar.Any(tile => tile.position.Equals(unit.tile.position)) && !unit.Equals(turn.actor)).Select(unit => unit.tile).ToList();
        }

        var allowedTargets = new List<WorldTile>();
        AbilityEffectTarget[] targeters = turn.actor.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < turn.targets.Count; ++i)
            if (IsTarget(turn.targets[i], targeters))
                allowedTargets.Add(turn.targets[i]);

        turn.targets = allowedTargets;
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
        WorldTile target = turn.targets[_index];

        string chanceAttacker = CalculateHitRate(turn.actor, target).ToString();
        string amountAttacker = EstimateAbilityDamage(target).ToString();
        hitSuccessIndicator.SetAttackerStats(chanceAttacker, amountAttacker);

        string chanceDefender = "-";
        string amountDefender = "-";
        
        if (target.content.GetComponentInChildren<AbilityRange>().range >=
            Vector3.Magnitude(turn.actor.transform.position - target.content.transform.position))
        {
            chanceDefender = CalculateHitRate(target.content.GetComponent<Unit>(), turn.actor.tile).ToString();
            amountDefender = EstimateFightBackDamage(target.content.GetComponent<Unit>(), turn.actor.tile)
                .ToString();
        }
        hitSuccessIndicator.SetDefenderStats(chanceDefender, amountDefender);
    }

    int CalculateHitRate(Unit attacker, WorldTile target)
    {
        HitRate hr = attacker.GetComponentInChildren<HitRate>();
        return hr.Calculate(target);
    }

    int EstimateAbilityDamage(WorldTile target)
    {
        BaseAbilityEffect effect = turn.ability.GetComponentInChildren<BaseAbilityEffect>();
        return effect.Predict(target);
    }
    
    int EstimateFightBackDamage(Unit attacker, WorldTile target)
    {
        BaseAbilityEffect effect = attacker.GetComponentInChildren<BaseAbilityEffect>();
        return effect.Predict(target);
    }
}
