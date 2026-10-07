using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    [HideInInspector]
    public float speedMod = 1f;
    public float timeBetweenAttacks, damagePerAttack;

    public float flightHeight;
    public bool isFlying;

    private NodePath path;
    private Castle theCastle;

    private int currentNode;
    private bool endReached;
    private float attackCounter;
    private int selectedSiegePoint;

    // Start is called before the first frame update
    void Start()
    {
        if (path == null)
        {
            path = FindObjectOfType<NodePath>();
        }

        if(theCastle == null)
        {
            theCastle = FindObjectOfType<Castle>();
        }

        attackCounter = timeBetweenAttacks;

        if(isFlying)
        {
            transform.position += Vector3.up * flightHeight;
            currentNode = path.pathNodes.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.levelActive)
        {
            if (!endReached)
            {
                transform.LookAt(path.pathNodes[currentNode]);

                if (!isFlying)
                {

                    transform.position = Vector3.MoveTowards(transform.position, path.pathNodes[currentNode].position, moveSpeed * Time.deltaTime * speedMod);

                    if (Vector3.Distance(transform.position, path.pathNodes[currentNode].position) < 0.1f)
                    {
                        currentNode += 1;
                        if (currentNode >= path.pathNodes.Length)
                        {
                            endReached = true;
                            selectedSiegePoint = Random.Range(0, theCastle.siegePoints.Length);
                        }
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, path.pathNodes[currentNode].position + (Vector3.up * flightHeight), moveSpeed * Time.deltaTime * speedMod);

                    if (Vector3.Distance(transform.position, path.pathNodes[currentNode].position) < 0.1f)
                    {
                        currentNode += 1;
                        if (currentNode >= path.pathNodes.Length)
                        {
                            endReached = true;
                            selectedSiegePoint = Random.Range(0, theCastle.siegePoints.Length);
                        }
                    }
                }
            }
            else
            {
                if(!isFlying)
                {
                    transform.position = Vector3.MoveTowards(transform.position, theCastle.siegePoints[selectedSiegePoint].position, moveSpeed * Time.deltaTime * speedMod);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, theCastle.siegePoints[selectedSiegePoint].position + (Vector3.up * flightHeight), moveSpeed * Time.deltaTime * speedMod);
                }

                attackCounter -= Time.deltaTime;

                if (attackCounter <= 0)
                {
                    attackCounter = timeBetweenAttacks;

                    theCastle.TakeDamage(damagePerAttack);
                }
            }
        }
    }

    public void Setup(Castle newCastle, NodePath newPath)
    {
        theCastle = newCastle;
        path = newPath;
    }
}
