using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float weaponDamage = 30f;

    [SerializeField] private ParticleSystem muzzleVFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Transform parent;

    [SerializeField] private Ammo ammoSlot;

    [SerializeField] private float shootTimeout = .2f;

    [SerializeField] private AmmoType ammoType;

    private bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        if (!ammoSlot.HasEnoughAmmo(ammoType))
        {
            yield break;
        }
        
        muzzleVFX.Play();
        ammoSlot.UseAmmo(ammoType);

        HitTarget();

        canShoot = false;
        yield return new WaitForSeconds(shootTimeout);
        canShoot = true;
    }

    private void HitTarget()
    {
        var cameraTransform = FPCamera.transform;
        var isHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, range);

        if (!isHit)
        {
            return;
        }

        CreateHitImpact(hit);

        var target = hit.transform.GetComponent<EnemyHealth>();
        if (target == null)
        {
            return;
        }

        target.TakeDamage(weaponDamage);
    }

    private void CreateHitImpact(RaycastHit hitInfo)
    {
        var impact = Instantiate(hitVFX, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), parent);
        Destroy(impact, .1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
