using UnityEngine;
using System.Collections;

public class PlaceholderItem : Item {

    public Weapon[] weps;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        Weapon wep = weps[Random.Range(0, weps.Length)];
        popupText = wep.getName();
        base.OnTriggerEnter2D(col);
        if (col.CompareTag("Player"))
        {
            ui.displayPopUpText("placeholder", transform.position);
            PlayerMovement player = col.GetComponent<PlayerMovement>();
            Weapon made = Instantiate(wep, player.transform.position, Quaternion.identity, player.transform) as Weapon;
            player.weapons.Add(made);
            Destroy(gameObject);
        }
    }
}