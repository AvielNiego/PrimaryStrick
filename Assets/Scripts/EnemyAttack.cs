using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 40f;
    
    private PlayerHealth target;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    // ReSharper disable once UnusedMember.Global
    // Used as animation event
    public void AttackHitEvent()
    {
        if (target == null)
        {
            return;
        }

        target.TakeDamage(damage);

        Debug.Log("bang bang");
    }
}