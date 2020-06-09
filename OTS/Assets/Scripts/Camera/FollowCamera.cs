using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float CameraMoveSpeed;
    public GameObject CameraFollowObj;
    public float clampAngle;
    public float inputSensitivity;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    public Vector3 startPos;
    public Quaternion startRot;
    public bool turning;
    public float lerpSpeed;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            turning = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            turning = false;
        }

        if (turning)
        {
            rotY += Input.GetAxis("Mouse X") * inputSensitivity * Time.deltaTime;
            rotX += Input.GetAxis("Mouse Y") * inputSensitivity * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
        else
        {
           // Debug.Log(transform.forward);
            //transform.position = Vector3.Lerp(transform.position, startPos, lerpSpeed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        // set the target object to follow
        Transform target = CameraFollowObj.transform;

        //move towards the game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (!turning)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, lerpSpeed * Time.deltaTime);
        }
    }


    //private Transform target;
    //private Vector3 target_Offset = new Vector3(-3.46f, 2.26f, 3.02f);
    //public float smoothFactor;

    //private Vector3 upVector = new Vector3(0.0f, 1.0f, 0.0f);
    //public bool turning = true;
    //public float lerpSpeed;
    //public Vector3 forwardVec;
    //public bool colliding;

    //private void Start()
    //{
    //    // Singleton
    //    if (!PlayerTargeting.instance)
    //    {
    //        Debug.LogError("Player Targeting Instance is Missing in Camera!");
    //    }
    //    // Swap this for a Player Reference later.
    //    target = PlayerTargeting.instance.gameObject.transform;

    //    forwardVec = transform.forward;
    //}
    //void Update()
    //{
    //    // Update Camera Position
    //    //transform.position = target.position + target_Offset;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        turning = true;
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        turning = false;
    //    }
    //    // Rotate Around World Up
    //    /*if (turning)
    //    {
    //        if (Input.GetAxis("Mouse X") < 0)
    //        {
    //            transform.RotateAround(transform.position, upVector, -1.0f);
    //        }
    //        if (Input.GetAxis("Mouse X") > 0)
    //        {
    //            transform.RotateAround(transform.position, upVector, 1.0f);
    //        }

    //        if (Input.GetAxis("Mouse Y") < 0)
    //        {
    //            transform.RotateAround(transform.position, transform.right, 1.0f);
    //        }
    //        if (Input.GetAxis("Mouse Y") > 0)
    //        {
    //            transform.RotateAround(transform.position, transform.right, -1.0f);
    //        }
    //    }
    //    // Lerp back to Character Forward
    //    else
    //    {
    //       // Debug.Log(transform.forward);
    //        transform.forward = Vector3.Lerp(transform.forward, forwardVec, lerpSpeed * Time.deltaTime);
    //    }*/
    //    float heightOffset = 0;

    //    if (turning)
    //    {
    //        heightOffset = -1 * Input.GetAxis("Mouse Y");

    //        Vector3 camHeightOffset = new Vector3(0.0f, heightOffset, 0.0f);

    //        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * lerpSpeed, Vector3.up);
    //        target_Offset = camTurnAngle * target_Offset + camHeightOffset;       
    //    }

    //    Vector3 newPos = target.position + target_Offset;

    //    RaycastHit hit;
    //    float distance = Vector3.Distance(newPos, target.position);
    //    if (Physics.Raycast(newPos, transform.forward, out hit, distance * 100))
    //    {
    //        if (hit.collider.gameObject.tag == "Environment")
    //        {
    //            Debug.Log("hit environment");
    //        }
    //        else
    //        {
    //            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    //        }
    //    }







    //    transform.LookAt(target.position);
    //}


}
