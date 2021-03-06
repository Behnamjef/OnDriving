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

    public GameObject MobileDrag;
    
    [Space] public GameObject TapTopStart;

    [Header("Game Panels")] public GameObject LevelComplelePanel;
    public GameObject GameoverPanel;
    public GameObject PausePanal;
    public GameObject CarRewardPanel;
    public GameObject WheelsPanel;

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
        TouchUp();
        MobileDrag.SetActive(false);
        WheelsPanel.SetActive(false);
        StartCoroutine(onGameover());
    }

    IEnumerator onGameover()
    {
        yield return new WaitForSeconds(1);
        GameoverPanel.SetActive(true);
    }

    public void LevelComplete()
    {
        TouchUp();
        WheelsPanel.SetActive(false);
        LevelComplelePanel.SetActive(true);
        MobileDrag.SetActive(false);
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
        AdManager.Instance.OnRewardAdComplete = UnlockCar;
        AdManager.Instance.ShowRewardAd();
    }

    private void UnlockCar()
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

    public void UpdateCoinValue()
    {
        
        CoinValue = PlayerPrefs.GetInt("LevelCoin", 0);
        UpdateCoinsText();
    }
    
    public void UpdateCoinsText()
    {
        CoinText.text = CoinValue.ToString();
        PlayerPrefs.SetInt("LevelCoin", CoinValue);
    }

    public void PlayerWantsSecondChance()
    {
        AdManager.Instance.OnRewardAdComplete = GivePlayerSecondChance;
        AdManager.Instance.ShowRewardAd();
    }

    private void GivePlayerSecondChance()
    {
        GameoverPanel.SetActive(false);
        MobileDrag.SetActive(true);
        WheelsPanel.SetActive(true);
        GameManger.Instance.currentCar.GiveSecondChance();
    }
}