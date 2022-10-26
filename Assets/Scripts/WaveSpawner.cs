using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //������ enemyPrefab
    public Transform enemyPrefab;

    //������ ��ġ : ������ ��ġ�� �����´�
    public Transform startPoint;

    //Ÿ�̸� - 5��
    public float timerWaves = 5.0f;
    private float countdown = 3f;

    //���̺� ī����
    private int waveCount = 0;

    //ī��Ʈ �ٿ� UI �ؽ�Ʈ
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //5�� Ÿ�̸�
        /*countdown += Time.deltaTime;
        if(countdown >= timerWaves)
        {
            //���๮
            Debug.Log("���̺� ����");

            countdown = 0f;
        }*/

        //5�� Ÿ�̸�       
        if (countdown <= 0)
        {
            //���๮
            StartCoroutine(SpawnWaves());

            countdown = timerWaves;
        }
        countdown -= Time.deltaTime;

        //countdownText UI�� countdown �� ����
        //countdownText.text = Mathf.Round(countdown).ToString();
        countdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWaves()
    {
        waveCount++;
        for (int i = 0; i < waveCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        //Debug.Log($"waveCount : {waveCount}");
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, startPoint.position, Quaternion.identity);
    }
}
