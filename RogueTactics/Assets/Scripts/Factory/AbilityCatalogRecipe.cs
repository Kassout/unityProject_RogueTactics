using System;
using System.Collections;
using System.Collections.Generic;
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
