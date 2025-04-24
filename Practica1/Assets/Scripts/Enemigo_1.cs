using UnityEngine;
using Pathfinding;

public class EnemyUnstuckHandler : MonoBehaviour
{
    public Transform player;
    public float stuckCheckInterval = 0.5f;
    public float movementThreshold = 0.01f;
    public float stuckTimeLimit = 2f;
    public float playerDistanceTrigger = 2f;

    private Vector2 lastPosition;
    private float stuckTimer = 0f;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        lastPosition = transform.position;

        InvokeRepeating("CheckIfStuck", stuckCheckInterval, stuckCheckInterval);
    }

    void CheckIfStuck()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);

        if (distanceMoved < movementThreshold)
        {
            stuckTimer += stuckCheckInterval;
        }
        else
        {
            stuckTimer = 0f;
        }

        lastPosition = transform.position;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (stuckTimer >= stuckTimeLimit && distanceToPlayer < playerDistanceTrigger)
        {
            PerformUnstuckAction();
            stuckTimer = 0f;
        }
    }

    void PerformUnstuckAction()
    {
        int randomAction = Random.Range(0, 3);

        switch (randomAction)
        {
            case 0:
                Debug.Log("Unstuck: Reactivando IA");
                aiPath.enabled = false;
                destinationSetter.enabled = false;
                Invoke("ReactivateAI", 0.1f);
                break;

            case 1:
                Debug.Log("Unstuck: Pequeño empujón");
                transform.position += (Vector3)(Random.insideUnitCircle.normalized * 0.3f);
                break;

            case 2:
                Debug.Log("Unstuck: Redirigiendo");
                if (destinationSetter != null)
                {
                    destinationSetter.target = player;
                }
                break;
        }
    }

    void ReactivateAI()
    {
        aiPath.enabled = true;
        destinationSetter.enabled = true;
    }
}

