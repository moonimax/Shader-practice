using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        if(PlayerStat.lives <= 0)
        {
            Debug.Log("Game Over!!!");
            isGameOver = true;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            ShowMeTheMoney();
        }
    }

    private void ShowMeTheMoney()
    {
        if (!PlayerStat.IsCheat)
            return;

        PlayerStat.AddMoney(100000);
    }
}
