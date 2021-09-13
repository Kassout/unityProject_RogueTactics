using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Object = UnityEngine.Object;

public static class WeaponParser
{
    [MenuItem("Pre Production/Parse Weapons")]
    public static void Parse()
    {
        CreateDirectories();
        ParseWeaponStats();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Weapons"))
            AssetDatabase.CreateFolder("Assets/Resources", "Weapons");
    }
    
    static void ParseWeaponStats()
    {
        string readPath = $"{Application.dataPath}/Settings/WeaponStats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ParseWeaponStats(readText[i]);
    }
    
    static void ParseWeaponStats(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0], elements[9]);

        ParseModel(obj);

        Weapon classObject = obj.GetComponent<Weapon>();
        
        classObject.defaultSlots = EquipSlots.Weapon;
        classObject.weaponCat = (Weapons) Enum.Parse(typeof(Weapons), Convert.ToString(elements[1]));
        classObject.weaponLevel = Convert.ToString(elements[2]);
        
        for (int i = 3; i - 4 < Weapon.StatOrder.Length - 1; ++i)
            classObject.baseStats[i - 3] = Convert.ToInt32(elements[i]);
        
        classObject.weaponDurability = Convert.ToInt32(elements[8]);
    }

    static void InstantiateDamageAbility(GameObject weaponObject, string damageType)
    {
        weaponObject.AddComponent<Ability>();
        weaponObject.AddComponent<WeaponAbilityPower>();
        weaponObject.AddComponent<WeaponAbilityRange>();
        weaponObject.AddComponent<UnitAbilityArea>();

        GameObject childObj = weaponObject.transform.GetChild(0).gameObject;
        childObj.AddComponent<AbilityTypeHitRate>();
        childObj.AddComponent<EnemyAbilityEffectTarget>();

        if (damageType == "PHY")
        {
            childObj.AddComponent<PhysicalDamageAbilityEffect>();
        }
        else
        {
            childObj.AddComponent<MagicalDamageAbilityEffect>();
        }
    }
    
    static void ParseModel(GameObject obj)
    {
        Weapon classObject = obj.GetComponent<Weapon>();

        string modelPath = $"Assets/Resources/Weapons/Sprites/{obj.name}.png";
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(modelPath);

        classObject.weaponModel = sprite;
    }
    
    static GameObject GetOrCreate(string weaponName, string damageType)
    {
        string fullPath = $"Assets/Resources/Weapons/{weaponName}.prefab";
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
            obj = Create(fullPath, damageType);
        return obj;
    }
    
    static GameObject Create(string fullPath, string damageType)
    {
        GameObject instance = new GameObject("temp");
        instance.AddComponent<Weapon>();
        
        GameObject childInstance = new GameObject("Damage");
        childInstance.transform.SetParent(instance.transform);
        
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        Object.DestroyImmediate(instance);
        InstantiateDamageAbility(prefab, damageType);
        return prefab;
    }
}
