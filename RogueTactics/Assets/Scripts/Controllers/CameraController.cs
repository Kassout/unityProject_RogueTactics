using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float speed = 3f;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Transform follow;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private Transform _transform;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void Awake()
    {
        _transform = transform;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void Update()
    {
        if (follow) _transform.position = Vector3.Lerp(_transform.position, new Vector3(follow.position.x, follow.position.y, _transform.position.z), speed * Time.fixedDeltaTime);
    }
}