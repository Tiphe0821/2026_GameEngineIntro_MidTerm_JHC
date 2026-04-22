using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPannel;
    public GameObject settingPannel;
    public SoundManager soundObject;

    public Scrollbar scrollbar;

    public void StartButton()
    {
        Debug.Log("StartButton Clicked");
        SceneManager.LoadScene("Stage_01_02");
    }

    public void OpenHelp()
    {
        helpPannel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPannel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingPannel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingPannel.SetActive(false);
    }

    public void OpenTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void OnValueChange(float value)
    {
        soundObject.ChangeVolume(scrollbar.value);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(helpPannel == isActiveAndEnabled)
            {
                CloseHelp();
            }

            if(settingPannel == isActiveAndEnabled)
            {
                CloseSettings();
            }
        }
    }
}
