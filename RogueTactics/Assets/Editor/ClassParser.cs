using UnityEngine;
using UnityEditor;
using System;
using System.Globalization;
using System.IO;
using UnityEditor.Animations;

public static class ClassParser 
{
    [MenuItem("Pre Production/Parse Classes")]
    public static void Parse()
    {
        CreateDirectories ();
        ParseStartingStats ();
        ParseGrowthStats ();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    static void CreateDirectories ()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Classes"))
            AssetDatabase.CreateFolder("Assets/Resources", "Classes");
    }

    static void ParseModel(GameObject obj)
    {
        Class classObject = obj.GetComponent<Class>();

        string modelPath = string.Format("Assets/Resources/Classes/Sprites/{0}.png", obj.name);
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(modelPath);
        
        string animatorControllerPath = string.Format("Assets/Resources/Classes/AnimatorControllers/{0}.overrideController", obj.name);
        AnimatorOverrideController animatorController =
            AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(animatorControllerPath);
        
        classObject.classModel = sprite;
        classObject.classAnimator = animatorController;
    }
    
    static void ParseStartingStats ()
    {
        string readPath = string.Format("{0}/Settings/ClassStartingStats.csv", Application.dataPath);
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ParseStartingStats(readText[i]);
    }
    
    static void ParseStartingStats (string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        
        ParseModel(obj);
        
        Class classObject = obj.GetComponent<Class>();
        for (int i = 1; i < Class.statOrder.Length + 1; ++i)
            classObject.baseStats[i-1] = Convert.ToInt32(elements[i]);
        
        StatModifierFeature evade = GetFeature (obj, StatTypes.EVD);
        evade.amount = Convert.ToInt32(elements[8]);
        
        StatModifierFeature tenacity = GetFeature (obj, StatTypes.TEN);
        tenacity.amount = Convert.ToInt32(elements[9]);
        
        StatModifierFeature move = GetFeature (obj, StatTypes.MOV);
        move.amount = Convert.ToInt32(elements[10]);
    }
    
    static void ParseGrowthStats ()
    {
        string readPath = string.Format("{0}/Settings/ClassGrowthStats.csv", Application.dataPath);
        string[] readText = File.ReadAllLines(readPath);
        for (int i = 1; i < readText.Length; ++i)
            ParseGrowthStats(readText[i]);
    }
    
    static void ParseGrowthStats (string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        Class classObject = obj.GetComponent<Class>();
        for (int i = 1; i < elements.Length; ++i)
            classObject.growStats[i-1] = Convert.ToSingle(elements[i], CultureInfo.InvariantCulture);
    }
    
    static StatModifierFeature GetFeature (GameObject obj, StatTypes type)
    {
        StatModifierFeature[] smf = obj.GetComponents<StatModifierFeature>();
        for (int i = 0; i < smf.Length; ++i)
        {
            if (smf[i].type == type)
                return smf[i];
        }
        StatModifierFeature feature = obj.AddComponent<StatModifierFeature>();
        feature.type = type;
        return feature;
    }
    
    static GameObject GetOrCreate (string jobName)
    {
        string fullPath = string.Format("Assets/Resources/Classes/{0}.prefab", jobName);
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        if (obj == null)
            obj = Create(fullPath);
        return obj;
    }
    
    static GameObject Create (string fullPath)
    {
        GameObject instance = new GameObject ("temp");
        instance.AddComponent<Class>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        GameObject.DestroyImmediate(instance);
        return prefab;
    }
}