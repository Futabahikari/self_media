using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject,InputActions.IPlayerActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    InputActions inputActions;

    private void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.Player.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisablePlayerInputs();
    }

    public void DisablePlayerInputs()
    {
        inputActions.Player.Disable();
    }

    public void EnablePlayerInputs()
    {
        inputActions.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(onMove != null)
                onMove.Invoke(context.ReadValue<Vector2>()); 
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            if (onStopMove != null)
                onStopMove.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
