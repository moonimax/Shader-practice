using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        //앞에서 인스턴스가 생성되어졌는지 체크
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
        
    //빌드하기위한 터렛을 담고 있는 변수
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
            Debug.Log("돈이 부족합니다");
            return;
        }

        PlayerStat.money -= turretToBuild.cost;

        Vector3 buildPos = tile.GetBuildPosition() + turretToBuild.offsetPos;
        GameObject _turret = (GameObject)Instantiate(turretToBuild.prefab, buildPos, Quaternion.identity);
        tile.turret = _turret;

        GameObject eff = (GameObject)Instantiate(buildEffectPrefab, buildPos, Quaternion.identity);   
        Destroy(eff.gameObject, 2.0f);

        Debug.Log($"나머지 돈: {PlayerStat.money}");
    }
   
    public void SetTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

}
