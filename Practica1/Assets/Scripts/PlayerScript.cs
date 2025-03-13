using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float Speed;
    private float h, v;

    private Rigidbody2D rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if(h < 0.0f) transform.localScale = new Vector3(-1, 1, 1);
        else if (h > 0.0f) transform.localScale = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(h * Speed, v * Speed);
    }
}
