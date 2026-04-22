using UnityEngine;

public class StageManager : MonoBehaviour
{

    public GameObject player;

    public GameObject[] respawnPoint;
    public int currentRespawnPoint = 0;

    private float deadTime;
    private float respawnDelayTime = 2.0f;

    private bool isDead;

    // 사용되었던 아이템 일괄 활성화
    public GameObject[] items;

    // 죽은 몬스터 활성화, 몬스터 원위치
    public GameObject[] enemies;
    public Transform[] enemySpawns;

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
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
        }

        isDead = true;
        deadTime = Time.time;
    }

    public void PlayerRespawn(bool isdead)
    { 
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
        }

        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(true);
            enemies[i].transform.position = enemySpawns[i].transform.position;
        }

        isDead = isdead;
        deadTime = Time.time - respawnDelayTime;
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
