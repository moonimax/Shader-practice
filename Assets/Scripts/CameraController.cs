using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 30f;

    public float scrollSpeed = 5f;
    private float minY = 15f;
    private float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
        //WŰ �� �ޱ� - ������ �̵�
        if (Input.GetKey(KeyCode.W))// || Input.mousePosition.y > (Screen.height - 10))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        }
        //SŰ �� �ޱ� - �ڷ� �̵�
        else if (Input.GetKey(KeyCode.S))// || Input.mousePosition.y < 10)
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        }
        //AŰ �� �ޱ� - �������� �̵�
        else if (Input.GetKey(KeyCode.A))// || Input.mousePosition.x < 10)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
        }
        //DŰ �� �ޱ� - ���������� �̵�
        else if (Input.GetKey(KeyCode.D))// || Input.mousePosition.x > (Screen.width - 10))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }

        //���콺 ���� ��ũ���ϸ� ȭ���� ���� �ܾƿ�
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * Time.deltaTime * scrollSpeed;
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //�ּ�,�ִ밪 ����
        transform.position = pos;
    }
}
