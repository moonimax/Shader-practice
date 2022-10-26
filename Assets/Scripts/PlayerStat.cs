using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private const bool isCheat = false;
    public static bool IsCheat { get { return isCheat; } }

    public static int money;
    public int startMoney = 4000;

    public static int lives;
    public int startLives = 10;

    private void Start()
    {
        money = startMoney;
        lives = startLives;
    }

    public static void AddMoney(int value)
    {
        money += value;
    }

    public static bool HasMoney(int value)
    {
        return money >= value;
    }

    public static bool UseMoney(int value)
    {
        if(money < value)
            return false;

        money -= value;
        return true;
    }
}
