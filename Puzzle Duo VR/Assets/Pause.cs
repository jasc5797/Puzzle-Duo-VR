using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button restartLevelButton;
    public Button resumeLevelButton;
    public Button returnToMainButton;
    public Button quitButton;

    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);

        restartLevelButton.onClick.AddListener(RestartLevel);
        resumeLevelButton.onClick.AddListener(ResumeLevel);
        returnToMainButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);

    }

    public void ResumeLevel()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        SceneManager.LoadScene(0);
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
