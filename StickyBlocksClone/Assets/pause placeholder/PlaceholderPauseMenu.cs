using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlaceholderPauseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI menuText;
    [SerializeField] GameObject menuPanel;
    [SerializeField] bool gamePaused;
    [SerializeField] GameObject pauseButton;
    [Tooltip("ensure that this scene is added to build!")]
    [SerializeField] string mainMenuSceneName;

    private void Start()
    {
        Time.timeScale = 0; 
    }
    public void ResetPause()
    {
        gamePaused = !gamePaused; //reverse bool
        menuPanel.SetActive(gamePaused); //activate or deactivate the panel
        menuText.text = gamePaused ? "game is paused!" : null; //decide text, look for TERNARY OPERATOR to learn this syntax
        pauseButton.SetActive(!gamePaused); //act or deact pause button
        Time.timeScale = gamePaused ? 0 : 1; //if paused => timescale = 0, if not => timescale = 1
        print("GUI Manager: game active = " + !gamePaused);
        print($"{Time.timeScale} is time scale now!");
    }
    public void ReloadScene()
    {
        print("GUI Manager: reloading the scene!");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //load CURRENT scene
    }
    public void GoToMainMenu()
    {
        print("GUI Manager: going to MAIN MENU");
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
