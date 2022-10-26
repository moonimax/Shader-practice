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
        //Debug.Log("�⺻ �ͷ��� ���� �߽��ϴ�.");
        buildManager.SetTurretToBuild(basicTurret);
    }

    public void MissileLauncherSelect()
    {
        //Debug.Log("�ٸ� �ͷ��� ���� �߽��ϴ�.");
        buildManager.SetTurretToBuild(missileLauncher);
    }

    public void LaserBeamerSelect()
    {
        //Debug.Log("�ٸ� �ͷ��� ���� �߽��ϴ�.");
        buildManager.SetTurretToBuild(laserBeamer);
    }
}
