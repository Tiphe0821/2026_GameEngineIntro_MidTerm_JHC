using UnityEngine;

public class PlatformDisapear : MonoBehaviour
{
    public GameObject[] disapearObjects;

    private float disapearTime = 5.0f;
    private float startTime;
    private bool actived = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            startTime = Time.time;
            actived = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(actived)
        {
            if(Time.time > disapearTime + startTime)
            {
                foreach(GameObject obj in disapearObjects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
