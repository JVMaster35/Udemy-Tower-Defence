using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    public bool enemiesUpdated;

    public float range = 3f;
    public float fireRate;
    public float checkTime = .2f;

    public int cost = 100;

    public GameObject rangeModel;

    [HideInInspector]
    public TowerUpgradeController upgradeController;

    public LayerMask whatIsEnemy;

    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    private float checkCounter;
    private Collider[] colliderInRanges;

    // Start is called before the first frame update
    void Start()
    {
        checkCounter = checkTime;

        upgradeController = GetComponent<TowerUpgradeController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesUpdated = false;

        checkCounter -= Time.deltaTime;

        if (checkCounter <= 0)
        {
            colliderInRanges = Physics.OverlapSphere(transform.position, range, whatIsEnemy);

            enemiesInRange.Clear();
            foreach (Collider col in colliderInRanges)
            {
                enemiesInRange.Add(col.GetComponent<EnemyController>());
            }

            enemiesUpdated = true;
        }

        if(TowerManager.instance.selectedTower== this)
        {
            rangeModel.SetActive(true);
            rangeModel.transform.localScale = new Vector3(range, 1f, range);
        }
    }

    private void OnMouseDown()
    {
        if(LevelManager.instance.levelActive)
        {
            if (TowerManager.instance.selectedTower != null)
            {
                TowerManager.instance.selectedTower.rangeModel.SetActive(false);
            }

            TowerManager.instance.selectedTower = this;

            UIController.instance.OpenTowerUpgradePanel();

            TowerManager.instance.MoveTowerSelectionEffect();
        }
    }
}
