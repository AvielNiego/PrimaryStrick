using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private List<AmmoSlot> ammoSlots;

    [Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public bool HasEnoughAmmo(AmmoType ammoType)
    {
        return ammoSlots.Where(a => a.ammoType == ammoType).Any(a => a.ammoAmount > 0);
    }

    public void UseAmmo(AmmoType ammoType)
    {
        ammoSlots.Where(a => a.ammoType == ammoType).ToList().ForEach(a => a.ammoAmount--);
    }

    public void AddAmmo(AmmoType ammoType, int ammoAmount)
    {
        ammoSlots.Where(a => a.ammoType == ammoType).ToList().ForEach(a => a.ammoAmount += ammoAmount);
    }
}