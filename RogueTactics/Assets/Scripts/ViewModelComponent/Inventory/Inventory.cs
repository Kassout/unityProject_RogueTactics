using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<IStorable> objects = new List<IStorable>(5);

    public void StoreObject(GameObject storableObject)
    {
        Instantiate(storableObject, transform, true);
        objects.Add(storableObject.GetComponent<IStorable>());
    }

    public void UnloadObject(GameObject storableObject)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).gameObject.Equals(storableObject))
            {
                Destroy(transform.GetChild(i));
            }
        }

        objects.Remove(storableObject.GetComponent<IStorable>());
    }
}
