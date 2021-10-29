using UnityEngine;

public class AbilityMagicCost : MonoBehaviour
{
    #region Fields

    public int amount;
    private Ability owner;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        owner = GetComponent<Ability>();
    }

    private void OnEnable()
    {
        this.AddObserver(OnCanPerformCheck, Ability.CanPerformCheck, owner);
        this.AddObserver(OnDidPerformNotification, Ability.DidPerformNotification, owner);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnCanPerformCheck, Ability.CanPerformCheck, owner);
        this.RemoveObserver(OnDidPerformNotification, Ability.DidPerformNotification, owner);
    }

    #endregion

    #region Notification Handlers

    private void OnCanPerformCheck(object sender, object args)
    {
        UnitStats s = GetComponentInParent<UnitStats>();
        if (s[UnitStatTypes.MP] < amount)
        {
            BaseException exc = (BaseException)args;
            exc.FlipToggle();
        }
    }

    private void OnDidPerformNotification(object sender, object args)
    {
        UnitStats s = GetComponentInParent<UnitStats>();
        s[UnitStatTypes.MP] -= amount;
    }

    #endregion
}
