using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 target_Offset = new Vector3(-3.46f, 2.26f, 3.02f);

    private Vector3 upVector = new Vector3(0.0f, 1.0f, 0.0f);
    public bool turning = true;
    public float lerpSpeed;
    public Vector3 forwardVec;

    private void Start()
    {
        // Singleton
        if (!PlayerTargeting.instance)
        {
            Debug.LogError("Player Targeting Instance is Missing in Camera!");
        }
        // Swap this for a Player Reference later.
        target = PlayerTargeting.instance.gameObject.transform;

        forwardVec = transform.forward;
    }
    void Update()
    {
        // Update Camera Position
        transform.position = target.position + target_Offset;

        if (Input.GetMouseButtonDown(0))
        {
            turning = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            turning = false;
        }
        // Rotate Around World Up
        if (turning)
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.RotateAround(transform.position, upVector, -1.0f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.RotateAround(transform.position, upVector, 1.0f);
            }

            if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.RotateAround(transform.position, transform.right, 1.0f);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.RotateAround(transform.position, transform.right, -1.0f);
            }
        }
        // Lerp back to Character Forward
        else
        {
           // Debug.Log(transform.forward);
            transform.forward = Vector3.Lerp(transform.forward, forwardVec, lerpSpeed * Time.deltaTime);
        }


    }
}
