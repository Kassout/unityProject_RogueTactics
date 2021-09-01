public abstract class ValueModifier : Modifier
{
    protected ValueModifier(int sortOrder) : base(sortOrder) {}

    public abstract float Modify(float value);
}
