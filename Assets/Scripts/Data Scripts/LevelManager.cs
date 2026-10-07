using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool levelActive;

    public string nextLevel;

    public List<EnemyHealthController> activeEnemies = new List<EnemyHealthController>();

    private bool levelVictory;

    private Castle[] theCastles;
    //private SimpleEnemySpawner enemySpawner;
    private EnemyWaveSpawner[] waveSpawners;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCastles = FindObjectsOfType<Castle>();
        //enemySpawner = FindObjectOfType<SimpleEnemySpawner>();
        waveSpawners = FindObjectsOfType<EnemyWaveSpawner>();

        AudioManager.instance.PlayBGM();

        levelActive = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (levelActive)
        {
            float totalCastleHealth = 0;
            foreach(Castle cas in theCastles)
            {
                totalCastleHealth += cas.currentHealth;
            }

            if(totalCastleHealth <= 0)
            {
                levelActive = false;
                levelVictory = false;
                UIController.instance.levelFailedScreen.SetActive(true);
                UIController.instance.towerBtns.SetActive(false);
            }
        }

        bool wavesComplete = true;
        foreach(EnemyWaveSpawner waveSpawn in waveSpawners)
        {
            if(waveSpawn.wavesToSpawn.Count > 0)
            {
                wavesComplete = false;
            }
        }

        if(activeEnemies.Count == 0 && wavesComplete) 
        {
            levelActive = false;
            levelVictory = true;
            UIController.instance.levelCompletionScreen.SetActive(true);
            UIController.instance.towerBtns.SetActive(false);

        }

        if(!levelActive) 
        {
            UIController.instance.CloseTowerUpgradePanel();

            UIController.instance.levelFailedScreen.SetActive(!levelVictory);
            UIController.instance.towerBtns.SetActive(levelVictory);
        }
    }
}
