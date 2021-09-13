using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    static readonly Dictionary<WeaponStatTypes, string> WillChangeNotifications = new Dictionary<WeaponStatTypes, string>();
    
    static readonly Dictionary<WeaponStatTypes, string> DidChangeNotifications = new Dictionary<WeaponStatTypes, string>();
    
    private readonly int[] _data = new int[(int)WeaponStatTypes.Count];
    
    public int this[WeaponStatTypes s]
    {
        get { return _data[(int)s]; }
        set { SetValue(s, value, true); }
    }

    public static string WillChangeNotification(WeaponStatTypes type)
    {
        if (!WillChangeNotifications.ContainsKey(type))
            WillChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        return WillChangeNotifications[type];
    }
    
    public static string DidChangeNotification (WeaponStatTypes type)
    {
        if (!DidChangeNotifications.ContainsKey(type))
            DidChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        return DidChangeNotifications[type];
    }

    public void SetValue(WeaponStatTypes type, int value, bool allowExceptions)
    {
        int oldValue = this[type];
        if (oldValue == value)
        {
            return;
        }

        if (allowExceptions)
        {
            // Allow exceptions to the rule here
            ValueChangeException exc = new ValueChangeException(oldValue, value);
            
            // The notification is unique per stat type
            this.PostNotification(WillChangeNotification(type), exc);
            
            // Did anything modify the value ?
            value = Mathf.FloorToInt(exc.GetModifiedValue());
            
            // Did something nullify the change?
            if (exc.toggle == false || value == oldValue)
            {
                return;
            }
        }
        
        _data[(int)type] = value;
        this.PostNotification(DidChangeNotification(type), oldValue);
    }
}