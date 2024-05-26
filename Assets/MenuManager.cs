using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu References")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject quitButton;

    [Header("Controls References")]
    [SerializeField] private GameObject backButton;

    private MenuInputManager inputManager;

    private bool shouldSelect;
    private bool shouldMoveUp;
    private bool shouldMoveDown;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = MenuInputManager.Instance;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    // Update is called once per frame
    void Update()
    {
        InputCallHandler();
    }

    public void PlayFunctionPress()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitFunctionPress()
    { 
        Application.Quit();
    }

    void InputCallHandler()
    {
        shouldMoveDown = inputManager.UseMoveDownTriggered;
        shouldMoveUp = inputManager.UseMoveUpTriggered;
        shouldSelect = inputManager.UseSelectTriggered;
    }
}
