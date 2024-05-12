using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;


    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Submarine";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string useClaw = "UseClaw";
    [SerializeField] private string retractClaw = "RetractClaw";
    [SerializeField] private string dropClaw = "DropClaw";


    private InputAction moveAction;
    private InputAction useClawAction;
    private InputAction retractClawAction;
    private InputAction dropClawAction;

    public Vector2 MoveInput { get; private set; }
    public bool UseClawTriggered { get; private set; }

    public bool RetractClawTriggered { get; private set; }

    public bool DropClawTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

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


        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        useClawAction = playerControls.FindActionMap(actionMapName).FindAction(useClaw);
        retractClawAction = playerControls.FindActionMap(actionMapName).FindAction(retractClaw);
        dropClawAction = playerControls.FindActionMap(actionMapName).FindAction(dropClaw);

        RegisterInputActions();
    }


    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        useClawAction.performed += context => UseClawTriggered = true;
        useClawAction.canceled += context => UseClawTriggered = false;

        dropClawAction.performed += context => DropClawTriggered = true;
        dropClawAction.canceled += context => DropClawTriggered = false;

        retractClawAction.performed += context => RetractClawTriggered = true;
        retractClawAction.canceled += context => RetractClawTriggered = false;
    }


    private void OnEnable()
    {
        moveAction.Enable();
        useClawAction.Enable();
        retractClawAction.Enable();
        dropClawAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        useClawAction.Disable();
        retractClawAction.Disable();
        dropClawAction.Disable();
    }

}
