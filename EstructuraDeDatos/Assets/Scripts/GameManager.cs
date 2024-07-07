using System;
using System.Collections;
using System.Collections.Generic;
using TDAs;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float coinsPerSeconds;
    public float coins;
    public TMP_Text coinsText;
    public int lvlToUnlock;

    public GameObject victoryPanel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        coins += coinsPerSeconds * Time.deltaTime;
        coinsText.SetText("Coins: " + coins.ToString("000"));
    }

    public void Victory()
    {
        victoryPanel.SetActive(true);
        victoryPanel.GetComponent<VictoryPanel>().GetConstructionLog();
    }
}
