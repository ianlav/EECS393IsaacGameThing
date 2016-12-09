using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponPickup : Item {

    public Weapon[] weps;
    private Merge merger;

    protected override void Start()
    {
        base.Start();
        merger = FindObjectOfType<Merge>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        Weapon wep = weps[Random.Range(0, weps.Length)];
        base.OnTriggerEnter2D(col);
        if (col.CompareTag("Player"))
        {
            PlayerMovement player = col.GetComponent<PlayerMovement>();
            Weapon made = Instantiate(wep, player.transform.position, Quaternion.identity, player.transform) as Weapon;
            ui.displayPopUpText(made.getName(), transform.position);
            player.weapons.Add(made);

            string wepstring = "weapons start: {";
            foreach (Weapon w in player.weapons)
                wepstring += w.getName() + ",";
            wepstring += "}";
            print(wepstring);
            merger.printTable();
            
            List<Weapon> result = merger.mergeIfPossible(player.weapons);
            if (result.Count != player.weapons.Count) {
                Vector3 textPos = transform.position;
                textPos.y -= 1f;
                ui.displayPopUpText(merger.getLastMergeMessage(), textPos);
            }
            player.weapons = result;

            wepstring = "weapons end: {";
            foreach (Weapon w in player.weapons)
                wepstring += w.getName() + ",";
            wepstring += "}";
            print(wepstring);

            Destroy(gameObject);
        }
    }
}