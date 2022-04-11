using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance { set; get; }

    public Transform Cars;

    [SerializeField] private ShopCar[] ShopCars;

    private CarType _currentCarType;
    private int _currentCarIndex;

    public Text ShopText;

    public Button SelectButton;

    int Coins;

    void Awake()
    {
        Instance = this;

        _currentCarType = UnlockManager.Instance.GetSelectedCarType();
        _currentCarIndex = ShopCars.ToList().FindIndex(c => c.CarType == _currentCarType);

        UpdatePrices();
    }

    void Start()
    {
        Coins = PlayerPrefs.GetInt("LevelCoin", 0);
    }

    public void NextCar()
    {
        if (_currentCarIndex >= ShopCars.Length - 1)
        {
            _currentCarIndex = 0;
        }
        else
        {
            _currentCarIndex++;
        }

        UpdatePrices();
    }


    public void PreviousCar()
    {
        if (_currentCarIndex == 0)
        {
            _currentCarIndex = ShopCars.Length - 1;
        }
        else
        {
            _currentCarIndex--;
        }

        UpdatePrices();
    }


    void UpdatePrices()
    {
        _currentCarType = ShopCars[_currentCarIndex].CarType;

        foreach (var car in ShopCars)
        {
            if (car.CarType == _currentCarType)
            {
                car.SetActive(true);

                if (UnlockManager.Instance.IsCarUnlock(car.CarType))
                {
                    if (UnlockManager.Instance.GetSelectedCarType() == _currentCarType)
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
                    ShopText.text = UnlockManager.Instance.GetCar(_currentCarType).UnlockPrice.ToString();
                }
            }
            else
            {
                car.SetActive(false);
            }
        }
    }


    public void SelectCar()
    {
        if (UnlockManager.Instance.IsCarUnlock(_currentCarType))
        {
            Debug.Log("Car Aleady Unlocked");
            UnlockManager.Instance.UnlockCar(_currentCarType);
            UnlockManager.Instance.SelectCar(_currentCarType);
            UpdatePrices();
        }

        else
        {
            var carPrice = UnlockManager.Instance.GetCar(_currentCarType).UnlockPrice;
            if (true)//carPrice <= PlayerPrefs.GetInt("LevelCoin", 0))
            {
                Coins -= carPrice;

                PlayerPrefs.SetInt("LevelCoin", Coins);

                Coins = PlayerPrefs.GetInt("LevelCoin");

                ShopManager.instance.UpdateCoin();
                
                UnlockManager.Instance.UnlockCar(_currentCarType);
                UnlockManager.Instance.SelectCar(_currentCarType);
                UpdatePrices();
            }
            else
            {
                Debug.Log("You don't have enough coins");
            }
        }
    }
}