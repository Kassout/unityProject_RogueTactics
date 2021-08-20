using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorBoundsBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();
    }
}
