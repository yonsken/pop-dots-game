using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerInstance;

    Vector3 playerIntialPos;

    void Awake()
    {
        SelectPlayerInitialPos();
        PlaceDots();
        PlacePlayer();
    }

    void Update()
    {

    }

    void SelectPlayerInitialPos()
    {
        // Needs changing to account for the walls on the map border.
        playerIntialPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0f));
        playerIntialPos = new Vector3(playerIntialPos.x, playerIntialPos.y, 0f);
    }

    void PlaceDots()
    {

    }

    void PlacePlayer()
    {
        Instantiate(playerInstance, playerIntialPos, Quaternion.identity);
        Debug.Log("Player initial position on spawn: " + playerIntialPos);
    }
}
