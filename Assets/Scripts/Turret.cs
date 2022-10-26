using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Enemy Enemy;
    //Ÿ��
    private GameObject target;

    [Header("Attributes")]
    //�ͷ� ���� ����
    public float attackRange = 15;
    //�߻� Ÿ�̸� - 1��
    public float timerFire = 1f;
    private float fireCountdown = 0f;
    //źȯ ������
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool isLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserimpact;
    public Light impactLight;

    [Header("Unity Field")]
    //ȸ������ ������Ʈ ��ü ��������
    public Transform partToRoate;
    //�߻� ��ġ
    public Transform firePoint;
    //enemy �������� �±�
    public string enemyTag = "Enemy";
    //�ͷ� ȸ�� �ӵ�
    public float turnSpeed = 10f;

    public float damageTimeOver = 30;
    public float slowsp = 0.4f;

    private void Start()
    {
        //UpdateTarget �޼ҵ带 0.5�� ���� �ݺ��ؼ� ȣ��
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //enemy�� attackRange�ȿ� 1������ �������� ������ return; �Ʒ� �ڵ带 �������� �ʴ´�
        if (target == null)
        {
            if (isLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserimpact.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        //target lock on 
        LockOn();

        if (isLaser)
        {
            Laser();
        }
        else
        {
            //�߻�Ÿ�̸� - 1��
            if (fireCountdown <= 0f)
            {
                //���๮ - źȯ �߻�
                Shoot();

                fireCountdown = timerFire;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        Enemy.TakeDamage(damageTimeOver * Time.deltaTime);
        Enemy.Slowenemy(slowsp);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserimpact.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 dir = firePoint.position - target.transform.position;

        laserimpact.transform.rotation = Quaternion.LookRotation(dir);
        laserimpact.transform.position = target.transform.position + dir.normalized * .5f;
    }

    private void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.SeekTarget(target.transform);
    }

    private void LockOn()
    {
        Vector3 dir = target.transform.position - partToRoate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Quaternion targetRotation = Quaternion.Lerp(partToRoate.rotation, lookRotation, Time.deltaTime * turnSpeed);
        Vector3 eulerRotation = targetRotation.eulerAngles;
        partToRoate.rotation = Quaternion.Euler(0f, eulerRotation.y, 0f);
    }

    //0.5�ʸ��� �Ÿ��� ���� ����� ���� ã�� Ÿ�� ��ġ
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //�ִܰŸ� ���ϴ� �ʱⰪ�� max ��
        float shortDistance = Mathf.Infinity;
        //�ִܰŸ��� Enemy
        GameObject nearEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            //�Ÿ��� ���ؼ� �ִܰŸ��� �ִ� ���� �Ǻ�
            if(distance < shortDistance)
            {
                shortDistance = distance;
                nearEnemy = enemy;
            }
        }

        if (nearEnemy != null && shortDistance <= attackRange)
        {
            target = nearEnemy;
            Enemy = target.transform.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
