using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuControllerInGame : MonoBehaviour
{
    int chrindex;
    int nrKilled;
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Save()
    {
        chrindex = PlayerPrefs.GetInt("CharacterSelected");
        nrKilled = killedNr + killedNrInit;
        string lvlName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedLevel", lvlName);
        PlayerPrefs.SetInt("SavedCharacter", chrindex);
        PlayerPrefs.SetInt("KilledEnemies", nrKilled);
    }

    [Header("Volume Setting")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField] private TextMeshProUGUI controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Difficulty Dropdown")]
    public TMP_Dropdown difficultyDropdown;

    private int _difficultyLevel = 1;

    public GameObject[] enemies;
    public GameObject[] initialEnemies;

    [SerializeField] public TextMeshProUGUI killedEnemies;

    public int killedNr;
    public int killedNrInit;


    [SerializeField] public TextMeshProUGUI wastedTime;

    public void SetDifficulty(int difficultyIndex)
    {
        _difficultyLevel = difficultyIndex;
    }


    private void Start()
    {
        killedNrInit = 0;

        initialEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        bool ng = GameObject.Find("MenuController").GetComponent<MenuController>().newGame;

        Debug.Log(ng);

        if (PlayerPrefs.HasKey("KilledEnemies") && ng == false)
        {
            killedNrInit = PlayerPrefs.GetInt("KilledEnemies");
        }

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        killedNr = initialEnemies.Length - enemies.Length;
        killedEnemies.text = (killedNr + killedNrInit).ToString();

        wastedTime.text = Time.time.ToString("0.0");
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetControllerSensitivity(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }
        PlayerPrefs.SetFloat("masterSen", mainControllerSen);

        int previousDifficulty = PlayerPrefs.GetInt("masterDifficulty");

        PlayerPrefs.SetInt("masterDifficulty", _difficultyLevel);

        if(previousDifficulty != _difficultyLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            _difficultyLevel = 1;
            GameplayApply();
        }
    }
}
