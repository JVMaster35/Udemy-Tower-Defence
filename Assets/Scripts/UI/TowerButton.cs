using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public Tower towerToPlace;

    public void SelectTower()
    {
        if(LevelManager.instance.levelActive)
        {
            TowerManager.instance.StartTowerPlacement(towerToPlace);
        }
    }
}
