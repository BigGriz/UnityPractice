using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class FollowCamera : MonoBehaviour
{
    #region Singleton
    public static FollowCamera instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple Cameras Exist!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion Singleton

    // Player & Attributes
    private Transform target;
    private float targetHeight;
    // Rotate Speeds
    private float xSpeed = 250.0f;
    private float ySpeed = 120.0f;
    // Y Angle Limits
    private int yMinLimit = -60;
    private int yMaxLimit = 60;
    // Zoom Speed
    private int zoomRate = 40;
    // Damping Rot & Xoom
    private float rotationDampening = 3.0f;
    private float zoomDampening = 5.0f;
    // Calculating Angles
    private float x = 0.0f;
    private float y = 0.0f;
    // Calculating Distance
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    [Header("Follow Distance")]
    public float minDistance = .6f;
    public float maxDistance = 20;
    [Header("Ease Timer")]
    public float timer = 0.0f;

    public bool rotating;

    void Start()
    {
        // Singletons
        if (!Player.instance)
        {
            Debug.LogError("Player Instance is Missing from Camera!");
        }
        else
        {
            target = Player.instance.gameObject.transform;
            targetHeight = target.GetComponent<Collider>().bounds.extents.y / 2;
        }
        // Get Starting Properties
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        float distance = (minDistance + maxDistance) / 3.0f;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
    }

    void LateUpdate()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                rotating = true;
            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            rotating = false;
        }
        // Get Mouse Axis
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && rotating)
        {

                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                timer = 1.5f;

                if (Input.GetMouseButton(1))
                {
                    target.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                }
            
        }
        // Ease on Movement
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || timer <= 0.0f)
        {
            float targetRotationAngle = target.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
        }
        // Clamp Y Rotation
        y = ClampAngle(y, yMinLimit, yMaxLimit);
        // Set Camera Rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // Calculate Desired Distance
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        // Calculate Desired Position
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));

        // Check for Collision using the Target's Desired Registration Point as set by user using height
        RaycastHit collisionHit;
        Vector3 targetPos = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

        bool isCorrected = false;
        // Check for Collision between Character & Camera
        if (Physics.Linecast(targetPos, position, out collisionHit))
        {
            // If so, get Distance between Character & Colliding Object
            if (collisionHit.transform != target)
            {
                transform.position = collisionHit.point;
                correctedDistance = Vector3.Distance(targetPos, collisionHit.point);
                correctedDistance *= 0.80f;
            }
            isCorrected = true;
        }
        // If there was a Collision - Snap
        if (isCorrected)
        {
            currentDistance = Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening);
        }
        // Else usual Lerp
        else
        {
            currentDistance = correctedDistance;
        }

        // Calc pos w/ new currentDistance
        position = target.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -targetHeight, 0));

        // Set Position & Rotation
        transform.rotation = rotation;
        transform.position = position;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        // Clamp Camera Angles
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        { 
            angle -= 360;
        }
        return (Mathf.Clamp(angle, min, max));
    }
}