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
        //W키 값 받기 - 앞으로 이동
        if (Input.GetKey(KeyCode.W))// || Input.mousePosition.y > (Screen.height - 10))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        }
        //S키 값 받기 - 뒤로 이동
        else if (Input.GetKey(KeyCode.S))// || Input.mousePosition.y < 10)
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        }
        //A키 값 받기 - 왼쪽으로 이동
        else if (Input.GetKey(KeyCode.A))// || Input.mousePosition.x < 10)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
        }
        //D키 값 받기 - 오른쪽으로 이동
        else if (Input.GetKey(KeyCode.D))// || Input.mousePosition.x > (Screen.width - 10))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }

        //마우스 휠을 스크롤하면 화면이 줌인 줌아웃
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * Time.deltaTime * scrollSpeed;
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //최소,최대값 적용
        transform.position = pos;
    }
}
