using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public float totalHealth;

    public Slider healthBar;

    public int currencyOnDeath = 50;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = totalHealth;
        healthBar.value = totalHealth;

        LevelManager.instance.activeEnemies.Add(this);

        AudioManager.instance.PlaySFX(7);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    public void TakeDamage(float damageTaken)
    {
        totalHealth -= damageTaken;
        if (totalHealth <= 0 )
        {
            totalHealth = 0;

            CurrencyManager.instance.TakeCurrency( currencyOnDeath );

            LevelManager.instance.activeEnemies.Remove(this);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5);
        }

        healthBar.value = totalHealth;
        healthBar.gameObject.SetActive(true);
    }
}
