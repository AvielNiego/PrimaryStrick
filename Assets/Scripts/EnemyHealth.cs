using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken", SendMessageOptions.DontRequireReceiver);
        hitPoints = Mathf.Max(hitPoints - damage, 0);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}