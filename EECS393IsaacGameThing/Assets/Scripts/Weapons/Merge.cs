using UnityEngine;
using System.Collections.Generic;
using System;

public class Merge : MonoBehaviour {
    Dictionary<Type[], Weapon> table;
    public bool isInTestMode = true;
    PlayerMovement player;

    public BurstGun burstGun;
    public BurstSpreadGun burstSpreadGun;
    public LaserGun laserGun;
    public MachineGun machineGun;
    public MachineSpreadGun machineSpreadGun;
    public SequentialSpreadGun sequentialSpreadGun;
    public SineGun sineGun;
    public SpreadGun spreadGun;
    public StartGun startGun;
    public StartGun BFG;

    //populate merge list
    public void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        table = new Dictionary<Type[], Weapon>();
        //populate with merge rules
        //addMergeRule(new[]{ typeof(StartGun) }, typeof(SpreadGun)); //<- example
        print(addMergeRule(new[] { typeof(MachineGun), typeof(SpreadGun), typeof(SineGun), typeof(BurstGun), typeof(LaserGun), typeof(StartGun) }, BFG));
        //print(addMergeRule(new[] { typeof(MachineGun), typeof(SpreadGun) }, sine));
        print(addMergeRule(new[] { typeof(MachineGun), typeof(SpreadGun) }, sineGun));

        print(addMergeRule(new[] { typeof(SpreadGun), typeof(MachineGun) }, machineSpreadGun));
        print(addMergeRule(new[] { typeof(SpreadGun), typeof(BurstGun) }, burstSpreadGun));
        print(addMergeRule(new[] { typeof(SpreadGun), typeof(SpreadGun) }, sequentialSpreadGun));
    }

    void Update()
    {
        isInTestMode = false; //if we have called update, we are in the actual game, not an editor test
    }

    public bool addMergeRule(Type[] ingredientTypes, Weapon mergedWeapon)
    {
        if(mergedWeapon == null)
            return false;
        //make sure the table has been initialized
        if (table == null)
            Start();
        //check to make sure we only add Weapons to the merge table
        for (int i=0; i < ingredientTypes.Length; i++) {
            if (!ingredientTypes[i].IsSubclassOf(typeof(Weapon)))
                return false;
        }
        if (!mergedWeapon.GetType().IsSubclassOf(typeof(Weapon)))
            return false;
        //add to merge table
        table.Add(ingredientTypes, mergedWeapon);
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
            if (isInTestMode)
                wep = (new GameObject()).AddComponent(mergedType) as Weapon;
            else
                //wep = Instantiate((GameObject)Resources.Load("Prefabs/Weapons/Guns/" + mergedType.ToString()));
                return false;
        }
        else
            return false;

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
        Type value;
        string keystring, tablestring = "table: { ";
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
            //if (keynum % 2 == 0)
            //    tablestring += "\n";
        }
        tablestring += " } len:" + table.Count;
        return tablestring;
    }

    public void printTable()
    {
        print(getTableString());
    }

    //get list of weapons after a possbile merge
    public List<Weapon> mergeIfPossible(List<Weapon> weapons)
    {
        var enumerator = table.GetEnumerator();
        List<Weapon> matches = new List<Weapon>();
        List<Weapon> listOut = new List<Weapon>(weapons.ToArray());
        List<Transform> playerChildTransforms = getPlayerChildTransforms();
        //this is really inefficient, O(table<> * tablekey[] * weapons[]) with N storage as well...
        while (enumerator.MoveNext()) {
            List<Weapon> listIn = new List<Weapon>(listOut);
            Type[] ingredientTypes = (Type[])enumerator.Current.Key.Clone();
            for (int i=0; i < ingredientTypes.Length; i++) {
                for (int w=0; w < listIn.Count; w++) {
                    if (listIn[w] != null && listIn[w].GetType() == ingredientTypes[i]) {
                        matches.Add(listIn[w]);
                        //print("match: " + listIn[w].GetType() + "  towards: " + enumerator.Current.Value.getName());
                        listIn[w] = null; //don't double-spend
                        break;
                    }
                }
            }
            if (matches.Count == ingredientTypes.Length) {
                //remove matched weapon from game
                for (int i = 0; i < matches.Count; i++)
                {
                    int index = listOut.IndexOf(matches[i]);
                    //first, destroy the game object in-game
                    Transform child;
                    Weapon w;
                    for(int c=0; c < playerChildTransforms.Count; c++)
                    {
                        child = playerChildTransforms[c];
                        w = child.GetComponent<Weapon>();
                        if (w != null)
                        {
                            //print("checking to destroy  " + w.getName() + " at " + c + "   trying for " + matches[i].getName());
                            if (w.getName() == matches[i].getName())
                            {
                                //print("destroying child wep  " + w.getName() + " at " + c + " of " + playerWeaponTransforms.Count);
                                //one of these, or maybe a combination of all of these, works to successfully destroy the weapon
                                Destroy(w);
                                Destroy(child.gameObject);
                                Destroy(child.transform);
                                Destroy(child);
                                //don't remove all of them
                                playerChildTransforms.RemoveAt(c);
                                break;
                            }
                        }
                    }
                    //remove the reference in the player's weapon list
                    listOut.RemoveAt(index);
                }
                //add weapon to player's arsenal
                if (isInTestMode)
                    listOut.Add((Weapon)(new GameObject()).AddComponent(enumerator.Current.Value.GetType()));
                else
                    listOut.Add(Instantiate(enumerator.Current.Value, player.transform.position, Quaternion.identity, player.transform) as Weapon);
                break;
            }
            else if (matches.Count > 0)
                matches.Clear();
        }
        return listOut;
    }

    public List<Transform> getPlayerChildTransforms()
    {
        List<Transform> transforms = new List<Transform>();
        //int cc = player.transform.childCount;
        int cc = player.weapons.Count;
        while(player.transform.childCount != cc)
        { /* wait for objects to be instantiated */ }
        for (int c = 0; c < cc; c++)
        {
            Transform child = player.transform.GetChild(c);
            transforms.Add(child);
        }
        return transforms;
    }

    public Weapon[] mergeIfPossible(Weapon[] listOut)
    {
        return mergeIfPossible(new List<Weapon>(listOut)).ToArray();
    }
}
