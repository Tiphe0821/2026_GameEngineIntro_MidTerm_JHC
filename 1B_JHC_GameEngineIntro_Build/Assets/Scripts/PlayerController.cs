using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject armorDisplay;
    public GameObject speedDisplay;
    public GameObject jumpDisplay;
    public StageManager stageManager;

    private Rigidbody2D rb;
    private Animator myAnimator;

    private bool isGrounded;
    private bool isDoubleJump = false;
    private float moveInput;

    private float speedUpTime = 15.0f;
    private float buffTime1;
    private bool isSpeedy = false;
    private float buffSpeed = 1.8f;

    private float jumpUpTime = 15.0f;
    private float buffTime2;
    private bool isFrog = false;
    private float buffJumpForce = 1.8f;

    public int damageAmount = 1;
    private float immuneTime = 1.0f;
    private float lastDamageTime = -1.0f;
    private bool isHurt = false;
    private bool isDead = false;

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

        if (!isSpeedy)
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed * buffSpeed, rb.linearVelocity.y);
            if (buffTime1 + speedUpTime < Time.time)
            {
                speedDisplay.SetActive(false);
                isSpeedy = false;
            }
        }

        if(buffTime2 + jumpUpTime < Time.time)
        {
            jumpDisplay.SetActive(false);
            isFrog = false;
        }



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
            if(!isFrog)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce * buffJumpForce, ForceMode2D.Impulse);
            }
                myAnimator.SetTrigger("jump");
        }

        if (value.isPressed && isDoubleJump && !isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            if (!isFrog)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce * buffJumpForce, ForceMode2D.Impulse);
            }
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
            stageManager.PlayerRespawn(isDead);
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
                collision.gameObject.SetActive(false);
                isArmed = false;
                armorDisplay.SetActive(false);
                
                lastDamageTime = Time.time;
            }
        }

        if(collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().NextLevel();
        }

        if(collision.CompareTag("Save"))
        {
            collision.GetComponent<SavePoint>().NewSavePoint();
        }

        if(collision.CompareTag("Item"))
        {
            int activeItemID = collision.GetComponent<ItemScripts>().ActiveItem();

            switch(activeItemID)
            {
                case 1:     // 1회 무적 아이템
                    isArmed = true;
                    armorDisplay.SetActive(true);
                    break;
                case 2:     // 이동속도 증가 아이템
                    buffTime1 = Time.time;
                    isSpeedy = true;
                    speedDisplay.SetActive(true);
                    break;
                case 3:     // 이동속도 감소 아이템
                    break;
                case 4:     // 점프력 증가 아이템
                    buffTime2 = Time.time;
                    isFrog = true;
                    jumpDisplay.SetActive(true);
                    break;
            }

            collision.gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        isHurt = false;
        isDead = false;

        speedDisplay.SetActive(false);
        isSpeedy = false;

        armorDisplay.SetActive(false);
        isArmed = false;

        jumpDisplay.SetActive(false);
        isFrog = false;
        
        playerHp = 3;

        for(int i = 0; i<3; i++)
        {
            playerHealthDisplay[i].gameObject.SetActive(true);
        }

        myAnimator.SetBool("isDead", false);

        rb.linearVelocity = new Vector2(0, 0);
    }
}

