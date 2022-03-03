using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]
    private float moveSpeed = 1f;



    private Rigidbody rb;
    private Vector2 movementAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey("left shift")) moveSpeed = 2f;
        else moveSpeed = 1f;

        UpdateMovementAxis();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdateMovementAxis()
    {
        movementAxis.x = Input.GetAxis("Horizontal");
    }

    private void UpdatePosition()
    {
        var positionMovement = transform.forward * 
            (movementAxis.x * moveSpeed * Time.deltaTime);

        var currentPosition = rb.position;
        var newPosition = currentPosition + positionMovement;

        rb.MovePosition(newPosition);
    }
}
