using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : MonoBehaviour 
{
    public const string CanPerformCheck = "Ability.CanPerformCheck";
    public const string FailedNotification = "Ability.FailedNotification";
    public const string DidPerformNotification = "Ability.DidPerformNotification";

    public bool CanPerform ()
    {
        BaseException exc = new BaseException(true);
        this.PostNotification(CanPerformCheck, exc);
        return exc.toggle;
    }

    public void Perform (List<WorldTile> targets)
    {
        if (!CanPerform())
        {
            this.PostNotification(FailedNotification);
            return;
        }

        for (int i = 0; i < targets.Count; ++i)
            Perform(targets[i]);

        this.PostNotification(DidPerformNotification);
    }

    void Perform (WorldTile target)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            BaseAbilityEffect effect = child.GetComponent<BaseAbilityEffect>();
            effect.Apply(target);
        }
    }

    public bool IsTarget(WorldTile worldTile)
    {
        Transform obj = transform;
        for (int i = 0; i < obj.childCount; ++i)
        {
            AbilityEffectTarget targeter = obj.GetChild(i).GetComponent<AbilityEffectTarget>();
            if (targeter.IsTarget(worldTile))
            {
                return true;
            }
        }

        return false;
    }
}