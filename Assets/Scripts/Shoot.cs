using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private bool addBulletSpred = true;
    [SerializeField] private Vector3 bulletSpreadVariant = new(0.1f, 0.1f, 0.1f);

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float shootDelay = 0.5f;

    [SerializeField] private LayerMask mask;
    [SerializeField] private float maxFireDistance = 100f;
    [SerializeField] private int damage = 1;

    private float bulletSpeed = 100;

    //[SerializeField] private ParticleSystem shootingSystem;

    private float lastShootTime;

    private void FixedUpdate()
    {
        DoShoot();
    }

    public void DoShoot()
    {
        if (lastShootTime + shootDelay > Time.time)
        {
            Vector3 direction = GetDirection();

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, maxFireDistance, mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit.point));

                lastShootTime = Time.time;

                if(hit.collider.CompareTag("Enemy")) 
                { 
                    PlaneAI plane = hit.collider.GetComponent<PlaneAI>();
                    plane.TakeDamage(damage);
                }
            }
            else
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, bulletSpawnPoint.position + GetDirection() * maxFireDistance));

                lastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (addBulletSpred)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariant.x, bulletSpreadVariant.x),
                Random.Range(-bulletSpreadVariant.y, bulletSpreadVariant.y),
                Random.Range(-bulletSpreadVariant.z, bulletSpreadVariant.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 HitPoint)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = HitPoint;
        Destroy(trail.gameObject, trail.time);
        // <= part sys
    }
}
