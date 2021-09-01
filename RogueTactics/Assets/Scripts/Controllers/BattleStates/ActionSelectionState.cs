using System.Collections.Generic;
using BattleStates;

public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    private readonly string[] _whiteMagicOptions = new string[] { "Cure", "Raise", "Holy" };
    private readonly string[] _blackMagicOptions = new string[] { "Fire", "Ice", "Lightning" };
    
    protected override void LoadMenu()
    {
        menuOptions ??= new List<string>(3);
        SetOptions(category == 0 ? _whiteMagicOptions : _blackMagicOptions);
        abilityMenuPanelController.Show(menuOptions);
    }

    protected override void Confirm ()
    {
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        owner.ChangeState<SelectUnitState>();
    }
    
    protected override void Cancel ()
    {
        owner.ChangeState<CategorySelectionState>();
    }
    
    void SetOptions (string[] options)
    {
        menuOptions.Clear();
        foreach (var t in options)
            menuOptions.Add(t);
    }
}
