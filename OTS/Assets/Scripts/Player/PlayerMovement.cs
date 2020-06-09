using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Setup
    private CharacterController controller;
    private Player player;
    private float movementSpeed;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        player = Player.instance;
        movementSpeed = player.movementSpeed;
    }
    #endregion Setup

    // Update is called once per frame
    void Update()
    {
        // Update to when gear Change
        UpdateMoveSpeed();
        // Get Key Input & Move
        controller.Move(GetDirection() * Time.deltaTime * movementSpeed);
    }

    // Update MS
    public void UpdateMoveSpeed()
    {
        movementSpeed = player.movementSpeed;
    }

    // Get Character Direction
    Vector3 GetDirection()
    {
        // Reset Direction
        Vector3 direction = Vector3.zero;
        // Key Inputs
        if (Input.GetKey(KeyCode.W) || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }
        // Get Angle
        return(direction.normalized);
    }
}
