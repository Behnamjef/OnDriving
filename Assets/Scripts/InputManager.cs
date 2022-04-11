using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    // public GameObject ScoreText;

    public static InputManager Instace { set; get; }

    public static bool OnTouch;

    public static int Gear;

    public Text ScoreText;

    public Text CoinText;
    public static int CoinValue;


    [Space] public GameObject TapTopStart;

    [Header("Game Panels")] public GameObject LevelComplelePanel;
    public GameObject GameoverPanel;
    public GameObject PausePanal;
    public GameObject CarRewardPanel;

    void Awake()
    {
        PausePanal.SetActive(false);
        Instace = this;

        ScoreText.text = "Level " + (PlayerPrefs.GetInt("BtnLevel", 0));


        AdManager.Instance.BannerShow();

        CoinValue = PlayerPrefs.GetInt("LevelCoin", 0);
        CoinText.text = CoinValue.ToString();
    }

    public void TouchDown()
    {
        OnTouch = true;
        onTapToStart();
        Debug.Log("Down");
    }

    public void TouchUp()
    {
        OnTouch = false;
        Debug.Log("Up");
    }


    public void ChangeGear()
    {
        if (Gear == 0)
        {
            Gear = 2;
        }
        else
        {
            Gear = 0;
        }

        Debug.Log("Current Gear " + Gear);
    }

    public int OnGear()
    {
        return Gear;
    }


    public void onTapToStart()
    {
        TapTopStart.SetActive(false);
    }

    public void Gameover()
    {
        StartCoroutine(onGameover());
    }

    IEnumerator onGameover()
    {
        yield return new WaitForSeconds(1);
        GameoverPanel.SetActive(true);
    }

    public void LevelComplete()
    {
        LevelComplelePanel.SetActive(true);
    }

    public void ShowUnlockPanel()
    {
        if (UnlockManager.Instance.CanUnlockOnThisLevel())
            CarRewardPanel.SetActive(true);
        else
            NextLevel();
    }

    public void ClaimTheCar()
    {
        AdManager.Instance.OnRewardAdComplete = OnRewardVideoCompleted;
        AdManager.Instance.ShowRewardAd();
    }

    private void OnRewardVideoCompleted()
    {
        var carToClaim = UnlockManager.Instance.GetUnlockCarOnThisLevel();
        UnlockManager.Instance.UnlockCar(carToClaim);
        UnlockManager.Instance.SelectCar(carToClaim);
        NextLevel();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("BtnLevel", PlayerPrefs.GetInt("BtnLevel", 0) + 1);

        SceneManager.LoadScene(1);
    }

    public void Home()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void PauseBtn()

    {
        Time.timeScale = 0;
        PausePanal.SetActive(true);
    }

    public void ResumeBtn()

    {
        Time.timeScale = 1;
        PausePanal.SetActive(false);
    }

    public void UpdateCoinsText()
    {
        CoinText.text = CoinValue.ToString();
        PlayerPrefs.SetInt("LevelCoin", CoinValue);
    }
}