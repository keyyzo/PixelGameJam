using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset menuControls;


    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "MenuControls";

    [Header("Action Name References")]
    [SerializeField] private string menuOpenClose = "MenuOpenClose";
    [SerializeField] private string select = "Select";
    [SerializeField] private string moveUp = "MoveUp";
    [SerializeField] private string moveDown = "MoveUp";

    private InputAction menuOpenCloseAction;
    private InputAction selectAction;
    private InputAction moveUpAction;
    private InputAction moveDownAction;

    public bool UseMenuOpenCloseTriggered { get; private set; }
    public bool UseSelectTriggered { get; private set; }

    public bool UseMoveUpTriggered { get; private set; }

    public bool UseMoveDownTriggered { get; private set; }


    public static MenuInputManager Instance { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        menuOpenCloseAction = menuControls.FindActionMap(actionMapName).FindAction(menuOpenClose);
        selectAction = menuControls.FindActionMap(actionMapName).FindAction(select);
        moveUpAction = menuControls.FindActionMap(actionMapName).FindAction(moveUp);
        moveDownAction = menuControls.FindActionMap(actionMapName).FindAction(moveDown);

        RegisterInputActions();
    }


    void RegisterInputActions()
    {
        menuOpenCloseAction.performed += context => UseMenuOpenCloseTriggered = true;
        menuOpenCloseAction.canceled += context => UseMenuOpenCloseTriggered = false;

        selectAction.performed += context => UseSelectTriggered = true;
        selectAction.canceled += context => UseSelectTriggered = false;

        moveUpAction.performed += context => UseMoveUpTriggered = true;
        moveUpAction.canceled += context => UseMoveUpTriggered = false;

        moveDownAction.performed += context => UseMoveDownTriggered = true;
        moveDownAction.canceled += context => UseMoveDownTriggered = false;
    }

    private void OnEnable()
    {
        menuOpenCloseAction.Enable();
        selectAction.Enable();
        moveUpAction.Enable();
        moveDownAction.Enable();
    }

    private void OnDisable()
    {
        menuOpenCloseAction.Disable();
        selectAction.Disable();
        moveUpAction.Disable();
        moveDownAction.Disable();
    }

}

    
