using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject optionsScreen;
    public GameObject pauseScreen;
    public MenuScreen winLoseScreen;
    public Transition transition;

    public AudioMixer audioMixer;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public List<string> levelNames = new List<string>();

    bool isPaused = false;
    float timeScale;
    int currentLevel = 0;
    bool lastLevel = false;

    static MenuController instance = null;
    public static MenuController Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        audioMixer.SetFloat("MasterVolume", masterVolume);
        MasterSlider.value = masterVolume;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
        audioMixer.SetFloat("MusicVolume", musicVolume);
        MusicSlider.value = musicVolume;

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
        audioMixer.SetFloat("SFXVolume", sfxVolume);
        SFXSlider.value = sfxVolume;
    }

    public void OnLoadGameScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(LoadGameScene(sceneName));
    }

    IEnumerator LoadGameScene(string sceneName)
    {
        //transition.StartTransition(Color.black, 1);

        //while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(false);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }

    public void OnReloadCurrentScene()
    {
        winLoseScreen.gameObject.SetActive(false);
        OnLoadGameScene(SceneManager.GetActiveScene().name);
    }

    public void OnLoadNextLevelScene()
    {
        winLoseScreen.gameObject.SetActive(false);
        currentLevel++;
        if (currentLevel == levelNames.Count - 1) lastLevel = true;
        if (currentLevel >= 0 && currentLevel < levelNames.Count)
        {
            string levelName = levelNames[currentLevel];
            OnLoadGameScene(levelName);
        }
    }

    public void OnLoadMenuScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            OnTitleScreen();
        }
        StartCoroutine(LoadMenuScene(sceneName));
    }

    IEnumerator LoadMenuScene(string sceneName)
    {
        //transition.StartTransition(Color.black, 1);

        //while (!transition.IsDone) { yield return null; }

        
        SceneManager.LoadScene(sceneName);

        yield return null;
    }

    public void OnTitleScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        titleScreen?.SetActive(true);


        optionsScreen?.SetActive(false);
        pauseScreen?.SetActive(false);
        winLoseScreen.gameObject?.SetActive(false);
    }

    public void OnOptionsScreen()
    {
        titleScreen?.SetActive(false);
        optionsScreen?.SetActive(true);
    }

    public void OnPauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = timeScale;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnPause()
    {
        OnPauseScreen();
    }

    public void OnWinScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winLoseScreen.gameObject.SetActive(true);
        winLoseScreen.GetText("Win").gameObject.SetActive(true);
        winLoseScreen.GetText("Lose").gameObject.SetActive(false);
        winLoseScreen.GetButton("Continue").gameObject.SetActive(!lastLevel);
        winLoseScreen.GetButton("Retry").gameObject.SetActive(true);
        winLoseScreen.GetButton("Quit").gameObject.SetActive(true);
    }

    public void OnLoseScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winLoseScreen.gameObject.SetActive(true);
        winLoseScreen.GetText("Win").gameObject.SetActive(false);
        winLoseScreen.GetText("Lose").gameObject.SetActive(true);
        winLoseScreen.GetButton("Continue").gameObject.SetActive(false);
        winLoseScreen.GetButton("Retry").gameObject.SetActive(true);
        winLoseScreen.GetButton("Quit").gameObject.SetActive(true);
    }

    public void OnMasterVolume(float level)
    {
        audioMixer?.SetFloat("MasterVolume", level);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void OnMusicVolume(float level)
    {
        audioMixer?.SetFloat("MusicVolume", level);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    public void OnSFXVolume(float level)
    {
        audioMixer?.SetFloat("SFXVolume", level);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }


}
