using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Canvas canvas = null;
    private MenuManager menuManager = null;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Setup(MenuManager menu)
    {
        this.menuManager = menu;
        Hide();
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }
}
