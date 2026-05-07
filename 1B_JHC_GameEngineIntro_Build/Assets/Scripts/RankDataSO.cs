using UnityEngine;

[CreateAssetMenu(fileName = "newRankData", menuName = "Game/RankData")]
public class RankDataSO : ScriptableObject
{
    [Header("스테이지 넘버")]

    public int Data = 0;
}
