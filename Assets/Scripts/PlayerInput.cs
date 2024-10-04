using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Restart);
        }
    }

}
