using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuStates
{
    InGame, MianMenu
}

public enum GameStates
{
    Resume, Pause, Main, Ending
}

public class MenuManager : MonoBehaviour
{
    public MenuStates menuState;
    [SerializeField] private InputManager inputManager;

    public GameStates State;
    [SerializeField] private string currentLevel;
    [SerializeField] private string MenuScene = "MainMenu";

    public Panel currentPanel = null;
    [SerializeField]private List<Panel> panelHistory = new List<Panel>();
    [SerializeField] private Panel ResumePanel, PausePanel;
    [SerializeField] private bool InResume, inPause;

    // Start is called before the first frame update
    void Start()
    {
        SetUpPanel();
        inputManager = InputManager.Instance;
    }
    private void SetUpPanel()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach (Panel panel in panels)
        {
            panel.Setup(this);
        }
        currentPanel.Show();
    }
    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameStates.Resume:
                ResumeFuctions();
                break;
            case GameStates.Pause:
                PauseFunctions();
                break;
            case GameStates.Ending:
                //put Ending Functions here
                break;
        }


        if (menuState == MenuStates.InGame)
        {
            //if (inputManager.PlayerPausedOnFrame())
            {
                if (State == GameStates.Pause)
                {
                    SetCurrentWithHistory(ResumePanel);
                    State = GameStates.Resume;

                }
                else if (State == GameStates.Resume)
                {
                    SetCurrentWithHistory(PausePanel);
                    State = GameStates.Pause;
                }
            }

        }

        if (menuState == MenuStates.MianMenu)
        {
            UnlockCursor();
        }
       

    }

    // functions
    void ResumeFuctions()
    {
        lockCursor();
        Time.timeScale = 1f;
    }
    void PauseFunctions()
    {
        UnlockCursor();
        Time.timeScale = 0.0f;
    }

    public void GoToPrevious()
    {
        if (panelHistory.Count == 0)
        {
            return;
        }

        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(Panel NewPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(NewPanel);

        if (panelHistory.Count >= 10)
        {
            panelHistory.Remove(panelHistory[10]);
            /*foreach (Panel panel in panelHistory)
            {
                if (panel == currentPanel)
                {
                    panelHistory.Remove(panel);
                }
            }*/
        }

    }

    private void SetCurrent(Panel NewPanel)
    {
        currentPanel.Hide();

        currentPanel = NewPanel;
        currentPanel.Show();
    }

    /// <summary>
    /// Buttons
    /// </summary>

    public void StartGame()
    {
        SceneManager.LoadScene(currentLevel);
    }
    public void ExitGame()
    {
        Application.Quit();
    }




    // Cursor Logic
    void lockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // switching states on button command
    public void StateToResume()
    {
        SetCurrentWithHistory(ResumePanel);
        State = GameStates.Resume;
    }
    public void StateToPause()
    {
        SetCurrentWithHistory(PausePanel);
        State = GameStates.Pause;
    }
    public void StateToMain()
    {
        State = GameStates.Main;
    }
    public void StateToEnd()
    {
        State = GameStates.Ending;
    }
    public void RestartFunction()
    {
        UnlockCursor();
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(currentLevel);
    }

    public void BackToMainMenuFunction()
    {
        Debug.Log("Main Menu Button Clicked");
        UnlockCursor();
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(MenuScene);
    }


}
