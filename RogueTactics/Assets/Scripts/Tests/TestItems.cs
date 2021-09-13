using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItems : MonoBehaviour 
{
    #region Fields
    List<GameObject> inventory = new List<GameObject>();
    List<GameObject> combatants = new List<GameObject>();
    #endregion
  
    #region MonoBehaviour
  
    void Start () 
    {
        CreateItems();
        CreateCombatants();
        StartCoroutine(SimulateBattle());
    }
  
    void OnEnable ()
    {
        this.AddObserver(OnEquippedItem, Equipment.EquippedNotification);
        this.AddObserver(OnUnEquippedItem, Equipment.UnEquippedNotification);
    }
  
    void OnDisable ()
    {
        this.RemoveObserver(OnEquippedItem, Equipment.EquippedNotification);
        this.RemoveObserver(OnUnEquippedItem, Equipment.UnEquippedNotification);
    }
  
    #endregion
  
    #region Notification Handlers
  
    void OnEquippedItem (object sender, object args)
    {
        Equipment eq = sender as Equipment;
        Equippable item = args as Equippable;
        inventory.Remove(item.gameObject);
        string message = string.Format("{0} equipped {1}", eq.name, item.name);
        Debug.Log(message);
    }
  
    void OnUnEquippedItem (object sender, object args)
    {
        Equipment eq = sender as Equipment;
        Equippable item = args as Equippable;
        inventory.Add(item.gameObject);
        string message = string.Format("{0} un-equipped {1}", eq.name, item.name);
        Debug.Log(message);
    }
  
    #endregion
  
    #region Factory
  
    GameObject CreateItem (string title, UnitStatTypes type, int amount)
    {
        GameObject item = new GameObject(title);
        UnitStatModifierFeature smf = item.AddComponent<UnitStatModifierFeature>();
        smf.type = type;
        smf.amount = amount;
        return item;
    }
  
    GameObject CreateConsumableItem (string title, UnitStatTypes type, int amount)
    {
        GameObject item = CreateItem(title, type, amount);
        item.AddComponent<Consumable>();
        return item;
    }
  
    GameObject CreateEquippableItem (string title, UnitStatTypes type, int amount, EquipSlots slot)
    {
        GameObject item = CreateItem(title, type, amount);
        Equippable equip = item.AddComponent<Equippable>();
        equip.defaultSlots = slot;
        return item;
    }
  
    GameObject CreateHero ()
    {
        GameObject actor = CreateActor("Hero");
        actor.AddComponent<Equipment>();
        return actor;
    }
  
    GameObject CreateActor (string title)
    {
        GameObject actor = new GameObject(title);
        UnitStats s = actor.AddComponent<UnitStats>();
        s[UnitStatTypes.HP] = s[UnitStatTypes.MHP] = UnityEngine.Random.Range(500, 1000);
        s[UnitStatTypes.STR] = UnityEngine.Random.Range(30, 50);
        s[UnitStatTypes.DEF] = UnityEngine.Random.Range(30, 50);
        return actor;
    }
    
    #endregion
    
    #region Private
    
    void CreateItems ()
    {
        inventory.Add( CreateConsumableItem("Health Potion", UnitStatTypes.HP, 300) );
        inventory.Add( CreateConsumableItem("Bomb", UnitStatTypes.HP, -150) );
        inventory.Add( CreateEquippableItem("Sword", UnitStatTypes.STR, 10, EquipSlots.Weapon) );
        inventory.Add( CreateEquippableItem("Broad Sword", UnitStatTypes.STR, 15, (EquipSlots.Weapon) ) );
        inventory.Add( CreateEquippableItem("Shield", UnitStatTypes.DEF, 10, EquipSlots.Accessory) );
    }
  
    void CreateCombatants ()
    {
        combatants.Add( CreateHero() );
        combatants.Add( CreateActor("Monster") );
    }
    
    IEnumerator SimulateBattle ()
    {
        while (VictoryCheck() == false)
        {
            LogCombatants();
            HeroTurn();
            EnemyTurn();
            yield return new WaitForSeconds(1);
        }
        LogCombatants();
        Debug.Log("Battle Completed");
    }
    
    void HeroTurn ()
    {
        int rnd = UnityEngine.Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                Attack(combatants[0], combatants[1]);
                break;
            default:
                UseInventory();
                break;
        }
    }
    
    void EnemyTurn ()
    {
        Attack(combatants[1], combatants[0]);
    }
    
    void Attack (GameObject attacker, GameObject defender)
    {
        UnitStats s1 = attacker.GetComponent<UnitStats>();
        UnitStats s2 = defender.GetComponent<UnitStats>();
        int damage = Mathf.FloorToInt((s1[UnitStatTypes.STR] * 4 - s2[UnitStatTypes.DEF] * 2) * UnityEngine.Random.Range(0.9f, 1.1f));
        s2[UnitStatTypes.HP] -= damage;
        string message = string.Format("{0} hits {1} for {2} damage!", attacker.name, defender.name, damage);
        Debug.Log(message);
    }
    
    void UseInventory ()
    {
        int rnd = UnityEngine.Random.Range(0, inventory.Count);
        GameObject item = inventory[rnd];
        if (item.GetComponent<Consumable>() != null)
            ConsumeItem(item);
        else
            EquipItem(item);
    }
    
    void ConsumeItem (GameObject item)
    {
        inventory.Remove(item);
        // This is dummy code - a user would know how to use an item and who to target with it
        UnitStatModifierFeature smf = item.GetComponent<UnitStatModifierFeature>();
        if (smf.amount > 0)
        {
            item.GetComponent<Consumable>().Consume( combatants[0] );
            Debug.Log("Ah... a potion!");
        }
        else
        {
            item.GetComponent<Consumable>().Consume( combatants[1] );
            Debug.Log("Take this you stupid monster!");
        }
    }
    
    void EquipItem (GameObject item)
    {
        Debug.Log("Perhaps this will help...");
        Equippable toEquip = item.GetComponent<Equippable>();
        Equipment equipment = combatants[0].GetComponent<Equipment>();
        equipment.Equip (toEquip, toEquip.defaultSlots );
    }
    
    bool VictoryCheck ()
    {
        for (int i = 0; i < 2; ++i)
        {
            UnitStats s = combatants[i].GetComponent<UnitStats>();
            if (s[UnitStatTypes.HP] <= 0)
                return true;
        }
        return false;
    }
    
    void LogCombatants ()
    {
        Debug.Log("============");
        for (int i = 0; i < 2; ++i)
            LogToConsole( combatants[i] );
        Debug.Log("============");
    }
    
    void LogToConsole (GameObject actor)
    {
        UnitStats s = actor.GetComponent<UnitStats>();
        string message = string.Format("Name:{0} HP:{1}/{2} ATK:{3} DEF:{4}", actor.name, s[UnitStatTypes.HP], s[UnitStatTypes.MHP], s[UnitStatTypes.STR], s[UnitStatTypes.DEF]);
        Debug.Log( message );
    }
    
    #endregion
}