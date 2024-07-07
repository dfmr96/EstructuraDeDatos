using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum MenuState
{
    PressStart,
    MainMenu,
    LevelSelect
}
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pressStart;
    [SerializeField] private float blinkTime = 0.5f;
    [SerializeField] private float blinkCounter = 0;
    [SerializeField] private GameObject menuTilePanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelScene;
    [SerializeField] private GameObject level1Btn;
    [SerializeField] private GameObject playBtn;
    [SerializeField] private MenuState state;

    private void Start()
    {
        state = MenuState.PressStart;
    }

    private void Update()
    {
        switch (state)
        {
            case MenuState.PressStart:
                blinkCounter += Time.deltaTime;
                if (blinkCounter > blinkTime)
                {
                    TogglePressStart();
                    blinkCounter = 0;
                }
                
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    state = MenuState.MainMenu;
                    menuTilePanel.SetActive(false);
                    mainMenuPanel.SetActive(true); 
                    EventSystem.current.SetSelectedGameObject(playBtn);
                }
                break;
            case MenuState.MainMenu:
                break;
            case MenuState.LevelSelect:
                
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void TogglePressStart()
    {
        pressStart.SetActive(!pressStart.activeSelf);
    }

    public void EnterLevelScene()
    {
        levelScene.SetActive(true);
        EventSystem.current.SetSelectedGameObject(level1Btn);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
