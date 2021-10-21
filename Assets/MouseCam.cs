using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{

    float MouseSensivity=200f;
    float mouseX;
    float mouseY;
    public Transform playerBody;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * MouseSensivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensivity * Time.deltaTime;
        xRotation-=mouseY;
        xRotation=Mathf.Clamp(xRotation,-90f,90f);
        transform.localRotation=Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
