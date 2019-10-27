using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputData inputData;

    void Update()
    {
        UpdateInputData();
    }

    void UpdateInputData()
    {
        inputData.isPressed = Input.GetMouseButtonDown(0);
        inputData.isHeld = Input.GetMouseButton(0);
        inputData.isReleased = Input.GetMouseButtonUp(0);
    }
}
