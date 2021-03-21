using System;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int ammoAmount = 10;
    private void OnTriggerEnter(Collider other)
    {
        var otherAmmo = other.GetComponent<Ammo>();
        if (otherAmmo == null)
        {
            return;
        }

        otherAmmo.AddAmmo(ammoType, ammoAmount);
        Destroy(gameObject);
    }
}