using UnityEngine;
using System.Collections;

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
            merger.mergeIfPossible(player.weapons);
            //print(wepArray);
            Destroy(gameObject);
        }
    }
}