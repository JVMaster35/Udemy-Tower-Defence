using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : MonoBehaviour
{
    //public float timeBetweenShots = 1f;
    private float shotCounter;

    public GameObject projectile;
    public GameObject shotEffect;

    private Tower theTower;

    public Transform firePoint;
    public Transform launchModel;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        theTower = GetComponent<Tower>();

        shotCounter = theTower.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.levelActive)
        {
            if (target != null)
            {
                //launchModel.LookAt(target);
                launchModel.rotation = Quaternion.Slerp(launchModel.rotation, Quaternion.LookRotation(target.position - transform.position), 5f * Time.deltaTime);

                launchModel.rotation = Quaternion.Euler(0f, launchModel.rotation.eulerAngles.y, 0f);
            }

            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0 && target != null)
            {
                shotCounter = theTower.fireRate;

                firePoint.LookAt(target);

                Instantiate(projectile, firePoint.position, firePoint.rotation);
                Instantiate(shotEffect, firePoint.position, firePoint.rotation);
            }

            if (theTower.enemiesUpdated)
            {
                if (theTower.enemiesInRange.Count > 0)
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
                }
                else
                {
                    target = null;
                }
            }
        }

    }
}
