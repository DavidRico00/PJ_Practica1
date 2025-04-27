using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public float Speed, attackRange;
    private float horizontal, vertical, lessSpeed=0.15f;

    private Rigidbody2D rb2D;
    private Animator animator;
    private bool atacando = false;
    public LayerMask enemyLayer;

    private int score = 0;  

    private ControladorGlobalPuntación controladorGlobalPuntación;

    private string nombre = "Jugador_1";

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controladorGlobalPuntación = GetComponent<ControladorGlobalPuntación>();

    }

    void Update()
    {
        if      (Input.GetKey(KeyCode.A))   horizontal = -1.0f;
        else if (Input.GetKey(KeyCode.D))   horizontal = 1.0f;
        else                                horizontal = 0.0f;

        if      (Input.GetKey(KeyCode.W))   vertical = 1.0f;
        else if (Input.GetKey(KeyCode.S))   vertical = -1.0f;
        else                                vertical = 0.0f;

        SetMovementAnimation();

        if      (Input.GetKey(KeyCode.UpArrow) && !atacando)        Attack(12);
        else if (Input.GetKey(KeyCode.DownArrow) && !atacando)      Attack(6);
        else if (Input.GetKey(KeyCode.LeftArrow) && !atacando)      Attack(9);
        else if (Input.GetKey(KeyCode.RightArrow) && !atacando)     Attack(3);
    }

    private void SetMovementAnimation()
    {
        if      (vertical > 0)     animator.SetInteger("walking", 12);
        else if (vertical < 0)     animator.SetInteger("walking", 6);
        else if (horizontal > 0)   animator.SetInteger("walking", 3);
        else if (horizontal < 0)   animator.SetInteger("walking", 9);
        else                       animator.SetInteger("walking", 0);
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(horizontal) == 1.0f && Mathf.Abs(vertical) == 1.0f)
            rb2D.linearVelocity = new Vector2(horizontal * (Speed-lessSpeed), vertical * (Speed-lessSpeed));
        else
            rb2D.linearVelocity = new Vector2(horizontal * Speed, vertical * Speed);
    }

    void Attack(int direccion)
    {
        Debug.Log("Atacando en direccion: " + direccion);
        atacando = true;
        animator.SetInteger("attack", direccion);
        LanzarRaycast(direccion);
    }

    void NoAttack()
    {
        animator.SetInteger("attack", 0);
        animator.SetInteger("walking", 0);
        Invoke("ResetAttack", 0.2f);
    }

    void ResetAttack()
    {
        atacando = false;
    }

    private Vector2 boxSize = new Vector2(0.48f, 0.48f);
        
    void LanzarRaycast(int direccion)
    {
        Vector2 direction = Vector2.zero;

        switch (direccion)
        {
            case 12: direction = Vector2.up; break;
            case 6: direction = Vector2.down; break;
            case 3: direction = Vector2.right; break;
            case 9: direction = Vector2.left; break;
        }

        Vector2 boxCenter = (Vector2)transform.position + direction * (attackRange / 2);

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, enemyLayer);

        if (hit != null)
        {
            GameObject enemigoGolpeado = hit.gameObject;
            enemigoGolpeado.GetComponent<EnemigoScript>().RecibirGolpe(1);

            Debug.Log("¡Enemigo golpeado en área! Objeto: " + enemigoGolpeado);
        }
        else
        {
            Debug.Log("No hay enemigo en el área de ataque.");
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Vector2 direction = Vector2.zero;
            // Aquí deberías guardar la última dirección usada si quieres mostrarlo en tiempo real
            direction = Vector2.up; // Por ejemplo, hacia arriba
            Vector2 boxCenter = (Vector2)transform.position + direction * (attackRange / 2);
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
    }

    public int vidas;

    public void RecibirGolpe(int dano)
    {
        vidas -= dano;
        score -= 20;
        if (vidas <= 0)
        {
            score -= 50;
            Guardar();

            animator.SetBool("dead", true);
            Invoke("SetDeadFalse", 0.1f);
        }
        Debug.Log("Recibido daño: " + dano + " | Vidas restantes: " + vidas);
    }

    void SetDeadFalse()
    {
        animator.SetBool("dead", false);
    }

    public void SumarPuntos(int puntos)
    {
        score += puntos;
        Debug.Log("Puntos totales: " + score);
    }

    public void Guardar()
    {
        Debug.Log("Guardando datos del jugador: " + nombre + " con " + score + " puntos.");
        controladorGlobalPuntación.AddPlayerRecord(nombre, score); 
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int numEnemigos;
    public GameObject paredSP, paredCP;
    public void CambiarPared()
    {
        numEnemigos--;
        if (numEnemigos == 0)
        {
            paredSP.SetActive(true);
            paredCP.SetActive(false);
        }
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(3);
    }
}
