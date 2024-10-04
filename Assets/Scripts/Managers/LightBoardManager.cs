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

    protected override void Awake()
    {
        base.Awake();

        lightGrid = new LightButton[5,5];
        ReadAllLightButtons();
    }

    private void Start()
    {
        ResetLightStates();
    }

    private void ReadAllLightButtons() 
    {
        int currentRow = 0;
        int currentColumn = 0;

        foreach (Transform row in lightBoardObj)
        {
            foreach (Transform lightButton in row) 
            {
                lightGrid[currentRow, currentColumn] = lightButton.gameObject.GetComponent<LightButton>();
                currentColumn++;
            }
            currentColumn = 0;
            currentRow++;       
        }

    }

    private void ResetLightStates() 
    {
        for (int row = 0; row < lightGrid.GetLength(0); row++) 
        {
            for (int col = 0; col < lightGrid.GetLength(1); col++) 
            {
                lightGrid[row,col].SetLight(false);
            }
        }
    } 

}
