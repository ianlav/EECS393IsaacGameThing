using UnityEngine;
using System.Collections.Generic;
using System;

public class Merge : MonoBehaviour {
    new static GameObject gameObject;
    static Dictionary<Type[], Type> table;

    //populate merge list
    void Start()
    {
        table = new Dictionary<Type[], Type>();
        //populate with merge rules
        table.Add(new[]{ typeof(StartGun) }, typeof(SpreadGun));
    }

    //get list of weapons after a possbile merge
    public Weapon[] mergeIfPossible(params Weapon[] weapons)
    {
        var enumerator = table.GetEnumerator();
        List<Weapon> listOut = new List<Weapon>(weapons);
        List<Weapon> matches = new List<Weapon>();
        bool exit = false;
        do {
            Type[] ingredientTypes = enumerator.Current.Key;
            foreach (Type ingredientType in ingredientTypes) {
                foreach (Weapon wep in weapons) {
                    if (wep.GetType() == ingredientType) {
                        matches.Add(wep);
                        break;
                    }
                }
            }
            if (matches.Count == ingredientTypes.Length) {
                listOut.Add((Weapon)Activator.CreateInstance(enumerator.Current.Value));
                foreach (Weapon match in matches)
                    listOut.Remove(match);
                break;
            }
            else if (matches.Count > 0)
                matches.Clear();

        } while (!exit && enumerator.MoveNext());

        return listOut.ToArray();
    }
}
