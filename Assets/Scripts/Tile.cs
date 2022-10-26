using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public GameObject turret;

    //마우스를 타일위에 올렸을때 변경되는 색
    public Color hoverColor;
    //마우스를 타일위에 올렸을때 변경되는 색
    public Color moneyColor;
    //타일의 초기 색
    private Color startColor;

    public Material hoverMaterial;
    public Material moneyMaterial;
    private Material startMaterial;

    //오브젝트 Renderer
    Renderer rend;

    public Vector3 offsetPos;

    private BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        //Renderer 객체 불러와서 초기화
        rend = gameObject.GetComponent<Renderer>();
        //타일의 초기 색 가져오기
        startColor = rend.material.color;
        //타일의 초기 메터리얼 가져오기
        //startMaterial = rend.material;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;// + offsetPos;
    }

    private void OnMouseDown()
    {
        //타일 위에 버튼(UI)이 있을 경우
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        //저장된 터렛이 있는지 검사
        if (!buildManager.CanBuild)
        {
            Debug.Log("저장된 터렛이 없습니다");
            return;
        }

        //타일위에 터렛이 설치되었는지 검사
        if (turret != null)
        {
            Debug.Log("타일위에 터렛이 있어 설치할 수 없습니다");
            return;
        }

        //기본 터렛을 설치한다
        BuildManager.instance.OnBuildTurret(this);
    }

    private void OnMouseEnter()
    {   
        //저장된 터렛이 있는지 검사
        if (!buildManager.CanBuild)
            return;
        //타일위에 터렛이 설치되었는지 검사
        if (turret != null)            
            return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
            //rend.material = hoverMaterial;
        }
        else
        {
            rend.material.color = moneyColor;
            //rend.material = moneyMaterial;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
        //rend.material = startMaterial;
    }
}
