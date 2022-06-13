using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    public GameManager nrEnemies;

    public Transform target;

    public GameObject txt;
    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (nrEnemies.enemies.Length == 0 && distance <= 2)
        {
            txt.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
        else
            txt.SetActive(false);   
    }
}
