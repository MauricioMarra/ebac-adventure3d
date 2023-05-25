using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Inputs inputActions;

    // Start is called before the first frame update
    void Start()
    {
        inputActions = new Inputs();
        inputActions.Enable();

        Init();
    }

    protected virtual void Init()
    {

    }

    private void OnEnable()
    {
        if (inputActions != null) 
            inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
