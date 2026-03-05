using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int count = 8;
    public float spawnInterval = 0.7f;
    public int enemyHp = 5;
    public float enemySpeed = 2.5f;
    public int reward = 5;
}

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance { get; private set; }

    [Header("References")]
    public GameObject enemyPrefab;
    public Transform pathRoot;

    [Header("Waves")]
    public Wave[] waves;
    public float waveInterval = 3f;

    public int CurrentWave { get; private set; } = 0;
    public int TotalWaves => waves != null ? waves.Length : 0;

    Transform[] waypoints;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int count = pathRoot.childCount;
        waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
            waypoints[i] = pathRoot.GetChild(i);

        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            CurrentWave = w + 1;
            Debug.Log($"Wave {CurrentWave} 시작");

            Wave wave = waves[w];

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            // 모든 적이 죽을 때까지 대기
            while (GameObject.FindGameObjectsWithTag("EnemyTag").Length > 0)
                yield return null;

            yield return new WaitForSeconds(waveInterval);
        }

        Debug.Log("ALL WAVES CLEARED!");
    }

    void SpawnEnemy(Wave wave)
    {
        Vector3 spawnPos = waypoints[0].position;
        GameObject e = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        e.tag = "EnemyTag";

        var mover = e.GetComponent<EnemyMover>();
        if (mover != null)
        {
            mover.waypoints = waypoints;
            mover.speed = wave.enemySpeed;
        }

        var hp = e.GetComponent<EnemyHealth>();
        if (hp != null)
        {
            hp.maxHp = wave.enemyHp;
            hp.reward = wave.reward;
        }
    }
}