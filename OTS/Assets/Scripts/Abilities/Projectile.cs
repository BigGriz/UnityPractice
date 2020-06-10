using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float projectileSpeed = 20.0f;
   // public Debuff debuff;
    public bool explodes;
    public float splashRadius;
    public float damage;

    // Set Target to Seek
    public void Seek(GameObject _target)
    {
        // Bad Pass
        if (_target == null)
        {
            Destroy();
        }
        // Set Target to Seek
        else
        {
            target = _target.transform;
        }
    }
    // Done with this Projectile
    private void Destroy()
    {
        // Cleanup
        Destroy(gameObject);    
    }

    // Movement + Hit Reg
    void Update()
    {
        // Target Dead
        if (target == null)
        {
            // May instead have bullet travel to targets last location
            Destroy();
            return;
        }
        // Target still Alive
        else
        {
            transform.LookAt(target.transform);

            // Get Direction & Find Movement
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = projectileSpeed * Time.deltaTime;

            // If distance to target is less than how far we move this frame
            if (dir.magnitude <= distanceThisFrame)
            {
                // Reg Hit
                HitTarget();
                return;
            }
            // Else Move
            else
            {
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            }
        }       
    }

    // Hit Reg
    void HitTarget()
    {
        // Check if Explodes
        if (explodes)
        {
            Explode();
        }
        // Else just damage Target
        else
        {
            Damage(target);
        }
        // Done w/ Projectile
        Destroy();
    }

    // For AoE Damage
    void Explode()
    {
        // Get Nearby Enemies
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, splashRadius);
        foreach (Collider2D collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                // Damage Enemy
                Damage(collider.transform);
            }
        }
        // Done w/ Projectile
        Destroy();
    }

    // Damage
    void Damage(Transform _enemy)
    {
        Enemy enemy = _enemy.GetComponent<Enemy>();

        // Ensure Enemy is Still Alive
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            // Check if Debuff to Apply
            /*if (debuff)
            {
                Debuff tempDebuff = Instantiate(debuff);
                enemy.ApplyDebuff(tempDebuff);
            }*/
        }
    }
}