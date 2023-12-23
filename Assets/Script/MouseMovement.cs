using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensivity = 100f;
    float yRotation = 0f;
    float xRotation = 0f;

    public float topClamp = -90f;
    public float downClamp = 90f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,topClamp,downClamp);
        yRotation += mouseX;
        
        transform.localRotation =  Quaternion.Euler(xRotation,yRotation,0f);
    }
}
