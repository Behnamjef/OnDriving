using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{

   public GameObject [] Levels;

    int CurrentLevel;

    public Material CollisionMaterial;

    public static GameManger Instance { set; get; }

    public Transform []CarPrefab;

    void Awake()
    {
        Instance = this;

        CurrentLevel = PlayerPrefs.GetInt("Level", 0);
        GameObject Level = Instantiate(Levels[PlayerPrefs.GetInt("BtnLevel", 0) - 1], transform.position, Quaternion.identity);
        Level.transform.localScale = new Vector3(0.57f, 0.57f, 0.57f);
        CarPrefab[PlayerPrefs.GetInt("SelectedCar", 0)].position = Level.transform.GetChild(0).transform.position;
        CarPrefab[PlayerPrefs.GetInt("SelectedCar", 0)].rotation = Level.transform.GetChild(0).rotation;
        CarPrefab[PlayerPrefs.GetInt("SelectedCar", 0)].gameObject.SetActive(true);




    }
}
