using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text constructionLogTMPText;
    [SerializeField] private PlayerCastle _playerCastle;
    [SerializeField] private string constructionLogText;

    private void Start()
    {
        GetConstructionLog();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetConstructionLog();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            string newString = "Prueba 1";
            _playerCastle.constructionLog.Push(newString);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            string newString = "Prueba 2";
            _playerCastle.constructionLog.Push(newString);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            string newString = "Prueba 3";
            _playerCastle.constructionLog.Push(newString);
        }
    }

    public void GetConstructionLog()
    {
        while (_playerCastle.constructionLog.head != null)
        {
            constructionLogText += $"{_playerCastle.constructionLog.Peek()} \n";
            _playerCastle.constructionLog.Pop();
        }
        
        constructionLogTMPText.SetText(constructionLogText);
    }
}
