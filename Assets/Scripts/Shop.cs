using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    public TurretBlueprint basicTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void BasicTurretSelect()
    {
        //Debug.Log("기본 터렛을 구매 했습니다.");
        buildManager.SetTurretToBuild(basicTurret);
    }

    public void MissileLauncherSelect()
    {
        //Debug.Log("다른 터렛을 구매 했습니다.");
        buildManager.SetTurretToBuild(missileLauncher);
    }

    public void LaserBeamerSelect()
    {
        //Debug.Log("다른 터렛을 구매 했습니다.");
        buildManager.SetTurretToBuild(laserBeamer);
    }
}
