using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float Speed;
    private float hor, ver, lessSpeed=0.15f;

    private Rigidbody2D rb2D;
    private Animator animator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");*/
        
        if(Input.GetKey(KeyCode.A)) hor = -1.0f;
        else if(Input.GetKey(KeyCode.D)) hor = 1.0f;
        else hor = 0.0f;

        if(Input.GetKey(KeyCode.W)) ver = 1.0f;
        else if(Input.GetKey(KeyCode.S)) ver = -1.0f;
        else ver = 0.0f;

        if(hor < 0.0f) transform.localScale = new Vector3(-0.4f, 0.4f, 1);
        else if (hor > 0.0f) transform.localScale = new Vector3(0.4f, 0.4f, 1);

        if(ver > 0)
            animator.SetInteger("walking", 2);
        else if(ver < 0)
            animator.SetInteger("walking", -1);
        else if(hor != 0)
            animator.SetInteger("walking", 1);
        else
            animator.SetInteger("walking", 0);

    }

    void FixedUpdate()
    {
        if(Mathf.Abs(hor) == 1.0f && Mathf.Abs(ver) == 1.0f)
            rb2D.linearVelocity = new Vector2(hor * (Speed-lessSpeed), ver * (Speed-lessSpeed));
        else
            rb2D.linearVelocity = new Vector2(hor * Speed, ver * Speed);

    }
}
