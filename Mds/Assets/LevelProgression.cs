using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    public GameManager nrEnemies;

    public Transform target;

    public GameObject txt;

    public MenuControllerInGame menu;
    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (nrEnemies.enemies.Length == 0 && distance <= 2)
        {
            txt.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                if (GlobalAchievements.triggerAch02 == false)
                {
                    GlobalAchievements.triggerAch02 = true;
                    StartCoroutine(WaitForSceneLoad());
                }
                else
                {
                    Screen.lockCursor = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Screen.lockCursor = false;
                }
            }

        }
        else
            txt.SetActive(false);   
    }

    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Screen.lockCursor = false;
    }
}
