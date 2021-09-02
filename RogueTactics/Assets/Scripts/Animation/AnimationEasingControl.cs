using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class <c>EasingControl</c> is a Unity component script used to manage animation easing behaviour.
/// </summary>
public class AnimationEasingControl : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>timeType</c> represents the time pass category of our animation regarding the Unity Engine clock options.
    /// </summary>
    public TimeType timeType = TimeType.Normal;

    /// <summary>
    /// Instance variable <c>endBehaviour</c> represents the animation behaviour to adopt at the end of the clip.
    /// </summary>
    public EndBehaviour endBehaviour = EndBehaviour.Constant;

    /// <summary>
    /// Instance variable <c>loopType</c> represents the looping behaviour to adopt for the animation.
    /// </summary>
    public LoopType loopType = LoopType.Repeat;

    /// <summary>
    /// Instance variable <c>startValue</c> represents the time value (expressed as a percentage of the clip duration,
    /// between 0 and 1) at which the clip of the animation should start from.
    /// </summary>
    public float startValue;

    /// <summary>
    /// Instance variable <c>endValue</c> represents the time value (expressed as a percentage of the clip duration,
    /// between 0 and 1) at which the clip of the animation should end to.
    /// </summary>
    public float endValue = 1.0f;

    /// <summary>
    /// Instance variable <c>duration</c> represents the duration time value of the animation.
    /// </summary>
    public float duration = 1.0f;

    /// <summary>
    /// Instance variable <c>loopCount</c> represents times the animation has get played and repeat.
    /// </summary>
    public int loopCount;

    /// <summary>
    /// Instance variable <c>equation</c> represents the easing equation the animation should follow while playing.
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

    #endregion

    #region Public Methods

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler UpdateEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler StateChangeEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler CompletedEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler LoopedEvent;
    
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

        if (UpdateEvent != null) UpdateEvent(this, System.EventArgs.Empty);
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

    #endregion

    #region MonoBehaviour

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

    #endregion
    
    #region Private Methods

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="target">TODO: comments</param>
    private void SetPlayState(PlayState target)
    {
        if (playState.Equals(target)) return;

        previousPlayState = playState;
        playState = target;

        if (StateChangeEvent != null) StateChangeEvent(this, System.EventArgs.Empty);

        StopCoroutine(nameof(Ticker));
        if (IsPlaying) StartCoroutine(nameof(Ticker));
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private IEnumerator Ticker()
    {
        while (true)
        {
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
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="time">TODO: comments</param>
    private void Tick(float time)
    {
        bool finished;
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

        if (UpdateEvent != null) UpdateEvent(this, System.EventArgs.Empty);

        if (finished)
        {
            ++loops;
            if (loopCount < 0 || loopCount >= loops)
            {
                if (loopType.Equals(LoopType.Repeat))
                    SeekToBeginning();
                else // PingPong
                    SetPlayState(playState.Equals(PlayState.Playing) ? PlayState.Reversing : PlayState.Playing);

                if (LoopedEvent != null) LoopedEvent(this, System.EventArgs.Empty);
            }
            else
            {
                if (CompletedEvent != null) CompletedEvent(this, System.EventArgs.Empty);

                Stop();
            }
        }
    }

    #endregion
}