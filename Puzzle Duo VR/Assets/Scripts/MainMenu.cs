using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        level1Button.onClick.AddListener(LoadLevel1);
        level2Button.onClick.AddListener(LoadLevel2);
        level3Button.onClick.AddListener(LoadLevel3);
        quitButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadLevel1()
    {

        SceneManager.LoadScene(1);
    }

    private void LoadLevel2()
    {

    }

    private void LoadLevel3()
    {

    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
