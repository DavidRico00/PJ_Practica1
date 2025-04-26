using UnityEngine;
using Pathfinding;

public class EnemigoScript : MonoBehaviour
{
    public GameObject playerGO;
    public Transform player;
    public float stuckCheckInterval = 0.5f, movementThreshold = 0.01f, stuckTimeLimit = 2f, playerDistanceTrigger = 2f;
    public float maxActiveDistance, attackRange = 0.1f;
    private Vector2 lastPosition;
    private float stuckTimer = 0f;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    private Animator animator;

    private bool isAttacking = false;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        lastPosition = transform.position;

        animator = GetComponent<Animator>();

        InvokeRepeating("CheckIfStuck", stuckCheckInterval, stuckCheckInterval);
    }

    void Update()
    {
        if (aiPath.velocity.magnitude > 0f)   animator.SetBool("move", true);
        else                                  animator.SetBool("move", false);

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > maxActiveDistance && aiPath.enabled) DesactivateAI();
        else ReactivateAI();

        if (!aiPath.pathPending)
        {
            if (aiPath.remainingDistance <= attackRange && !isAttacking)
            {
                    Atacar();
            }
        }
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
                aiPath.enabled = false;
                destinationSetter.enabled = false;
                Invoke("ReactivateAI", 0.1f);
                break;

            case 1:
                transform.position += (Vector3)(Random.insideUnitCircle.normalized * 0.3f);
                break;

            case 2:
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

    void DesactivateAI()
    {
        aiPath.enabled = false;
        destinationSetter.enabled = false;
    }

    public int vidas = 2;
    
    public void RecibirGolpe(int dano)
    {
        vidas -= dano;
        if (vidas <= 0)
        {
            playerGO.GetComponent<PlayerScript>().SumarPuntos(100);
            DesactivateAI();
            maxActiveDistance = 0f;
            animator.SetBool("dead", true);
        }
    }

    public void Eliminar()
    {
        Destroy(gameObject, 0f);
    }

    void Atacar()
    {
        isAttacking = true;
        animator.SetBool("attack", true);
        playerGO.GetComponent<PlayerScript>().RecibirGolpe(1);
    }

    public void TerminarAtaque()
    {
        animator.SetBool("attack", false);
        isAttacking = false;
    }

}

