using UnityEngine;
using UnityEngine.Rendering;

public class StageManager : MonoBehaviour
{

    public GameObject player;

    public GameObject[] respawnPoint;
    private int currentRespawnPoint;

    private float deadTime;
    private float respawnDelayTime = 2.0f;

    private bool isDead;

    private void Start()
    {
        currentRespawnPoint = 0;
    }

    public void SetCurrentPoint(int current)
    {
        currentRespawnPoint = current;
    }

    public void PlayerRespawn()      // 죽었을 경우 리스폰 포인트에서 부활하도록 
    {
        isDead = true;
        deadTime = Time.time;
    }

    private void Update()
    {
        if(isDead)
        {
            if (Time.time > deadTime + respawnDelayTime) 
            {

                player.transform.position = respawnPoint[currentRespawnPoint].transform.position;
                PlayerController playerController = player.GetComponent<PlayerController>();

                playerController.Respawn();
                isDead = false;
            }
        }
    }
}
