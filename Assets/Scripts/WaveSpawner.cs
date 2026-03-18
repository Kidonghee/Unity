using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance { get; private set; }

    public GameObject enemyPrefab;
    public GameObject bossEnemyPrefab;

    public Transform spawnPoint;
    public Transform pathRoot;

    public float timeBetweenWaves = 5f;

    int waveIndex = 0;
    bool spawning = false;

    public int CurrentWave { get; private set; } = 0;
    public int TotalWaves => waves != null ? waves.Length : 0;

    public bool IsClear { get; private set; } = false;

    [System.Serializable]
    public class Wave
    {
        public int count = 5;
        public float rate = 1f;
        public int enemyHp = 5;
        public int reward = 5;
        public bool isBossWave = false;
    }

    public Wave[] waves;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (waveIndex < waves.Length)
        {
            if (BaseHealth.Instance != null && BaseHealth.Instance.IsGameOver)
                yield break;

            yield return new WaitForSeconds(timeBetweenWaves);

            if (BaseHealth.Instance != null && BaseHealth.Instance.IsGameOver)
                yield break;

            if (!spawning)
            {
                CurrentWave = waveIndex + 1;
                yield return StartCoroutine(SpawnWave(waves[waveIndex]));
                waveIndex++;
            }
        }

        // 마지막 웨이브 적 전부 사라질 때까지 대기
        while (GameObject.FindGameObjectsWithTag("EnemyTag").Length > 0)
        {
            if (BaseHealth.Instance != null && BaseHealth.Instance.IsGameOver)
                yield break;

            yield return null;
        }

        // 게임오버가 아닐 때만 클리어
        if (BaseHealth.Instance != null && !BaseHealth.Instance.IsGameOver)
        {
            IsClear = true;
            Debug.Log("GAME CLEAR");
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawning = true;

        Debug.Log("Wave " + CurrentWave + " 시작");

        for (int i = 0; i < wave.count; i++)
        {
            if (BaseHealth.Instance != null && BaseHealth.Instance.IsGameOver)
            {
                spawning = false;
                yield break;
            }

            SpawnEnemy(wave);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        spawning = false;
    }

    void SpawnEnemy(Wave wave)
    {
        GameObject prefabToSpawn = wave.isBossWave ? bossEnemyPrefab : enemyPrefab;

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("Enemy Prefab 없음");
            return;
        }

        GameObject e = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        e.tag = "EnemyTag";

        EnemyMover mover = e.GetComponent<EnemyMover>();
        if (mover != null)
        {
            mover.waypoints = GetWaypoints();
        }

        EnemyHealth hp = e.GetComponent<EnemyHealth>();
        if (hp != null)
        {
            hp.reward = wave.reward;
            hp.InitHp(wave.enemyHp);
        }
    }

    Transform[] GetWaypoints()
    {
        int count = pathRoot.childCount;
        Transform[] points = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            points[i] = pathRoot.GetChild(i);
        }

        return points;
    }
}