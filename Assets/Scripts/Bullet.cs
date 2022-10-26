using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    //탄환 이동 속도
    public float moveSpeed = 70f;

    //탄환 타격 이펙트 프리팹
    public GameObject impactEffectPrefab;
        
    public int attackDamage = 50;

    //터렛의 락온된 타겟을 탄환의 타겟으로 가져온다
    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        //타겟이 없어지면, 다른 탄환에 의해 죽으면
        //목표한 타겟이 사라지면 탄환 처리(킬)
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //타겟으로 탄환을 이동시킨다
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * moveSpeed;
        //타겟까지의 거리가 현재 프레임에서 이동할 거리보다 작으면 충돌했다고 판정
        if(dir.magnitude < distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
        transform.LookAt(target.position); //타겟 방향을 바라본다
    }

    private void HitTarget()
    {
        //Hit Effect 효과
        GameObject eff = (GameObject)Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        Destroy(eff, 3f);

        //타격 데미지
        Damage(target);

        //탄환 오브젝트 킬
        Destroy(gameObject);
    }

    private void Damage(Transform _target)
    {
        //타겟 오브젝트 킬
        _target.GetComponent<Enemy>().TakeDamage(attackDamage);
    }

}
