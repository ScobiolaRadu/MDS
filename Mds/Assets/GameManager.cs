using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject menu;

    public GameObject[] enemies;

void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }

            if(menu.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }

            menu.SetActive(!menu.activeSelf);

        }

    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}

       
