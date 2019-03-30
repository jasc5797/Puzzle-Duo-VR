using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public Button restartLevelButton;
    public Button resumeLevelButton;
    public Button returnToMainButton;
    public Button quitButton;

    public GameObject pausePanel;


    //private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);

        restartLevelButton.onClick.AddListener(RestartLevel);
        resumeLevelButton.onClick.AddListener(ResumeLevel);
        returnToMainButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(Quit);

        //eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonUp("Pause"))
        {
            Debug.Log("Q Pressed");
            if (!pausePanel.activeInHierarchy)
            {
                Debug.Log("Pausing Level");
                PauseLevel();
            }
            else if (pausePanel.activeInHierarchy)
            {
                Debug.Log("Resuming Level");
                ResumeLevel();
            }
        }
    }

    public void PauseLevel()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //resumeLevelButton.Select();
        //eventSystem.SetSelectedGameObject(resumeLevelButton.gameObject);

    }

    public void ResumeLevel()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        ResumeLevel();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void ReturnToMainMenu()
    {
        ResumeLevel();
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        ResumeLevel();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
