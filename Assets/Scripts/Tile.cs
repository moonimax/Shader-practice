using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public GameObject turret;

    //���콺�� Ÿ������ �÷����� ����Ǵ� ��
    public Color hoverColor;
    //���콺�� Ÿ������ �÷����� ����Ǵ� ��
    public Color moneyColor;
    //Ÿ���� �ʱ� ��
    private Color startColor;

    public Material hoverMaterial;
    public Material moneyMaterial;
    private Material startMaterial;

    //������Ʈ Renderer
    Renderer rend;

    public Vector3 offsetPos;

    private BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        //Renderer ��ü �ҷ��ͼ� �ʱ�ȭ
        rend = gameObject.GetComponent<Renderer>();
        //Ÿ���� �ʱ� �� ��������
        startColor = rend.material.color;
        //Ÿ���� �ʱ� ���͸��� ��������
        //startMaterial = rend.material;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;// + offsetPos;
    }

    private void OnMouseDown()
    {
        //Ÿ�� ���� ��ư(UI)�� ���� ���
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        //����� �ͷ��� �ִ��� �˻�
        if (!buildManager.CanBuild)
        {
            Debug.Log("����� �ͷ��� �����ϴ�");
            return;
        }

        //Ÿ������ �ͷ��� ��ġ�Ǿ����� �˻�
        if (turret != null)
        {
            Debug.Log("Ÿ������ �ͷ��� �־� ��ġ�� �� �����ϴ�");
            return;
        }

        //�⺻ �ͷ��� ��ġ�Ѵ�
        BuildManager.instance.OnBuildTurret(this);
    }

    private void OnMouseEnter()
    {   
        //����� �ͷ��� �ִ��� �˻�
        if (!buildManager.CanBuild)
            return;
        //Ÿ������ �ͷ��� ��ġ�Ǿ����� �˻�
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
