using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //스폰할 enemyPrefab
    public Transform enemyPrefab;

    //시작점 위치 : 스폰할 위치를 가져온다
    public Transform startPoint;

    //타이머 - 5초
    public float timerWaves = 5.0f;
    private float countdown = 3f;

    //웨이브 카운팅
    private int waveCount = 0;

    //카운트 다운 UI 텍스트
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //5초 타이머
        /*countdown += Time.deltaTime;
        if(countdown >= timerWaves)
        {
            //실행문
            Debug.Log("웨이브 스폰");

            countdown = 0f;
        }*/

        //5초 타이머       
        if (countdown <= 0)
        {
            //실행문
            StartCoroutine(SpawnWaves());

            countdown = timerWaves;
        }
        countdown -= Time.deltaTime;

        //countdownText UI에 countdown 값 대입
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
