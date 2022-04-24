using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject[] Levels;

    int CurrentLevel;

    public Material CollisionMaterial;

    public static GameManger Instance { set; get; }

    
    public Transform Cars;

    private MainCar[] CarPrefab
    {
        get
        {
            if (_carPrefab.IsNullOrEmpty())
                _carPrefab = Cars.GetComponentsInChildren<MainCar>(true);
            return _carPrefab;
        }
    }
    private MainCar[] _carPrefab;

    private MainCar currentCar;
    
    void Awake()
    {
        Instance = this;

        CurrentLevel = PlayerPrefs.GetInt("Level", 0);
        GameObject Level = Instantiate(Levels[PlayerPrefs.GetInt("BtnLevel", 0) - 1], transform.position,
            Quaternion.identity);
        Level.transform.localScale = new Vector3(0.57f, 0.57f, 0.57f);

        InitCar(Level);
    }

    private void InitCar(GameObject Level)
    {
        currentCar = CarPrefab.FirstOrDefault(c=>c.CarType == UnlockManager.Instance.GetSelectedCarType());
        currentCar.transform.position = Level.transform.GetChild(0).transform.position;
        currentCar.transform.rotation = Level.transform.GetChild(0).rotation;
        currentCar.gameObject.SetActive(true);
    }
}