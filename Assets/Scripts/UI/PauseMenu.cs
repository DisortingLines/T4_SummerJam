using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Resume, Pause, Main, Ending
}
public class PauseMenu : MonoBehaviour
{
    public GameStates State;
    private MenuManager Menus;
    [SerializeField] private InputManager inputManager;

    [Header("Pause Functions")]
    [SerializeField] private string currentLevel;
    [SerializeField] private string MenuScene = "MainMenu";
    [SerializeField] private Panel ResumePanel, PausePanel;
    [SerializeField] private bool InResume, inPause;
    // Start is called before the first frame update
    void Start()
    {
        Menus = MenuManager.Instance;
        inputManager = InputManager.Instance;
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
        //if (inputManager.PlayerPausedOnFrame())
        {
            if (State == GameStates.Pause)
            {
                Menus.SetCurrentWithHistory(ResumePanel);
                State = GameStates.Resume;

            }
            else if (State == GameStates.Resume)
            {
                Menus.SetCurrentWithHistory(PausePanel);
                State = GameStates.Pause;
            }
        }
    }

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
        Menus.SetCurrentWithHistory(ResumePanel);
        State = GameStates.Resume;
    }
    public void StateToPause()
    {
        Menus.SetCurrentWithHistory(PausePanel);
        State = GameStates.Pause;
    }

    public void StateToEnd()
    {
        State = GameStates.Ending;
    }
}
