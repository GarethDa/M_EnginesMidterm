using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public PlayerAction inputAction;

    public static PlayerInputController controller;

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }

        inputAction = new PlayerAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
