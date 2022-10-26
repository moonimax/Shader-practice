using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //이동할 목표 지점
    private Transform target;
    //Enemy의 이동속도
    public float moveSpeed = 10f;
    private float Startspeed = 10f;
    //points 배열의 인덱스 변수
    private int wayPointsIndex = 0;

    public float health = 100;
    private float startenemyhealth = 100;

    public int rewardGold = 50;

    public GameObject deathEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Startspeed;
        health = startenemyhealth;
        //points 배열의 인덱스 변수 초기화
        wayPointsIndex = 0;
        //WayPoints 클래스의 스태틱 변수 가져와서 target 초기화
        target = WayPoints.points[wayPointsIndex];        
    }

    // Update is called once per frame
    void Update()
    {
        //[1]방향을 구한다 - Vector3 진행방향 = 목표위치 - 현재위치
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);

        //[2]목표지점 도착 판정 : 타겟과 자신과의 거리를 구해 도착판정한다
        float dist = Vector3.Distance(transform.position, target.position);
        if(dist <= 0.2f)
        {            
            GetNextPoint();
        }
    }

    //도착시 다음 포인트의 정보를 가져와서 타겟 설정
    private void GetNextPoint()
    {
        //종점 판별해서 도착하면 Enemy 오브젝트 Destroy
        if (wayPointsIndex == WayPoints.points.Length - 1)
        {
            EndPoint();
            return; //메소드에서 return; 만나면 메소드 종료
        }
        
        wayPointsIndex++;
        target = WayPoints.points[wayPointsIndex];
    }

    private void EndPoint()
    {
        PlayerStat.lives--;

        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        //PlayerStat.money += rewardGold;
        PlayerStat.AddMoney(rewardGold);

        //Hit Effect 효과
        GameObject eff = (GameObject)Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(eff, 3f);

        Destroy(gameObject);
    }

    public void Slowenemy(float pct)
    {
        moveSpeed = Startspeed * (1f - pct);
    }
}
