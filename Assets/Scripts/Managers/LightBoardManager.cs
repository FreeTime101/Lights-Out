using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoardManager : Singleton<LightBoardManager>
{

    [Header("Light Board Settings")]
    [SerializeField] private Transform lightBoardObj;
    [Tooltip("Range for the number of buttons the system try to randomly press before the game start.")]
    [SerializeField] private Vector2Int gridRandomRange = new Vector2Int(5, 10);
    public Color32 lightOnColor = Color.white;
    public Color32 lightOffColor = new Color32(105, 105, 105, 255);

    private LightButton[,] lightGrid;
    private bool[,] lightStatus;

    protected override void Awake()
    {
        base.Awake();

        lightGrid = new LightButton[5,5];
        lightStatus = new bool[5,5];
        ReadAllLightButtons();
        ValdiateRandomRange();

        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Start:
                ResetLightStates();
                RandomStartBoard();
                break;
            case GameManager.GameState.GameOver:
                break;
            case GameManager.GameState.Restart:
                break;
            default:
                break;
        }
    }

    private void ValdiateRandomRange() 
    {
        int temp;

        // Default value for min and max is 5 and 10 accordingly

        // Check if the min is greater than max
        if (gridRandomRange.x > gridRandomRange.y)
        {
            temp = gridRandomRange.x;
            gridRandomRange.x = gridRandomRange.y;
            gridRandomRange.y = temp;
        }

        // Check if min is less than or equal to 0 (set to default if it is)
        if (gridRandomRange.x <= 0) 
        {
            gridRandomRange.x = 5;
            if (gridRandomRange.y < gridRandomRange.x)
                gridRandomRange.y = 5;
        }
    }

    private void ReadAllLightButtons() 
    {
        int x = 0;
        int y = 0;

        foreach (Transform row in lightBoardObj)
        {
            foreach (Transform lightButton in row) 
            {
                lightGrid[x, y] = lightButton.gameObject.GetComponent<LightButton>();
                lightGrid[x, y].SetGridPos(new Vector2Int(x, y));
                x++;
            }
            x = 0;
            y++;       
        }

    }

    private void ResetLightStates() 
    {
        for (int y = 0; y < lightGrid.GetLength(0); y++) 
            for (int x = 0; x < lightGrid.GetLength(1); x++)
            {
                lightGrid[x, y].SetLight(false);
                lightStatus[x, y] = false;
            } 

    }

    private void RandomStartBoard() 
    {
        int iterations = Random.Range(gridRandomRange.x, gridRandomRange.y + 1);
        int x = 0;
        int y = 0;

        Debug.Log(iterations);

        for (int i = 0; i < iterations; i++) 
        {
            x = Random.Range(0, lightGrid.GetLength(0));
            y = Random.Range(0, lightGrid.GetLength(1));
            ButtonPressed(new Vector2Int(x, y));
        }
    }

    public void ButtonPressed(Vector2Int pos) 
    {
        UpdateLightState(pos);

        TryUpdateNeighborLight(pos, Vector2Int.down); // Up
        TryUpdateNeighborLight(pos, Vector2Int.right); // Right
        TryUpdateNeighborLight(pos, Vector2Int.up); // Down
        TryUpdateNeighborLight(pos, Vector2Int.left); // Left
    }

    private void TryUpdateNeighborLight(Vector2Int pressedPos, Vector2Int directionVector) 
    {
        if (directionVector == Vector2Int.up) // Actually, down in grid
        {
            if (pressedPos.y >= lightGrid.GetLength(1) - 1)
                return;
        }
        else if (directionVector == Vector2Int.right)
        {
            if (pressedPos.x >= lightGrid.GetLength(0) - 1)
                return;
        }
        else if (directionVector == Vector2Int.down) // Actually, up in grid
        {
            if (pressedPos.y <= 0)
                return;
        }
        else // Left
        {
            if (pressedPos.x <= 0)
                return;
        }

        UpdateLightState(pressedPos + directionVector);
    }

    private void UpdateLightState(Vector2Int pos) 
    {
        lightStatus[pos.x, pos.y] = !lightStatus[pos.x, pos.y];
        lightGrid[pos.x, pos.y].SetLight(lightStatus[pos.x, pos.y]);
    }

}
