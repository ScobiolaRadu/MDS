using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject menu;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(menu.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }

            menu.SetActive(!menu.activeSelf);

        }
    }
}
