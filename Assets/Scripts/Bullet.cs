using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    //źȯ �̵� �ӵ�
    public float moveSpeed = 70f;

    //źȯ Ÿ�� ����Ʈ ������
    public GameObject impactEffectPrefab;
        
    public int attackDamage = 50;

    //�ͷ��� ���µ� Ÿ���� źȯ�� Ÿ������ �����´�
    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        //Ÿ���� ��������, �ٸ� źȯ�� ���� ������
        //��ǥ�� Ÿ���� ������� źȯ ó��(ų)
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Ÿ������ źȯ�� �̵���Ų��
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * moveSpeed;
        //Ÿ�ٱ����� �Ÿ��� ���� �����ӿ��� �̵��� �Ÿ����� ������ �浹�ߴٰ� ����
        if(dir.magnitude < distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
        transform.LookAt(target.position); //Ÿ�� ������ �ٶ󺻴�
    }

    private void HitTarget()
    {
        //Hit Effect ȿ��
        GameObject eff = (GameObject)Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        Destroy(eff, 3f);

        //Ÿ�� ������
        Damage(target);

        //źȯ ������Ʈ ų
        Destroy(gameObject);
    }

    private void Damage(Transform _target)
    {
        //Ÿ�� ������Ʈ ų
        _target.GetComponent<Enemy>().TakeDamage(attackDamage);
    }

}
