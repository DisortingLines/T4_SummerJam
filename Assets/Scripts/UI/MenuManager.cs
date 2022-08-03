using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get
        {
            return _instance;
        }
    }
    [SerializeField] private InputManager inputManager;

    public Panel currentPanel = null;
    [SerializeField]private List<Panel> panelHistory = new List<Panel>();
    [SerializeField] private string currentLevel;
    [SerializeField] private string MenuScene = "MainMenu";

    // Start is called before the first frame update
    void Start()
    {
        SetUpPanel();
        //inputManager = InputManager.Instance;
        UnlockCursor();
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
       

    }

    // functions

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
