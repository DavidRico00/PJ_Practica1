using UnityEngine;
using Pathfinding;

public class Enemy1Script : MonoBehaviour
{
    public Transform target;
    public float Speed;
    public float nextWaypointDistance = 0.5f; // Distancia m�nima para cambiar de punto

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Buscar el camino al jugador cada 0.5 segundos
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())  // Solo busca si no est� calculando otro camino
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        // Si llegamos al �ltimo punto, no seguir m�s
        if (currentWaypoint >= path.vectorPath.Count)
            return;

        // Mover el enemigo hacia el siguiente punto
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.linearVelocity = direction * Speed;

        // Verificar si llegamos al waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }
}
