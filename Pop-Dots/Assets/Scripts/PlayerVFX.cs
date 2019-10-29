using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public GameObject chargeDotPrefab;
    public int chargeDotAmount;

    float chargeDotGap;

    GameObject[] chargeDotArray;

    // Start is called before the first frame update
    void Start()
    {
        chargeDotGap = 1f / chargeDotAmount;


    }

    void SpawnChargeDots()
    {
        chargeDotArray = new GameObject[chargeDotAmount];

        for (int dotIndex = 0; dotIndex < chargeDotAmount; dotIndex++)
        {
            GameObject dot = Instantiate(chargeDotPrefab);
            dot.SetActive(false);
            chargeDotArray[dotIndex] = dot;
        }
    }

    public void SetChargeDotPos(Vector3 startPos, Vector3 endPos)
    {
        for (int dotIndex = 0; dotIndex < chargeDotAmount; dotIndex++)
        {
            Vector3 dotPos = chargeDotArray[dotIndex].transform.position;
            Vector3 targetPos = Vector2.Lerp(startPos, endPos, dotIndex * chargeDotGap);
        }
    }
}
