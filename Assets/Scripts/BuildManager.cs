using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        //�տ��� �ν��Ͻ��� �����Ǿ������� üũ
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
        
    //�����ϱ����� �ͷ��� ��� �ִ� ����
    private TurretBlueprint turretToBuild;

    public GameObject buildEffectPrefab;

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }

    public bool HasMoney
    {
        get { return PlayerStat.money >= turretToBuild.cost;}
    }

    public void OnBuildTurret(Tile tile)
    {
        if(PlayerStat.money < turretToBuild.cost)
        {
            Debug.Log("���� �����մϴ�");
            return;
        }

        PlayerStat.money -= turretToBuild.cost;

        Vector3 buildPos = tile.GetBuildPosition() + turretToBuild.offsetPos;
        GameObject _turret = (GameObject)Instantiate(turretToBuild.prefab, buildPos, Quaternion.identity);
        tile.turret = _turret;

        GameObject eff = (GameObject)Instantiate(buildEffectPrefab, buildPos, Quaternion.identity);   
        Destroy(eff.gameObject, 2.0f);

        Debug.Log($"������ ��: {PlayerStat.money}");
    }
   
    public void SetTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

}
