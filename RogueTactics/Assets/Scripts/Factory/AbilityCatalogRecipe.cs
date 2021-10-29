using System;
using UnityEngine;

public class AbilityCatalogRecipe : ScriptableObject
{
    [Serializable]
    public class Category
    {
        public string name;
        public string[] entries;
    }

    public Category[] categories;
}
