using System;

/// <summary>
/// TODO: comments
/// </summary>
/// <typeparam name="T">TODO: comments </typeparam>
public class InfoEventArgs<T> : EventArgs
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    public T info;

    /// <summary>
    /// Default constructor method.
    /// </summary>
    public InfoEventArgs()
    {
        info = default(T);
    }

    /// <summary>
    /// Constructor method with parameter.
    /// </summary>
    /// <param name="info">TODO: comments</param>
    public InfoEventArgs(T info)
    {
        this.info = info;
    }
}
