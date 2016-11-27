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
        base.OnTriggerEnter2D(col);
        if (col.CompareTag("Player"))
        {
            PlayerMovement player = col.GetComponent<PlayerMovement>();
            Weapon made = Instantiate(wep, player.transform.position, Quaternion.identity, player.transform) as Weapon;
            ui.displayPopUpText(made.getName(), transform.position);
            player.weapons.Add(made);
            Destroy(gameObject);
        }
    }
}