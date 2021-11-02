using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    Animator anim;

    Rigidbody2D body;

    float horizontal;
    float vertical;

    private bool isJumping;
    private bool isEnemy;

    private float jumpForce;
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;

    private bool lbMovement;
    private bool lbJump;

    public GameObject PlayerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lbMovement = false;
        lbJump = false;
        isEnemy = false;
        isJumping = false;
        moveSpeed = 3f;
        jumpForce = 40f;
        this.gameObject.GetComponent<EnemyMovement>().enabled = false;

    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = vertical = Input.GetAxisRaw("Vertical");

        lbMovement = moveHorizontal != 0;
        lbJump = moveVertical != 0;
        anim.SetBool("lbMovement", lbMovement);
        anim.SetBool("lbJump", lbJump);

        if (Input.GetKeyDown(KeyCode.E))
        {
            isEnemy = false;
            Destroy(this.gameObject);
            Instantiate(PlayerPrefab);

        }

    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0f || moveHorizontal < 0f)
        {
            body.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if (moveHorizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (moveHorizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (!isJumping && isEnemy)
        {
            body.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isEnemy = true;
            this.gameObject.GetComponent<EnemyMovement>().enabled = true;
        }
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = true;
        }
    }
}