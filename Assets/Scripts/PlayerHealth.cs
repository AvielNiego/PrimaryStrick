using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    private DeathHandler _deathHandler;

    private void Start()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }

    public void TakeDamage(float damage)
    {
        hitPoints = Mathf.Max(hitPoints - damage, 0);
        if (hitPoints <= 0)
        {
            _deathHandler.HandleDeath();
        }
    }
}