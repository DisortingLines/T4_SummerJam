using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayControls Controls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Controls = new PlayControls();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return Controls.InGame.Move.ReadValue<Vector2>();
    }

    public Vector2 GetCameraMovement()
    {
        return Controls.InGame.Camera.ReadValue<Vector2>();
    }

    public bool JumpOnFrame()
    {
        return Controls.InGame.Jump.triggered;
    }
}
