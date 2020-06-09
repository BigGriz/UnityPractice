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

    /* 
    //If not Chaining - or at end of Chain
    if (!chains || numChains == 0)
    {
        Destroy();
    }
        Else find new Chain target
    else
    {
            Get Nearby
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, chainRange);

        if (hitObjects.Length == 0)
        {
            Destroy();
        }
        else
        {
            float closestTarget = Mathf.Infinity;
            Enemy tempEnemy = null;

                Loop through all Enemies within range, find closest
            foreach (Collider2D colliders in hitObjects)
            {
                    Make sure not counting self
                if (colliders.transform != target && colliders.gameObject.tag == "Wolf")
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, colliders.transform.position);
                    if (distanceToEnemy<closestTarget)
                    {
                        tempEnemy = colliders.gameObject.GetComponent<Enemy>();
                        closestTarget = distanceToEnemy;
                    }
                }
            }
                No targets within range
            if (tempEnemy == null)
            {
                Destroy();
            }
            else
            {
                numChains--;
                Seek(tempEnemy);
            }
        }
    }
    */