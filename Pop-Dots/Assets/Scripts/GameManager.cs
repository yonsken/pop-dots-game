using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerInstance;
    public GameObject dotInstance;

    Vector3 playerIntialPos;

    void Awake()
    {
        SelectPlayerInitialPos();
        PlaceDots();
        PlacePlayer();

        /* Testing fun
        for (int dotIndex = 0; dotIndex < 1000; dotIndex++)
        {
            SelectPlayerInitialPos();
            //PlaceDots();
            PlacePlayer();
        }
        */
    }

    void SelectPlayerInitialPos()
    {
        /* We need to find the positions of the world space corners to then use their positions to get the range of appropriate
           positions for the Player node, while accounting for the map borders */
        Vector3 bottomLeftCornerWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topLeftCornerWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        // Not needed for calculations
        // Vector3 bottomRightCornerWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)); 
        Vector3 topRightCornerWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        playerIntialPos = new Vector3(Random.Range(topLeftCornerWorldPos.x + 2f, topRightCornerWorldPos.x - 2f), 
                                      Random.Range(bottomLeftCornerWorldPos.y + 2f, topLeftCornerWorldPos.y - 2f), 0f);
        playerIntialPos = new Vector3(playerIntialPos.x, playerIntialPos.y, 0f);
    }

    void PlaceDots()
    {
        // Find a random direction to shoot from the initial Player node position
        Vector2 intialRandomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // For now lets use a constant number of dots [SHOULD BE CHANGED]
        int noOfDots = 3;

        Vector2 nextRaycastDir = intialRandomDir;
        Vector2 nextPosToRaycastFrom = playerIntialPos;
        for (int dotIndex = 0; dotIndex < noOfDots; dotIndex++)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(nextPosToRaycastFrom, nextRaycastDir);

            // Debugging outputs
            Debug.Log("Reflection no. " + (dotIndex + 1) + " happened at position: " + hitInfo.point);
            Debug.DrawLine(nextPosToRaycastFrom, hitInfo.point, Color.red, 20f);
            // Debug.DrawLine(nextPosToRaycastFrom, actualHitPoint, Color.green, 20f);

            PlaceNewDot(nextPosToRaycastFrom, hitInfo.point, .15f);

            nextRaycastDir = Vector2.Reflect(nextRaycastDir, hitInfo.normal);
            /* The hit point of a ray needs adjusting, because it is inside of an obstacle and if we cast the next ray from this point it will
               detect the same obstacle and will get stuck inside of it */
            nextPosToRaycastFrom = new Vector2(hitInfo.point.x + hitInfo.normal.x * .001f, hitInfo.point.y + hitInfo.normal.y * .001f);
            //Debug.Log("Intial hit point adjusted: " + nextPosToRaycastFrom);
        }
    }

    void PlacePlayer()
    {
        Instantiate(playerInstance, playerIntialPos, Quaternion.identity);
        Debug.Log("Player initial position on spawn: " + playerIntialPos);
    }

    void PlaceNewDot(Vector2 minPoint, Vector2 maxPoint, float maxPerpendicularOffset)
    {
        // Calculate a random position between the start of a ray and its hit point.
        Vector2 posDelta = maxPoint - minPoint;
        /* The value window should scale with the length of a ray, because in some cases a dot sprite overlaps with the map border can
           too noticable [SHOULD BE CHANGED]*/
        Vector2 newDotPos = minPoint + posDelta * Random.Range(.1f, .9f);

        Vector2 perpendicularOffset = Vector2.Perpendicular(posDelta).normalized * Random.Range(-maxPerpendicularOffset, maxPerpendicularOffset);
        Vector2 newDotPosWithOffset = newDotPos + perpendicularOffset;

        // Debugging outputs
        Debug.DrawLine(newDotPos, newDotPosWithOffset, Color.red, 20f);

        Instantiate(dotInstance, newDotPosWithOffset, Quaternion.identity);
    }
}
