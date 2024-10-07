using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : Singleton<StatsManager>
{

    [Header("Object References")]
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Animator newGameAnimator;

    private int pressedAmount = 0;
    private int passedTime = 0;

    private float timer;

    private bool isTimerRun = false;

    protected override void Awake()
    {
        base.Awake();

        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if (!isTimerRun)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f) 
        {
            passedTime += (int)(timer / 1);
            timer -= (timer / 1);
            UpdateTimerText();
        }
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Start:
                break;
            case GameManager.GameState.GameOver:
                isTimerRun = false;
                break;
            case GameManager.GameState.Restart:
                newGameAnimator.SetTrigger("HideTrigger");
                pressedAmount = 0;
                passedTime = 0;
                timer = 0;
                UpdateMovesText();
                UpdateTimerText();
                break;
            default:
                break;
        }
    }

    public void IncreaseMoves() 
    {
        pressedAmount++;
        UpdateMovesText();
    }

    private void UpdateMovesText() => movesText.text = pressedAmount.ToString();

    private void UpdateTimerText() => timerText.text = CalculatePassedTime();

    private string CalculatePassedTime()
    {
        return (passedTime / 60).ToString() + ":" + (passedTime % 60).ToString("D2");
    }

    public void SetTimerState(bool isRun) => isTimerRun = isRun;

    public void FlashNewGameText() => newGameAnimator.SetTrigger("FlashTrigger");

}
