using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightButton : MonoBehaviour
{

    private Image buttonImage;

    private Vector2Int gridPos = Vector2Int.zero;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SetLight(bool isOn) 
    {
        buttonImage.color = isOn ? LightBoardManager.Instance.lightOnColor : LightBoardManager.Instance.lightOffColor;
    }

    public void SetGridPos(Vector2Int pos) => gridPos = pos;

}
