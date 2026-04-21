using UnityEngine;

public class StoneFall : MonoBehaviour
{

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("StoneFloor"))
        {
            gameObject.SetActive(false);
        }
    }
}
