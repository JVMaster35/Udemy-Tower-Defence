using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public float totalHealth = 100f;
    [HideInInspector]
    public float currentHealth;

    public Slider healthSlider;

    public Transform[] siegePoints;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;

        healthSlider.maxValue = totalHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if(currentHealth <= 0 )
        {
            AudioManager.instance.PlaySFX(3);
            GameOver();
        }
        else
        {
            AudioManager.instance.PlaySFX(4);
        }

        healthSlider.value = currentHealth;
    }

    public void GameOver()
    {
        currentHealth = 0;
        gameObject.SetActive(false);
    }
}
