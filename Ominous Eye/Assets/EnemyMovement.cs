using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Enemy = GameObject.Find("Enemy");
        Enemy.GetComponent<EnemyMovement>().enabled = false;
    }

    // Update is called once per frame
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
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemy.GetComponent<EnemyMovement>().enabled = true;
        }
    }
}