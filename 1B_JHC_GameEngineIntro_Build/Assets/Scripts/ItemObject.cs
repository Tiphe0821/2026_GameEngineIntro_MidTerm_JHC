using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemSO data; // Inpector 그래그
    
    public int GetPoint()
    {
        return data.point; // ItemSO의 Point 값 반환
    }
}
