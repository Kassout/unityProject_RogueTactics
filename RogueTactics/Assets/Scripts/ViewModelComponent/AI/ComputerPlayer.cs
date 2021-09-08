using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    private BattleController bc;
    Unit actor
    {
        get { return bc.turn.actor; }
    }

    private void Awake()
    {
        bc = GetComponent<BattleController>();
    }

    public PlanOfAttack Evaluate()
    {
        // Create and fill out a plan of attack
        PlanOfAttack poa = new PlanOfAttack();
        
        // Step 1: Decide what ability to use
        AttackPattern pattern = actor.GetComponentInChildren<AttackPattern>();
        if (pattern)
        {
            pattern.Pick(poa);
        }
        else
        {
            //DefaultAttackPattern(poa);
        }
        
        // Step 2: Determine where to move and aim to best use the ability
        //PlaceHolderCode(poa);
        
        // Return the complete plan
        return poa;
    }
}
