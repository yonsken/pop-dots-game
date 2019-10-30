using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public InputData inputData;
    public Rigidbody2D rigid2D;

    public float moveSpeed = 5f;
    public bool leftClickedOnPlayer;
    public bool controlEnabled;
    public bool holdingDragEnabled;
    public bool holdingLeftMButton;
    public bool releasedLeftMButton;
    public bool releasedLeftClickOnPlayer;

    int numberOfReflections;

    Vector3 initialPos;
    Vector3 possibleReleasePos;
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
        holdingLeftMButton = Input.GetMouseButton(0);
        releasedLeftMButton = Input.GetMouseButtonUp(0);

        if (!holdingDragEnabled && leftClickedOnPlayer)
        {
            leftClickedOnPlayer = false;
            holdingDragEnabled = true;
        }

        if (controlEnabled)
        {
            UpdateMovementState();
        }
    }

    void FixedUpdate()
    {
        MovePlayerInDirection();
    }

    void UpdateMovementState()
    {
        if (holdingDragEnabled && holdingLeftMButton)
        {
            possibleReleasePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            possibleReleasePos = new Vector2(possibleReleasePos.x, possibleReleasePos.y);
        }

        if (holdingDragEnabled && releasedLeftMButton)
        {
            if (!releasedLeftClickOnPlayer)
            {
                CalculateDirection();
                holdingDragEnabled = false;

                controlEnabled = false;
                releasedLeftClickOnPlayer = false;
            }
            else
            {
                holdingDragEnabled = false;
                releasedLeftClickOnPlayer = false;
            }
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
        direction = -(possibleReleasePos - initialPos).normalized;
        //Debug.Log(direction.magnitude);
    }

    void MovePlayerInDirection()
    {
        rigid2D.velocity = direction * moveSpeed;
        //Debug.Log(rigid2D.velocity.magnitude);
    }

    /*
    bool LeftClickedOnPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHitInfo;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Debug.Log("pas");

            if (Physics.Raycast(ray, out rayHitInfo))
            {
                Debug.Log(rayHitInfo.transform.name);
                if (rayHitInfo.transform.name == "Player(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    */

    private void OnMouseDown()
    {
        leftClickedOnPlayer = true;
    }

    private void OnMouseUpAsButton()
    {
        releasedLeftClickOnPlayer = true;
    }
}
