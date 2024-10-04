using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoardManager : Singleton<LightBoardManager>
{

    [Header("Light Board Settings")]
    [SerializeField] private Transform lightBoardObj;
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
    }

    private void Start()
    {
        ResetLightStates();
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
            if (pressedPos.y == lightGrid.GetLength(1) - 1)
                return;
        }
        else if (directionVector == Vector2Int.right)
        {
            if (pressedPos.x == lightGrid.GetLength(0) - 1)
                return;
        }
        else if (directionVector == Vector2Int.down) // Actually, up in grid
        {
            if (pressedPos.y == 0)
                return;
        }
        else // Left
        {
            if (pressedPos.x == 0)
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
