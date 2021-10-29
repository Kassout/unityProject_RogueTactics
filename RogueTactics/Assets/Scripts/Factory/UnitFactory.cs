using UnityEngine;

public static class UnitFactory
{
    public static GameObject Create(string name, int level)
    {
        UnitRecipe recipe = Resources.Load<UnitRecipe>("UnitRecipes/" + name);

        if (recipe == null)
        {
            Debug.LogError("No Unit Recipe for name: " + name);
            return null;
        }

        return Create(recipe, level);
    }

    public static GameObject Create(UnitRecipe recipe, int level)
    {
        GameObject obj = InstantiatePrefab("Units/" + recipe.model);
        
        obj.name = recipe.name;
        
        obj.AddComponent<Unit>();
        AddStats(obj);
        AddLocomotion(obj, recipe.locomotion);

        obj.AddComponent<Status>();
        obj.AddComponent<Equipment>();
        AddClass(obj, recipe.unitClass);
        AddRank(obj, level);
        
        obj.AddComponent<Health>();
        obj.AddComponent<Mana>();
        AddAttack(obj, recipe.attack);
        AddAbilityCatalog(obj, recipe.abilityCatalog);
        AddAlliance(obj, recipe.alliance);
        AddAttackPattern(obj, recipe.strategy);
        return obj;
    }

    private static GameObject InstantiatePrefab(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        if (prefab == null)
        {
            Debug.LogError("No Prefab for name: " + name);
            return new GameObject(name);
        }

        GameObject instance = GameObject.Instantiate(prefab);
        return instance;
    }

    private static void AddStats(GameObject obj)
    {
        UnitStats s = obj.AddComponent<UnitStats>();
        s.SetValue(UnitStatTypes.LVL, 1, false);
        WeaponStats ws = obj.AddComponent<WeaponStats>();
    }

    private static void AddClass(GameObject obj, string name)
    {
        GameObject instance = InstantiatePrefab("Classes/" + name);
        instance.transform.SetParent(obj.transform);
        Class unitClass = instance.GetComponent<Class>();
        unitClass.Promote();
        unitClass.LoadDefaultStats();
    }

    private static void AddLocomotion(GameObject obj, Locomotions type)
    {
        switch (type)
        {
            case Locomotions.Walk:
                obj.AddComponent<WalkMovement>();
                break;
            default:
                obj.AddComponent<WalkMovement>();
                break;
        }
    }

    private static void AddAlliance(GameObject obj, Alliances type)
    {
        Alliance alliance = obj.AddComponent<Alliance>();
        alliance.type = type;
    }

    private static void AddRank(GameObject obj, int level)
    {
        Rank rank = obj.AddComponent<Rank>();
        rank.Init(level);
    }

    private static void AddAttack(GameObject obj, string name)
    {
        GameObject instance = InstantiatePrefab("Abilities/" + name);
        instance.name = "Attack";
        instance.transform.SetParent(obj.transform);
    }

    private static void AddAbilityCatalog(GameObject obj, string name)
    {
        GameObject main = new GameObject("Ability Catalog");
        main.transform.SetParent(obj.transform);
        main.AddComponent<AbilityCatalog>();
        AbilityCatalogRecipe recipe = Resources.Load<AbilityCatalogRecipe>("AbilityCatalogRecipes/" + name);
        if (recipe == null)
        {
            Debug.LogError("No Ability Catalog Recipe Found: " + name);
            return;
        }
        for (int i = 0; i < recipe.categories.Length; ++i)
        {
            GameObject category = new GameObject( recipe.categories[i].name );
            category.transform.SetParent(main.transform);
            for (int j = 0; j < recipe.categories[i].entries.Length; ++j)
            {
                string abilityName = string.Format("Abilities/{0}/{1}", recipe.categories[i].name, recipe.categories[i].entries[j]);
                GameObject ability = InstantiatePrefab(abilityName);
                ability.name = recipe.categories[i].entries[j];
                ability.transform.SetParent(category.transform);
            }
        }
    }

    private static void AddAttackPattern(GameObject obj, string name)
    {
        Driver driver = obj.AddComponent<Driver>();
        if (string.IsNullOrEmpty(name))
        {
            driver.normal = Drivers.Human;
        }
        else
        {
            driver.normal = Drivers.Computer;
            GameObject instance = InstantiatePrefab("Attack Pattern/" + name);
            instance.transform.SetParent(obj.transform);
        }
    }
}
