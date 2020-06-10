using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    [Header("Setup")]
    [HideInInspector] public float targetRange;
    private FollowCamera followCam;
    public Color color;   
    [Header("Current Target")]
    public GameObject target;
    [HideInInspector] public List<GameObject> enemies;
    // Targeting Range
    private SphereCollider sphereCollider;
    private TargetHighlight targetGraphic;

    #region Setup
    private void Awake()
    {   
        // Setup
        sphereCollider = GetComponent<SphereCollider>();
        targetGraphic = GetComponentInChildren<TargetHighlight>();
    }
    private void Start()
    {
        followCam = FollowCamera.instance;
        // Update When Gear Change in Future
        UpdateRange();
    }
    #endregion Setup

    private void Update()
    {
        
        // Check for Targets
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetTarget();
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Enemy>())
                {
                    targetGraphic.NewTarget(hit.collider.gameObject);
                    GameEvents.instance.OnGetTarget(hit.collider.gameObject.GetComponent<Enemy>());
                }
            }
        }
        // Set to Match Targeting
        target = targetGraphic.target;
    }

    // Get Target if Possible
    void GetTarget()
    {
        // Check Enemies Exist in Range
        if (enemies.Count > 0)
        {
            // Iterate Through Enemies
            for (int i = 0; i < enemies.Count; ++i)
            {
                // Check not on Current Target
                if (targetGraphic.target != enemies[i])
                {
                    if (enemies[i].GetComponent<Enemy>().isVisible)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(followCam.transform.position, enemies[i].transform.position - followCam.transform.position, out hit, Mathf.Infinity))
                        {
                            if (hit.collider.gameObject == enemies[i].gameObject)
                            {
                                // Set to Target
                                targetGraphic.NewTarget(enemies[i]);
                                target = targetGraphic.target;
                                GameEvents.instance.OnGetTarget(target.GetComponent<Enemy>());
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    // Update Range
    void UpdateRange()
    {
        targetRange = Player.instance.range;
        sphereCollider.radius = targetRange;
    }

    // Draw Targeting Range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, targetRange);
    }

    // Add to Enemy Array
    private void OnTriggerEnter(Collider other)
    {
        // Swap to Check for Component
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }
    // Remove from Array
    private void OnTriggerExit(Collider other)
    {
        // Release Target
        if (target == other)
        {
            target = null;
            GameEvents.instance.OnLoseTarget();
        }
        // Remove from List
        enemies.Remove(other.gameObject);
    }
}
