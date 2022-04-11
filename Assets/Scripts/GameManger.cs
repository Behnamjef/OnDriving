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

    public MainCar[] CarPrefab;

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
        var car = CarPrefab.FirstOrDefault(c=>c.CarType == UnlockManager.Instance.GetSelectedCarType());
        car.transform.position = Level.transform.GetChild(0).transform.position;
        car.transform.rotation = Level.transform.GetChild(0).rotation;
        car.gameObject.SetActive(true);
    }
}