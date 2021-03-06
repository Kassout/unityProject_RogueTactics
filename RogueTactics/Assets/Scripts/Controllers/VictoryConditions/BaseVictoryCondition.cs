using UnityEngine;

public abstract class BaseVictoryCondition : MonoBehaviour
{
    public Alliances Victory { get; protected set; } = Alliances.None;

    protected BattleController bc;

    protected virtual void Awake()
    {
        bc = GetComponent<BattleController>();
    }

    protected virtual void OnEnable()
    {
        this.AddObserver(OnHPDidChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.HP));
    }

    protected virtual void OnDisable()
    {
        this.RemoveObserver(OnHPDidChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.HP));
    }

    protected virtual void OnHPDidChangeNotification(object sender, object args)
    {
        CheckForGameOver();
    }

    protected virtual bool IsDefeated(Unit unit)
    {
        Health health = unit.GetComponent<Health>();
        if (health)
        {
            return health.MinHP == health.HP;
        }

        UnitStats unitStats = unit.GetComponent<UnitStats>();
        return unitStats[UnitStatTypes.HP] == 0;
    }

    protected virtual bool PartyDefeated(Alliances type)
    {
        for (int i = 0; i < bc.units.Count; ++i)
        {
            Alliance a = bc.units[i].GetComponent<Alliance>();
            if (a == null)
            {
                continue;
            }

            if (a.type == type && !IsDefeated(bc.units[i]))
            {
                return false;
            }
        }

        return true;
    }

    protected virtual void CheckForGameOver()
    {
        if (PartyDefeated(Alliances.Hero))
        {
            Victory = Alliances.Enemy;
        }
    }
}