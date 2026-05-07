using System.Linq;
using TMPro;
using UnityEngine;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot; // ÄÁÅÙÆ® ¿ÀºêÁ§Æ®
    [SerializeField] GameObject rowPrefab; // RankRow ÇÁ¸®ÆÕ
    [SerializeField] RankDataSO rankData;   // SO ¿¬°á

    StageResultList allData;


    private void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList();
    }

    private void RefreshRankList()
    {
        foreach(Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        // ·©Å© µ¥ÀÌÅÍ Á¤·Ä 
        var sortedData = allData.results.Where(r => r.stage == rankData.Data ).OrderByDescending(x => x.score).ToList();

        // ·©Å© µ¥ÀÌÅÍ »ý¼º
        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
