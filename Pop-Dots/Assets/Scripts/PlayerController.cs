using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public InputData inputData;
    public Rigidbody2D rigid2D;

    public float moveSpeed = 5f;
    public bool leftButtonIsPressed;
    public bool controlEnabled;

    Vector3 initialPos;
    Vector3 leftClickPos;
    Vector3 direction;

    Camera mainCam;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        mainCam = FindObjectOfType<Camera>();

        initialPos = transform.position;
        controlEnabled = true;
    }

    void Update()
    {
        leftButtonIsPressed = Input.GetMouseButtonDown(0);
        HandleMovement();
    }

    void HandleMovement()
    {
        if (leftButtonIsPressed && controlEnabled)
        {
            controlEnabled = false;

            leftClickPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            leftClickPos = new Vector3(leftClickPos.x, leftClickPos.y, 0f);

            Debug.Log(leftClickPos);

            CalculateDirection();
            MovePlayerInDirection();
        }
    }

    void CalculateDirection()
    {
        direction = (leftClickPos - initialPos).normalized;
    }

    void MovePlayerInDirection()
    {
        rigid2D.velocity = direction * moveSpeed;
    }

    /*
    void ResetPlayerPos()
    {
        transform.position = this.leftClickPos;
    }
    */
}
