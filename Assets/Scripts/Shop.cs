using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{

    public static Shop Instance { set; get; }

    public Transform Cars;

    GameObject[] Vehicales;

    int CurrentIndex;

    public int[] CarPrices;

    public Text ShopText;

    public Button SelectButton;

    int Coins;

    void Awake()
    {
        Instance = this;

        PlayerPrefs.SetInt("UnlockCar" + CurrentIndex, 1);

        CurrentIndex = PlayerPrefs.GetInt("CurrentIndex", PlayerPrefs.GetInt("SelectedCar",0));

        Vehicales = new GameObject[Cars.transform.childCount];


        UpdatePrices();
      
    }

    void Start()
    {
        Coins = PlayerPrefs.GetInt("LevelCoin", 0);

    }

    public void NextCar()
    {


        if (CurrentIndex >= Vehicales.Length -1 )
        {
            CurrentIndex = 0;
        }
        else
        {
            CurrentIndex += 1;
        }
        Debug.Log(CurrentIndex);


        UpdatePrices();



    }


    public void PreviousCar()
    {
        if (CurrentIndex < 1)
        {
            CurrentIndex = Vehicales.Length -1;
        }
        else
        {
            CurrentIndex -= 1;
        }

        UpdatePrices();

    }



    void UpdatePrices()
    {

        for (int i = 0; i < Vehicales.Length; i++)
        {
            Vehicales[i] = Cars.GetChild(i).gameObject;

            if (CurrentIndex == i)
            {

                Vehicales[i].SetActive(true);

                if (PlayerPrefs.GetInt("UnlockCar" + CurrentIndex, 0) == 1)
                {

                    if(PlayerPrefs.GetInt("SelectedCar",0)== CurrentIndex)
                    {
                        SelectButton.transform.GetChild(0).GetComponent<Text>().text = "Selected";
                        ShopText.text = "";
                    }
                    else
                    {
                        SelectButton.transform.GetChild(0).GetComponent<Text>().text = "Select";
                        ShopText.text = "";
                    }
                    

                }
                else
                {
                    SelectButton.transform.GetChild(0).GetComponent<Text>().text = "Buy";
                    ShopText.text = CarPrices[i].ToString();


                }


            }
            else
            {
                Vehicales[i].SetActive(false);
            }

        }
    }


    public void SelectCar()
    {

        if (PlayerPrefs.GetInt("UnlockCar" + CurrentIndex, 0) == 1)
        {
            Debug.Log("Car Aleady Unlocked");
            PlayerPrefs.SetInt("UnlockCar" + CurrentIndex, 1);
            PlayerPrefs.SetInt("SelectedCar", CurrentIndex);
            UpdatePrices();
        }

        else
        {
            if (CarPrices[CurrentIndex] <= PlayerPrefs.GetInt("LevelCoin", 0))
            {



                Coins = Coins -  CarPrices[CurrentIndex];

                PlayerPrefs.SetInt("LevelCoin", Coins);

                Coins = PlayerPrefs.GetInt("LevelCoin");


                ShopManager.instance.UpdateCoin();

                PlayerPrefs.SetInt("UnlockCar" + CurrentIndex, 1);
                PlayerPrefs.SetInt("SelectedCar", CurrentIndex);
                UpdatePrices();
            }
            else
            {
                Debug.Log("You don't have enough coins");
            }
        }

      
       
      
    }

}
