using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float Speed, attackRange;
    private float horizontal, vertical, lessSpeed=0.15f;

    private Rigidbody2D rb2D;
    private Animator animator;
    private bool atacando = false;
    public LayerMask enemyLayer;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        atacando = false;
        animator.SetInteger("attack", 0);
        animator.SetInteger("walking", 0);
    }

    void LanzarRaycast(int direccion)
    {
        Vector2 direction = Vector2.zero;

        switch (direccion)
        {
            case 12: direction = Vector2.up; break;
            case 6: direction =  Vector2.down; break;
            case 3: direction =  Vector2.right; break;
            case 9: direction =  Vector2.left; break;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, enemyLayer);

        if (hit.collider != null)
        {
            Debug.Log("Enemigo golpeado: " + hit.collider.name);
        }
        else
        {
            Debug.Log("No hay enemigo en el rango de ataque.");
        }

        
    }
}
