using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAchievements : MonoBehaviour
{
    //general vars
    public GameObject achNote;
    public AudioSource achSound;
    public bool achActive = false;
    public GameObject achTitle;
    public GameObject achDesc;

    //Ach 01 specific
    public GameObject ach01Image;
    public static int ach01Count;
    public int ach01Trigger = 10;
    public int ach01Code;

    //Ach 02 specific
    public GameObject ach02Image;
    public static bool triggerAch02 = false;
    public int ach02Code;

    //Ach 03 specific
    public GameObject ach03Image;
    public static bool triggerAch03 = false;
    public int ach03Code;

    //Ach 04 specific
    public GameObject ach04Image;
    public static bool triggerAch04 = false;
    public int ach04Code;

    //Ach 05 specific
    public GameObject ach05Image;
    public static bool triggerAch05 = false;
    public int ach05Code;

    void Update()
    {
        ach01Code = PlayerPrefs.GetInt("Ach01");

        if(ach01Count == ach01Trigger && ach01Code != 111)
        {
            StartCoroutine(Trigger01Ach());
        }

        ach02Code = PlayerPrefs.GetInt("Ach02");

        if (triggerAch02 == true && ach02Code != 222)
        {
            StartCoroutine(Trigger02Ach());
        }

        ach03Code = PlayerPrefs.GetInt("Ach03");

        if (triggerAch03 == true && ach03Code != 333)
        {
            StartCoroutine(Trigger03Ach());
        }

        ach04Code = PlayerPrefs.GetInt("Ach04");

        if (triggerAch04 == true && ach04Code != 444)
        {
            StartCoroutine(Trigger04Ach());
        }

        ach05Code = PlayerPrefs.GetInt("Ach05");

        if (triggerAch05 == true && ach05Code != 555)
        {
            StartCoroutine(Trigger05Ach());
        }
    }

    IEnumerator Trigger01Ach()
    {
        achActive = true;
        ach01Code = 111;
        PlayerPrefs.SetInt("Ach01", ach01Code);
        achSound.Play();
        ach01Image.SetActive(true);
        achTitle.GetComponent<Text>().text = "Bone Collector";
        achDesc.GetComponent<Text>().text = "Kill 10 skeletons";
        achNote.SetActive(true);
        yield return new WaitForSeconds(5);
        //Resetting UI
        achNote.SetActive(false);
        ach01Image.SetActive(false);
        achTitle.GetComponent<Text>().text = "";
        achDesc.GetComponent<Text>().text = "";
        achActive = false;
    }


    IEnumerator Trigger02Ach()
    {
        achActive = true;
        ach02Code = 222;
        PlayerPrefs.SetInt("Ach02", ach02Code);
        achSound.Play();
        ach02Image.SetActive(true);
        achTitle.GetComponent<Text>().text = "Just The Beggining";
        achDesc.GetComponent<Text>().text = "Complete the first level";
        achNote.SetActive(true);
        yield return new WaitForSeconds(5);
        //Resetting UI
        achNote.SetActive(false);
        ach02Image.SetActive(false);
        achTitle.GetComponent<Text>().text = "";
        achDesc.GetComponent<Text>().text = "";
        achActive = false;
    }

    IEnumerator Trigger03Ach()
    {
        achActive = true;
        ach03Code = 333;
        PlayerPrefs.SetInt("Ach03", ach03Code);
        achSound.Play();
        ach03Image.SetActive(true);
        achTitle.GetComponent<Text>().text = "Good Choice!";
        achDesc.GetComponent<Text>().text = "Choose one of the 2 doors";
        achNote.SetActive(true);
        yield return new WaitForSeconds(5);
        //Resetting UI
        achNote.SetActive(false);
        ach03Image.SetActive(false);
        achTitle.GetComponent<Text>().text = "";
        achDesc.GetComponent<Text>().text = "";
        achActive = false;
    }

    IEnumerator Trigger04Ach()
    {
        achActive = true;
        ach04Code = 444;
        PlayerPrefs.SetInt("Ach04", ach04Code);
        achSound.Play();
        ach04Image.SetActive(true);
        achTitle.GetComponent<Text>().text = "Tank Destroyer";
        achDesc.GetComponent<Text>().text = "Defeat the RhinoScarab boss";
        achNote.SetActive(true);
        yield return new WaitForSeconds(5);
        //Resetting UI
        achNote.SetActive(false);
        ach04Image.SetActive(false);
        achTitle.GetComponent<Text>().text = "";
        achDesc.GetComponent<Text>().text = "";
        achActive = false;
    }

    IEnumerator Trigger05Ach()
    {
        achActive = true;
        ach05Code = 555;
        PlayerPrefs.SetInt("Ach05", ach05Code);
        achSound.Play();
        ach05Image.SetActive(true);
        achTitle.GetComponent<Text>().text = "The Bigger They Are";
        achDesc.GetComponent<Text>().text = "Defeat the Golem boss";
        achNote.SetActive(true);
        yield return new WaitForSeconds(5);
        //Resetting UI
        achNote.SetActive(false);
        ach04Image.SetActive(false);
        achTitle.GetComponent<Text>().text = "";
        achDesc.GetComponent<Text>().text = "";
        achActive = false;
    }
}
