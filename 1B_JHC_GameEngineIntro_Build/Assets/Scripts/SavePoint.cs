using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public StageManager stageManager;
    public SpriteRenderer spriteRenderer;

    public Sprite savedImage;

    public int SavePointNum = 1;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void NewSavePoint()
    {
        stageManager.SetCurrentPoint(SavePointNum);
        spriteRenderer.sprite = savedImage;
    }
}
