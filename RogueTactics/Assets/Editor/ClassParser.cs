using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Object = UnityEngine.Object;

public static class ClassParser
{
    [MenuItem("Pre Production/Parse Classes")]
    public static void Parse()
    {
        CreateDirectories();
        ParseStartingStats();
        ParseGrowthStats();
        ParseStatCaps();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Classes"))
            AssetDatabase.CreateFolder("Assets/Resources", "Classes");
    }
    
    static void ParseStartingStats()
    {
        string readPath = $"{Application.dataPath}/Settings/ClassStartingStats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ParseStartingStats(readText[i]);
    }
    
    static void ParseStartingStats(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);

        ParseModel(obj);

        Class classObject = obj.GetComponent<Class>();
        for (int i = 1; i < Class.StatOrder.Length + 1; ++i)
            classObject.baseStats[i - 1] = Convert.ToInt32(elements[i]);

        UnitStatModifierFeature move = GetFeature(obj, UnitStatTypes.MOV);
        move.amount = Convert.ToInt32(elements[12]);
    }

    static void ParseModel(GameObject obj)
    {
        Class classObject = obj.GetComponent<Class>();

        string modelPath = $"Assets/Resources/Classes/Sprites/{obj.name}.png";
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(modelPath);

        string animatorControllerPath = $"Assets/Resources/Classes/AnimatorControllers/{obj.name}.overrideController";
        AnimatorOverrideController animatorController =
            AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(animatorControllerPath);

        classObject.classModel = sprite;
        classObject.classAnimator = animatorController;
    }

    static void ParseGrowthStats()
    {
        string readPath = $"{Application.dataPath}/Settings/ClassGrowthStats.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ParseGrowthStats(readText[i]);
    }

    static void ParseGrowthStats(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        Class classObject = obj.GetComponent<Class>();
        for (int i = 1; i < elements.Length; ++i)
            classObject.growStats[i - 1] = Convert.ToSingle(elements[i]);
    }

    static void ParseStatCaps()
    {
        string readPath = $"{Application.dataPath}/Settings/ClassStatCaps.csv";
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ParseStatCaps(readText[i]);
    }

    static void ParseStatCaps(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        Class classObject = obj.GetComponent<Class>();
        for (int i = 1; i < elements.Length; ++i)
            classObject.statCaps[i - 1] = Convert.ToInt32(elements[i]);
    }

    static UnitStatModifierFeature GetFeature(GameObject obj, UnitStatTypes type)
    {
        UnitStatModifierFeature[] smf = obj.GetComponents<UnitStatModifierFeature>();
        foreach (var t in smf)
        {
            if (t.type == type)
                return t;
        }

        UnitStatModifierFeature feature = obj.AddComponent<UnitStatModifierFeature>();
        feature.type = type;
        return feature;
    }

    static GameObject GetOrCreate(string className)
    {
        string fullPath = $"Assets/Resources/Classes/{className}.prefab";
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
            obj = Create(fullPath);
        return obj;
    }

    static GameObject Create(string fullPath)
    {
        GameObject instance = new GameObject("temp");
        instance.AddComponent<Class>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        Object.DestroyImmediate(instance);
        return prefab;
    }
}