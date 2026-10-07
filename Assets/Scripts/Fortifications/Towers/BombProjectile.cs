using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPoint;

    public float flingSpeed;
    public float damage;
    public float explosionRange;

    public Transform pivotPoint, bombModel;
    public GameObject explosionFX;
    public LayerMask whatIsEnemy;
    //public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = transform.position;

        //targetPoint = target.position;

        Vector3 centerPosition = (transform.position + targetPoint) * .5f;
        transform.position = centerPosition;

        transform.right = targetPoint - transform.position;

        bombModel.transform.position = startPos;

        AudioManager.instance.PlaySFX(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.levelActive)
        {
            pivotPoint.localRotation = Quaternion.RotateTowards(pivotPoint.localRotation, Quaternion.Euler(0f, 0f, 180f), flingSpeed * Time.deltaTime);
            bombModel.rotation = Quaternion.identity;

            if (Vector3.Distance(bombModel.position, targetPoint) < 0.1f)
            {
                Collider[] colliderInRanges = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);

                foreach (Collider col in colliderInRanges)
                {
                    col.GetComponent<EnemyHealthController>().TakeDamage(damage);
                }

                if (explosionFX != null)
                {
                    Instantiate(explosionFX, bombModel.position, Quaternion.identity);
                }

                AudioManager.instance.PlaySFX(0);

                Destroy(gameObject);
            }
        }
    }
}
