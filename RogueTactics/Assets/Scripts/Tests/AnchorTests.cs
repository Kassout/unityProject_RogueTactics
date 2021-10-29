using UnityEngine;
using System.Collections;

public class AnchorTests : MonoBehaviour 
{
    [SerializeField] bool animated;
    [SerializeField] float delay = 0.5f;
    IEnumerator Start ()
    {
        LayoutAnchor anchor = GetComponent<LayoutAnchor>();
        while (true)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    TextAnchor a1 = (TextAnchor)i;
                    TextAnchor a2 = (TextAnchor)j;
                    Debug.Log(string.Format("A1:{0}   A2:{1}", a1, a2));
                    if (animated)
                    {
                        Tweener t = anchor.MoveToAnchorPosition( a1, a2, Vector2.zero );
                        while (t != null)
                            yield return null;
                    }
                    else
                    {
                        anchor.SnapToAnchorPosition(a1, a2, Vector2.zero);
                    }
                    yield return new WaitForSeconds(delay);
                }
            }
        }
    }
}