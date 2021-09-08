using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchException : BaseException
{
    public readonly Unit attacker;
    public readonly Unit target;
    
    public MatchException(Unit attacker, Unit target) : base(false)
    {
        this.attacker = attacker;
        this.target = target;
    }
}
