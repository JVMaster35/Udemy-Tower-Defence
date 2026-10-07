using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    //public float timeBetweenBombs;

    public Transform spawnPoint;

    public BombProjectile theBomb;

    private float bombCounter;
    private Transform target;
    private Tower theTower;

    // Start is called before the first frame update
    void Start()
    {
        theTower = GetComponent<Tower>();

        bombCounter = theTower.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        bombCounter -= Time.deltaTime;

        if (theTower.enemiesInRange.Count > 0)
        {
            if(bombCounter <= 0)
            {
                float mindDistance = theTower.range + 1;

                foreach (EnemyController enemy in theTower.enemiesInRange)
                {
                    if (enemy != null)
                    {
                        float distance = Vector3.Distance(transform.position, enemy.transform.position);

                        if (distance < mindDistance)
                        {
                            mindDistance = distance;
                            target = enemy.transform;
                        }
                    }
                }

                bombCounter = theTower.fireRate;

                BombProjectile newBomb = Instantiate(theBomb, spawnPoint.position, Quaternion.identity);
                newBomb.targetPoint = target.position;
            }
            else
            {
                target = null;
            }
        }
    }
}
