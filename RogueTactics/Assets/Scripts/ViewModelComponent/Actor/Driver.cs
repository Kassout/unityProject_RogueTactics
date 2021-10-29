using UnityEngine;

public class Driver : MonoBehaviour
{
    public Drivers normal;
    public Drivers special;

    public Drivers Current
    {
        get
        {
            return special != Drivers.None ? special : normal;
        }
    }
}
