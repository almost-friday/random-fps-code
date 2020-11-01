using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float Sensitivity_X;
    public float Sensitivity_Y;

    private Vector2 rotation = Vector2.zero;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RotateCamera (Transform parent){
        rotation.y += Input.GetAxis("Mouse X") * Sensitivity_X;
        rotation.x += -Input.GetAxis("Mouse Y") * Sensitivity_Y;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

        parent.eulerAngles = new Vector2(0, rotation.y) * Sensitivity_X;
        transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
    }


    public void ChangeXSensitivity(float value) => Sensitivity_X = value/2;
    public void ChangeYSensitivity(float value) => Sensitivity_Y = value/2;
}

