using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Animator anim;

    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;

    private bool isEye;

    public GameObject Enemy;

    private bool Movement;

    void Start()
    {
        isEye = true;
        Movement = false;

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Enemy = GameObject.Find("Enemy");
        Enemy.GetComponent<PlayerMovement>().enabled = false;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

            if (horizontal > 0)
            {
             transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (horizontal < 0)
            {
             transform.eulerAngles = new Vector3(0, 180, 0);
            }

        if (isEye)
        {

                 Movement = horizontal != 0;
                 anim.SetBool("Movement", Movement);

        }

        Debug.Log("update: " + isEye);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isEye = false;
            Destroy(gameObject);
            Enemy.GetComponent<PlayerMovement>().enabled = true;
            Debug.Log("collision: " + isEye);
        }
    }
}