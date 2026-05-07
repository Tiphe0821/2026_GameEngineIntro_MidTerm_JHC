using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPannel;
    public GameObject settingPannel;
    public GameObject leaderboard;
    public SoundManager soundObject;

    public Scrollbar scrollbar;

    public TMP_InputField inputField;

    public void StartButton()
    {
        string playerName = inputField.text;
        if(string.IsNullOrEmpty(playerName) )
        {
            Debug.Log("플레이어 이름을 입력하세요");
            return;
        }
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Debug.Log("플레이어 이름 저장됨: " + playerName);   



        Debug.Log("StartButton Clicked");
        SceneManager.LoadScene("Stage_01_02");
    }

    public void OpenLeaderboard()
    {
        leaderboard.SetActive(true);
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
