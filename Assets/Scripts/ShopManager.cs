using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { set; get; }


    public Text CoinText;
    int Coins;
    void Start()
    {
        instance = this;

        Coins = PlayerPrefs.GetInt("LevelCoin", 0);

        CoinText.text = Coins.ToString();
    }

    
    
    public void UpdateCoin()
    {
        Coins = PlayerPrefs.GetInt("LevelCoin", 0);
        CoinText.text = Coins.ToString();

    }

    public void NextCar()
    {
        Shop.Instance.NextCar();
    }

    public void PreviousCar()
    {
        Shop.Instance.PreviousCar();

    }


    public void SelectCar()
    {
        Shop.Instance.SelectCar();
    }

    public void BackBtn()
	{
        SceneManager.LoadScene("Menu");

    }
}
