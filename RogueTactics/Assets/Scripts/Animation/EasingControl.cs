using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class <c>EasingControl</c> is a Unity component script used to manage the rotation platform trap behaviour.
/// </summary>
public class EasingControl : MonoBehaviour
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public TimeType timeType = TimeType.Normal;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public EndBehaviour endBehaviour = EndBehaviour.Constant;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public LoopType loopType = LoopType.Repeat;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float startValue;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float endValue = 1.0f;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float duration = 1.0f;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public int loopCount;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Func<float, float, float, float> equation = EasingEquations.Linear;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public PlayState playState { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public PlayState previousPlayState { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public bool IsPlaying => playState.Equals(PlayState.Playing) || playState.Equals(PlayState.Reversing);

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float currentTime { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float currentValue { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float currentOffset { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public int loops { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void OnEnable()
    {
        Resume();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void OnDisable()
    {
        Pause();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler updateEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler stateChangeEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler completedEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler loopedEvent;
    
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum TimeType
    {
        Normal,
        Real,
        Fixed
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum PlayState
    {
        Stopped,
        Paused,
        Playing,
        Reversing
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum EndBehaviour
    {
        Constant,
        Reset
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum LoopType
    {
        Repeat,
        PingPong
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Play()
    {
        SetPlayState(PlayState.Playing);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Reverse()
    {
        SetPlayState(PlayState.Reversing);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Pause()
    {
        SetPlayState(PlayState.Paused);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Resume()
    {
        SetPlayState(previousPlayState);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Stop()
    {
        SetPlayState(PlayState.Stopped);
        loops = 0;
        if (endBehaviour.Equals(EndBehaviour.Reset)) SeekToBeginning();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="time">TODO: comments</param>
    public void SeekToTime(float time)
    {
        currentTime = Mathf.Clamp01(time / duration);
        var newValue = (endValue - startValue) * currentTime + startValue;
        currentOffset = newValue - currentValue;
        currentValue = newValue;

        if (updateEvent != null) updateEvent(this, System.EventArgs.Empty);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void SeekToBeginning()
    {
        SeekToTime(0.0f);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void SeekToEnd()
    {
        SeekToTime(duration);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="target">TODO: comments</param>
    private void SetPlayState(PlayState target)
    {
        if (playState.Equals(target)) return;

        previousPlayState = playState;
        playState = target;

        if (stateChangeEvent != null) stateChangeEvent(this, System.EventArgs.Empty);

        StopCoroutine("Ticker");
        if (IsPlaying) StartCoroutine("Ticker");
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private IEnumerator Ticker()
    {
        while (true)
            switch (timeType)
            {
                case TimeType.Normal:
                    yield return new WaitForEndOfFrame();
                    Tick(Time.deltaTime);
                    break;
                case TimeType.Real:
                    yield return new WaitForEndOfFrame();
                    Tick(Time.unscaledDeltaTime);
                    break;
                default: // FixedUpdate
                    yield return new WaitForEndOfFrame();
                    Tick(Time.fixedDeltaTime);
                    break;
            }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="time">TODO: comments</param>
    private void Tick(float time)
    {
        var finished = false;
        if (playState.Equals(PlayState.Playing))
        {
            currentTime = Mathf.Clamp01(currentTime + time / duration);
            finished = Mathf.Approximately(currentTime, 1.0f);
        }
        else // Reversing
        {
            currentTime = Mathf.Clamp01(currentTime - time / duration);
            finished = Mathf.Approximately(currentTime, 0.0f);
        }

        var frameValue = (endValue - startValue) * equation(0.0f, 1.0f, currentTime) + startValue;
        currentOffset = frameValue - currentValue;
        currentValue = frameValue;

        if (updateEvent != null) updateEvent(this, System.EventArgs.Empty);

        if (finished)
        {
            ++loops;
            if (loopCount < 0 || loopCount >= loops)
            {
                if (loopType.Equals(LoopType.Repeat))
                    SeekToBeginning();
                else // PingPong
                    SetPlayState(playState.Equals(PlayState.Playing) ? PlayState.Reversing : PlayState.Playing);

                if (loopedEvent != null) loopedEvent(this, System.EventArgs.Empty);
            }
            else
            {
                if (completedEvent != null) completedEvent(this, System.EventArgs.Empty);

                Stop();
            }
        }
    }
}