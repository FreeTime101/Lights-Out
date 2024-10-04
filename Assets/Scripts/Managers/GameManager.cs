using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public GameState state { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    public enum GameState
    {
        Start,
        GameOver,
        Restart
    }

    private void Start()
    {
        UpdateGameState(GameState.Restart);
    }

    public void UpdateGameState(GameState newState)
    {
        this.state = newState;

        switch (newState)
        {
            case GameState.Start:
                break;
            case GameState.GameOver:
                break;
            case GameState.Restart:
                OnGameStateChanged?.Invoke(newState);
                UpdateGameState(GameState.Start);
                return;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

}
