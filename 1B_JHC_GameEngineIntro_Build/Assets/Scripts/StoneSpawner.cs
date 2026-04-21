using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stone;
    public float coolDown = 3.0f;
    private float lastStoneTime;

    private void Start()
    {
        lastStoneTime = Time.time;
    }

    private void Update()
    {
        if (lastStoneTime + coolDown < Time.time)
        {
            stone.SetActive(true);
            stone.transform.position = transform.position;
            stone.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);

            lastStoneTime = Time.time;
        }
    }

}
