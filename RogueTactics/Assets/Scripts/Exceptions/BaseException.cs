public class BaseException
{
    public bool toggle { get; private set; }

    private readonly bool _defaultToggle;

    public BaseException(bool defaultToggle)
    {
        this._defaultToggle = defaultToggle;
        toggle = defaultToggle;
    }

    public void FlipToggle()
    {
        toggle = !_defaultToggle;
    }
}
