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

    int numberOfReflections;

    Vector3 initialPos;
    Vector3 leftClickPos;
    Vector3 direction;

    Camera mainCam;
    PlayerVFX playerVFX;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        playerVFX = GetComponent<PlayerVFX>();

        mainCam = FindObjectOfType<Camera>();

        initialPos = transform.position;
        controlEnabled = true;
    }

    void Update()
    {
        leftButtonIsPressed = Input.GetMouseButtonDown(0);
        HandleMovement();
    }

    void FixedUpdate()
    {
        MovePlayerInDirection();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MapBorder"))
        {
            //Debug.Log(collision.contactCount);

            Vector2 wallNormal = collision.GetContact(0).normal;
            direction = Vector2.Reflect(rigid2D.velocity, wallNormal).normalized;

            //Debug.Log(collision.gameObject);
            if (numberOfReflections == 2)
            {
                Object.Destroy(gameObject);
            }
            else
            {
                numberOfReflections++;
                //Debug.Log(numberOfReflections);
            }
        }

        if (collision.gameObject.CompareTag("PopDot"))
        {
            Object.Destroy(collision.gameObject);
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
}
