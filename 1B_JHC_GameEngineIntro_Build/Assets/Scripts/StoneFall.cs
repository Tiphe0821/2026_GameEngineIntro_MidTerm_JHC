using UnityEngine;

public class StoneFall : MonoBehaviour
{

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);   
        }
    }

}
