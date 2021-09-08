
public class FixedAbilityPicker : BaseAbilityPicker
{
    public Targets target;
    public string ability;

    public override void Pick(PlanOfAttack plan)
    {
        plan.target = target;
        plan.ability = Find(ability);

        if (plan.ability == null)
        {
            plan.ability = Default();
            plan.target = Targets.Foe;
        }
    }
}
