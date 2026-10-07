using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    //public EnemyController enemyToSpawn;
    public EnemyController[] enemiesToSpawn;

    public Transform spawnPoint;

    public float timeBetweenSpawns;
    private float spawnCounter;

    public int spawnAmount;

    public Castle castle;

    public NodePath path;

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeBetweenSpawns;

        spawnAmount = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAmount > 0 && LevelManager.instance.levelActive)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = timeBetweenSpawns;

                Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPoint.position, spawnPoint.rotation).Setup(castle, path);

                spawnAmount--;
            }
        }
    }
}
