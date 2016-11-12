using UnityEngine;
using System.Collections.Generic;
using System;

public class Merge : MonoBehaviour {
    Dictionary<Type[], Type> table;

    //populate merge list
    public void Start()
    {
        table = new Dictionary<Type[], Type>();
        //populate with merge rules
        addMergeRule(new[] { typeof(SpreadGun), typeof(MachineGun) }, typeof(MachineSpreadGun));
        addMergeRule(new[] { typeof(SpreadGun), typeof(BurstGun) }, typeof(BurstSpreadGun));
        addMergeRule(new[] { typeof(SpreadGun), typeof(SpreadGun) }, typeof(SequentialSpreadGun));

    }

    public bool addMergeRule(Type[] ingredientTypes, Type mergedType)
    {
        //make sure the table has been initialized
        if (table == null)
            Start();
        //check to make sure we only add Weapons to the merge table
        for (int i=0; i < ingredientTypes.Length; i++) {
            if (!ingredientTypes[i].IsSubclassOf(typeof(Weapon)))
                return false;
        }
        if (!mergedType.IsSubclassOf(typeof(Weapon)))
            return false;
        //add to merge table
        table.Add(ingredientTypes, mergedType);
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
            value = enumerator.Current.Value;
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
    public Weapon[] mergeIfPossible(Weapon[] weapons)
    {
        var enumerator = table.GetEnumerator();
        List<Weapon> listIn = new List<Weapon>(weapons);
        List<Weapon> listOut = new List<Weapon>(weapons);
        List<Weapon> matches = new List<Weapon>();
        //this is really inefficient, O(table<> * tablekey[] * weapons[]) with N storage as well...
        while (enumerator.MoveNext()) {
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
            if (matches.Count == ingredientTypes.Length) {
                listOut.Add((Weapon)gameObject.AddComponent(enumerator.Current.Value));
                for (int i=0; i < matches.Count; i++)
                    listOut.Remove(matches[i]);
                break;
            }
            else if (matches.Count > 0)
                matches.Clear();
        }
        return listOut.ToArray();
    }
}
