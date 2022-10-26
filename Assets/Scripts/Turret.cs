using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Enemy Enemy;
    //타겟
    private GameObject target;

    [Header("Attributes")]
    //터렛 공격 범위
    public float attackRange = 15;
    //발사 타이머 - 1초
    public float timerFire = 1f;
    private float fireCountdown = 0f;
    //탄환 프리팹
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool isLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserimpact;
    public Light impactLight;

    [Header("Unity Field")]
    //회전관리 오브젝트 객체 가져오기
    public Transform partToRoate;
    //발사 위치
    public Transform firePoint;
    //enemy 프리팹의 태그
    public string enemyTag = "Enemy";
    //터렛 회전 속도
    public float turnSpeed = 10f;

    public float damageTimeOver = 30;
    public float slowsp = 0.4f;

    private void Start()
    {
        //UpdateTarget 메소드를 0.5초 마다 반복해서 호출
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //enemy가 attackRange안에 1마리도 존재하지 않을때 return; 아래 코드를 실행하지 않는다
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
            //발사타이머 - 1초
            if (fireCountdown <= 0f)
            {
                //실행문 - 탄환 발사
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

    //0.5초마다 거리가 가장 가까운 적을 찾아 타겟 서치
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //최단거리 구하는 초기값은 max 값
        float shortDistance = Mathf.Infinity;
        //최단거리의 Enemy
        GameObject nearEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            //거리를 비교해서 최단거리에 있는 적을 판별
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
