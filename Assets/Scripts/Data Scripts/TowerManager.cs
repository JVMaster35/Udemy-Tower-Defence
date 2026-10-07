using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    public Tower activeTower;
    [HideInInspector]
    public Tower selectedTower;

    public Transform indicator;

    public GameObject selectedTowerEffect;

    public bool isPlacing;

    public float topSafePercent = 11.5f;

    public LayerMask whatIsPlacement, whatIsObstacle;

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
        if(isPlacing) 
        {
            indicator.position = GetGridPosition();

            RaycastHit hit;

            if(Input.mousePosition.y > Screen.height * (1f - (topSafePercent/100)))
            {
                indicator.gameObject.SetActive(false);
            }
            else if (Physics.Raycast(indicator.position + new Vector3(0f, -2f, 0f), Vector3.up, out hit, 10f, whatIsObstacle))
            {
                indicator.gameObject.SetActive(false);
            }
            else
            {
                indicator.gameObject.SetActive(true);

                UIController.instance.goldWarning.SetActive(CurrencyManager.instance.currentCurrency < activeTower.cost);

                if (Input.GetMouseButtonDown(0))
                {
                    if (CurrencyManager.instance.SpendCurrency(activeTower.cost))
                    {
                        isPlacing = false;

                        Instantiate(activeTower, indicator.position, activeTower.transform.rotation);
                        indicator.gameObject.SetActive(false);

                        UIController.instance.goldWarning.SetActive(false);

                        AudioManager.instance.PlaySFX(8);
                    }
                }
            }
        }
    }

    public void StartTowerPlacement(Tower towerToPlace)
    {
        activeTower = towerToPlace;

        isPlacing = true;

        Destroy(indicator.gameObject);

        Tower placeTower = Instantiate(activeTower);
        placeTower.enabled = false;
        placeTower.GetComponent<Collider>().enabled = false;
        indicator = placeTower.transform;

        placeTower.rangeModel.SetActive(true);
        placeTower.rangeModel.transform.localScale = new Vector3(placeTower.range, 1f, placeTower.range);
    }

    public Vector3 GetGridPosition()
    {
        Vector3 location = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 200f, whatIsPlacement)) 
        {
            location = hit.point;
        }

        location.y = 0f;

        return location;
    }

    public void MoveTowerSelectionEffect()
    {
        if(selectedTower != null)
        {
            selectedTowerEffect.transform.position = selectedTower.transform.position;
            selectedTowerEffect.SetActive(true);
        }
    }
}
