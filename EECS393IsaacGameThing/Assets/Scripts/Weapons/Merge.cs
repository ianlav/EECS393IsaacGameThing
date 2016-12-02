using UnityEngine;
using System.Collections.Generic;
using System;

public class Merge : MonoBehaviour {
    Dictionary<Type[], Weapon> table;
    bool isInTestMode = true;
    PlayerMovement player;

    public SineGun sine;
    public StartGun BFG;

    //populate merge list
    public void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        table = new Dictionary<Type[], Weapon>();
        //populate with merge rules
        //addMergeRule(new[]{ typeof(StartGun) }, typeof(SpreadGun)); //<- example
        print(addMergeRule(new[] { typeof(MachineGun), typeof(SpreadGun), typeof(SineGun), typeof(BurstGun), typeof(LaserGun), typeof(StartGun) }, BFG));
        print(addMergeRule(new[] { typeof(MachineGun), typeof(SpreadGun) }, sine));
    }

    void Update()
    {
        isInTestMode = false; //if we have called update, we are in the actual game, not an editor test
    }

    public bool addMergeRule(Type[] ingredientTypes, Weapon mergedType)
    {
        if(mergedType == null)
        {
            return false;
        }
        //make sure the table has been initialized
        if (table == null)
            Start();
        //check to make sure we only add Weapons to the merge table
        for (int i=0; i < ingredientTypes.Length; i++) {
            if (!ingredientTypes[i].IsSubclassOf(typeof(Weapon)))
                return false;
        }
        if (!mergedType.GetType().IsSubclassOf(typeof(Weapon)))
            return false;
        //add to merge table
        table.Add(ingredientTypes, mergedType);
        return true;
    }

    public bool addMergeRule(Type[] ingredientTypes, Type mergedType)
    {
        //make sure the table has been initialized
        if (table == null)
            Start();
        //check to make sure we only add Weapons to the merge table
        for (int i = 0; i < ingredientTypes.Length; i++)
        {
            if (!ingredientTypes[i].IsSubclassOf(typeof(Weapon)))
                return false;
        }
        if (!mergedType.IsSubclassOf(typeof(Weapon)))
            return false;
        //add to merge table
        Weapon wep = null;
        if (mergedType.IsSubclassOf(typeof(Weapon)))
        {
            wep = (new GameObject()).AddComponent(mergedType) as Weapon;
        }
        else
        {
            return false;
        }

        table.Add(ingredientTypes, wep);
        return true;
    }

    public void clearMergeRules()
    {
        table.Clear();
    }

    //new public static string ToString(this Dictionary<Type[], Type> table)
    public string getTableString()
    {
        var enumerator = table.GetEnumerator();
        Type[] keys;
        string keystring, tablestring = "table: { ";
        Type value;
        int keynum = 0;
        while (enumerator.MoveNext()) {
            keys = enumerator.Current.Key;
            value = enumerator.Current.Value.GetType();
            keystring = "[";
            if (keys != null)
                for (int i=0; i < keys.Length; i++) {
                    keystring += keys[i];
                    if (i < keys.Length - 1)
                        keystring += ",";
                }
            keystring += "]";
            tablestring += string.Format("({0}, {1}) ", keystring, value);
            keynum++;
            if (keynum % 2 == 0)
                tablestring += "\n";
        }
        tablestring += " } len:" + table.Count;
        return tablestring;
    }

    public void printTable()
    {
        print(getTableString());
    }

    //get list of weapons after a possbile merge
    public Weapon[] mergeIfPossible(List<Weapon> listOut)
    {
        var enumerator = table.GetEnumerator();
        List<Weapon> matches = new List<Weapon>();
        //this is really inefficient, O(table<> * tablekey[] * weapons[]) with N storage as well...
        print("length: " + table.Count);
        while (enumerator.MoveNext()) {
            List<Weapon> listIn = new List<Weapon>(listOut);
            print("hi");
            Type[] ingredientTypes = (Type[])enumerator.Current.Key.Clone();
            for (int i=0; i < ingredientTypes.Length; i++) {
                for (int w=0; w < listIn.Count; w++) {
                    if (listIn[w] != null && listIn[w].GetType() == ingredientTypes[i]) {
                        matches.Add(listIn[w]);
                        listIn[w] = null; //don't double-spend
                        break;
                    }
                }
            }
            print("hiya");
            if (matches.Count == ingredientTypes.Length) {
                print("here");
                if (isInTestMode)
                {
                    listOut.Add((Weapon)(new GameObject()).AddComponent(enumerator.Current.Value.GetType()));
                }
                else
                {
                    listOut.Add(Instantiate(enumerator.Current.Value, player.transform.position, Quaternion.identity, player.transform) as Weapon);
                }
                for (int i=0; i < matches.Count; i++)
                    listOut.Remove(matches[i]);
                break;
            }
            else if (matches.Count > 0)
                matches.Clear();
            print("hey");
        }
        return listOut.ToArray();
    }

     public Weapon[] mergeIfPossible(Weapon[] listOut)
    {
        return mergeIfPossible(new List<Weapon>(listOut));
    }
}
