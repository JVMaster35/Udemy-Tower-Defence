using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    public int currentRangeUpgrade;
    public int currentRoFUpgrade;

    public bool hasRangeUpgrade = true;
    public bool hasRoFUpgrade = true;

    [TextArea]
    public string rateOfFireTxt;

    public UpgradeStage[] upgradeRange;
    public UpgradeStage[] upgradeRoF;

    private Tower theTower;

    // Start is called before the first frame update
    void Start()
    {
        theTower = GetComponent<Tower>();


    }

    public void UpgradeRange()
    {
        theTower.range = upgradeRange[currentRangeUpgrade].amount;
        currentRangeUpgrade++;

        if(currentRangeUpgrade >= upgradeRange.Length)
        {
            hasRangeUpgrade = false;
        }
    }

    public void UpgradeRoF()
    {
        theTower.fireRate = upgradeRoF[currentRoFUpgrade].amount;
        currentRoFUpgrade++;

        if (currentRoFUpgrade >= upgradeRoF.Length)
        {
            hasRoFUpgrade = false;
        }
    }
}

[System.Serializable]
public class UpgradeStage
{
    public float amount;
    public int cost;
}
