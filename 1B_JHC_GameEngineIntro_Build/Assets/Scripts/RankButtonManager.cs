using UnityEngine;

public class RankButtonManager : MonoBehaviour
{
    public GameObject Content1;
    public GameObject Content2;
    public GameObject Content3;
    public GameObject Content4;

    public void ActiveContent1()
    {
        Content1.SetActive(true);
        Content2.SetActive(false);
        Content3.SetActive(false);
        Content4.SetActive(false);
    }
    public void ActiveContent2()
    {
        Content1.SetActive(false);
        Content2.SetActive(true);
        Content3.SetActive(false);
        Content4.SetActive(false);

    }
    public void ActiveContent3()
    {
        Content1.SetActive(false);
        Content2.SetActive(false);
        Content3.SetActive(true);
        Content4.SetActive(false);

    }
    public void ActiveContent4()
    {
        Content1.SetActive(false);
        Content2.SetActive(false);
        Content3.SetActive(false);
        Content4.SetActive(true);

    }
}
