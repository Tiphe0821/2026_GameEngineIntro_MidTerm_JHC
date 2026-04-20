using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float jumpForce = 8.0f;
    public float dashForce = 2.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public int playerHp = 3;
    private bool isArmed = false;

    public GameObject[] playerHealthDisplay;
    public StageManager stageManager;

    private Rigidbody2D rb;
    private Animator myAnimator;

    private bool isGrounded;
    private bool isDoubleJump = false;
    private float moveInput;

    public int damageAmount = 1;
    private float immuneTime = 1.0f;
    private float lastDamageTime = -1.0f;
    private bool isHurt = false;
    private bool isDead = false;
    public Color immuneStateColor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastDamageTime + 0.09f > Time.time)
        {
            isHurt = true;
            moveInput = 0;

            myAnimator.SetTrigger("hurt");
        }
        else if(lastDamageTime + 0.4f < Time.time && !isDead)
        {
            isHurt = false;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);



        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(rb.linearVelocity.y < -0)
        {
            myAnimator.SetBool("isFalling", true);
        }
        else if(rb.linearVelocity.y > 0)
        {
            myAnimator.SetBool("isFalling", false);
        }

        if (isGrounded)
        {
            isDoubleJump = true;
            myAnimator.SetBool("isGrounded", true);
            myAnimator.SetBool("isFalling", false);
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
        }

        if (moveInput != 0)
        {
            myAnimator.SetBool("move", true);
        }
        else
        {
            myAnimator.SetBool("move", false);
        }
    }

    public void OnMove(InputValue value)
    {
        if (isHurt) return;
        if (isDead) return;

        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
        if (moveInput != 0)
        {
            if (moveInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
        }
    }

    public void OnJump(InputValue value)
    {
        if(isHurt) return;
        if(isDead) return;

        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnimator.SetTrigger("jump");
        }

        if (value.isPressed && isDoubleJump && !isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isDoubleJump = false;
            myAnimator.SetTrigger("jump");
        }
    }

    private void PlayerDamage(int amount)
    {
        playerHp -=amount;
        playerHealthDisplay[playerHp].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Respawn"))
        {
            isDead = true;
            stageManager.PlayerRespawn();
        }

        if(collision.CompareTag("Enemy"))
        {


            if (!isArmed)
            {
                if (lastDamageTime + immuneTime < Time.time)
                {

                    PlayerDamage(damageAmount);


                    if (playerHp <= 0)
                    {
                        isDead = true;
                        myAnimator.SetBool("isDead", true);
                        stageManager.PlayerRespawn();
                        
                    }

                    lastDamageTime = Time.time;
                }
            }
            else
            {
                Destroy(collision.gameObject);
                isArmed = false;
                
                lastDamageTime = Time.time;
            }
        }

        if(collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().NextLevel();
        }

        if(collision.CompareTag("Item"))
        {
            int activeItemID = collision.GetComponent<ItemScripts>().ActiveItem();

            switch(activeItemID)
            {
                case 1:     // 1회 무적 아이템
                    isArmed = true;
                    break;
                case 2:     // 이동속도 증가 아이템
                    break;
                case 3:     // 이동속도 감소 아이템
                    break;
                case 4:     // 
                    break;
            }
        }
    }

    public void Respawn()
    {
        isHurt = false;
        isDead = false;
        playerHp = 3;

        for(int i = 0; i<3; i++)
        {
            playerHealthDisplay[i].gameObject.SetActive(true);
        }

        myAnimator.SetBool("isDead", false);
    }
}

