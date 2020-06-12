using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float projectileSpeed = 20.0f;
   // public Debuff debuff;
    public bool explodes;
    public int numChains;
    public float chainRange;
    public float splashRadius;
    public float damage;

    public void Setup(List<AbilityMods> mods)
    {
        foreach(AbilityMods n in mods)
        {
            numChains += n.numChains;
            chainRange = Mathf.Max(chainRange, n.chainRange);
        }
    }

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
        // Check if Chains
        if (!Chain())
        {
            // Done w/ Projectile
            Destroy();
        }
    }

    // Custom Sort by Distance
     public int DistanceSort(Collider _a, Collider _b)
     {
         return (transform.position - _a.transform.position).sqrMagnitude.
             CompareTo((transform.position - _b.transform.position).sqrMagnitude);
     }

    public bool Chain()
    {
        if (numChains > 0)
        {
            // Decrement Number of Remaining Chains
            numChains--;
            // Get nearby Objects - Sort by Distance
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, chainRange);
            Array.Sort(hitObjects, DistanceSort);
            // Iterate Through Nearest Objects
            foreach (Collider collider in hitObjects)
            {
                // Check for different Enemy
                if (collider.tag == "Enemy" && collider.gameObject != target.gameObject)
                {
                    // Damage Enemy
                    Seek(collider.gameObject);
                    // Found Target
                    return true;
                }
            }
        }
        // No Nearby Target
        return false;
    }

    // For AoE Damage
    void Explode()
    {
        // Get Nearby Enemies
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, splashRadius);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                // Damage Enemy
                Damage(collider.transform);
            }
        }
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