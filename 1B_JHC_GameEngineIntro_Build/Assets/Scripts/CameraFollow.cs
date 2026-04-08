using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraOffest = -12.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, cameraOffest);
        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        transform.position = targetPos;
    }
}
