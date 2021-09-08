using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAllEnemiesVictoryCondition : BaseVictoryCondition
{
    protected override void CheckForGameOver()
    {
        base.CheckForGameOver();
        if (Victory == Alliances.None && PartyDefeated(Alliances.Enemy))
        {
            Victory = Alliances.Hero;
        }
    }
}
