using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public TMP_Text goldTxt;
    public TMP_Text waveTxt;

    public GameObject goldWarning;
    public GameObject levelCompletionScreen, levelFailedScreen;
    public GameObject towerBtns;
    public GameObject pauseScreen;

    public TowerUpgradePanel towerUpgradePanel;

    public string levelSelectionScene, mainMenuScene;

    void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void PauseUnPause()
    {
        if(pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LevelSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelectionScene);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(LevelManager.instance.nextLevel);
        Debug.Log("Next Level!");
    }

    public void OpenTowerUpgradePanel()
    {
        if(LevelManager.instance.levelActive)
        {
            towerUpgradePanel.gameObject.SetActive(true);
            towerUpgradePanel.SetupPanel();
        }
    }

    public void CloseTowerUpgradePanel()
    {
        towerUpgradePanel.gameObject.SetActive(false);

        if (TowerManager.instance.selectedTower != null)
        {

            TowerManager.instance.selectedTower.rangeModel.SetActive(false);

            TowerManager.instance.selectedTower = null;

            TowerManager.instance.selectedTowerEffect.SetActive(false);
        }

        goldWarning.SetActive(false);
    }
}
