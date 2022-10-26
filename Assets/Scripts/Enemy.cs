using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�̵��� ��ǥ ����
    private Transform target;
    //Enemy�� �̵��ӵ�
    public float moveSpeed = 10f;
    private float Startspeed = 10f;
    //points �迭�� �ε��� ����
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
        //points �迭�� �ε��� ���� �ʱ�ȭ
        wayPointsIndex = 0;
        //WayPoints Ŭ������ ����ƽ ���� �����ͼ� target �ʱ�ȭ
        target = WayPoints.points[wayPointsIndex];        
    }

    // Update is called once per frame
    void Update()
    {
        //[1]������ ���Ѵ� - Vector3 ������� = ��ǥ��ġ - ������ġ
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);

        //[2]��ǥ���� ���� ���� : Ÿ�ٰ� �ڽŰ��� �Ÿ��� ���� ���������Ѵ�
        float dist = Vector3.Distance(transform.position, target.position);
        if(dist <= 0.2f)
        {            
            GetNextPoint();
        }
    }

    //������ ���� ����Ʈ�� ������ �����ͼ� Ÿ�� ����
    private void GetNextPoint()
    {
        //���� �Ǻ��ؼ� �����ϸ� Enemy ������Ʈ Destroy
        if (wayPointsIndex == WayPoints.points.Length - 1)
        {
            EndPoint();
            return; //�޼ҵ忡�� return; ������ �޼ҵ� ����
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

        //Hit Effect ȿ��
        GameObject eff = (GameObject)Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(eff, 3f);

        Destroy(gameObject);
    }

    public void Slowenemy(float pct)
    {
        moveSpeed = Startspeed * (1f - pct);
    }
}
