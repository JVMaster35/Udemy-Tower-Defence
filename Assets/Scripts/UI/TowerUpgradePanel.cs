using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUpgradePanel : MonoBehaviour
{
    public GameObject rangeBTN, rofBTN;
    public TMP_Text rangeTxt, rofTxt;

    public void SetupPanel()
    {
        TowerUpgradeController upgradeController = TowerManager.instance.selectedTower.upgradeController;

        if (TowerManager.instance.selectedTower.upgradeController.hasRangeUpgrade)
        {
            rangeTxt.text = "Upgrade Range (" + TowerManager.instance.selectedTower.upgradeController.upgradeRange[upgradeController.currentRangeUpgrade].cost + "G)";

            rangeBTN.SetActive(true);
        }
        else
        {
            rangeBTN.SetActive(false);
        }

        if (TowerManager.instance.selectedTower.upgradeController.hasRoFUpgrade)
        {
            rofTxt.text = upgradeController.rateOfFireTxt + "(" + TowerManager.instance.selectedTower.upgradeController.upgradeRoF[upgradeController.currentRoFUpgrade].cost + "G)";

            rofBTN.SetActive(true);
        }
        else
        {
            rofBTN.SetActive(false);    
        }
    }

    public void RemoveTower()
    {
        CurrencyManager.instance.SpendCurrency(-50);

        Destroy(TowerManager.instance.selectedTower.gameObject);

        UIController.instance.CloseTowerUpgradePanel();

        AudioManager.instance.PlaySFX(9);
    }

    public void UpgradeRange()
    {
        TowerUpgradeController upgradeController = TowerManager.instance.selectedTower.upgradeController;
        if (upgradeController.hasRangeUpgrade)
        {
            if (CurrencyManager.instance.SpendCurrency(upgradeController.upgradeRange[upgradeController.currentRangeUpgrade].cost))
            {
                upgradeController.UpgradeRange();

                SetupPanel();

                UIController.instance.goldWarning.SetActive(false);

                AudioManager.instance.PlaySFX(10);
            }
            else
            {
                UIController.instance.goldWarning.SetActive(true);
            }
        }
    }

    public void UpgradeRoF()
    {
        TowerUpgradeController upgradeController = TowerManager.instance.selectedTower.upgradeController;
        if (upgradeController.hasRoFUpgrade)
        {
            if (CurrencyManager.instance.SpendCurrency(upgradeController.upgradeRoF[upgradeController.currentRoFUpgrade].cost))
            {
                upgradeController.UpgradeRoF();

                SetupPanel();

                UIController.instance.goldWarning.SetActive(false);

                AudioManager.instance.PlaySFX(10);
            }
            else
            {
                UIController.instance.goldWarning.SetActive(true);
            }
        }
    }
}
