using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 200f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("masterSen"))
        { 
            mouseSensitivity = PlayerPrefs.GetFloat("masterSen");
            mouseSensitivity *= 50f; 
        }
        var flipY = PlayerPrefs.GetInt("masterInvertY");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY;
        if(flipY == 1)
             mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * (-1);   
        else
             mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}