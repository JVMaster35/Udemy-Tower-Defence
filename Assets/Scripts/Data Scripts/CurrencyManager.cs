using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public int currentCurrency;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.goldTxt.text = currentCurrency.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeCurrency(int amountToTake)
    {
        currentCurrency += amountToTake;
        UIController.instance.goldTxt.text = currentCurrency.ToString();
    }

    public bool SpendCurrency(int amountToPay)
    {
        bool canSpend = false;

        if(amountToPay <= currentCurrency)
        {
            canSpend = true;

            currentCurrency -= amountToPay;
        }

        UIController.instance.goldTxt.text = currentCurrency.ToString();

        return canSpend;
    }
}
