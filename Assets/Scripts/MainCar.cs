using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCar : MonoBehaviour
{
    public RCC_CarControllerV3 car;

    bool isLevelComplete;

    public CarType CarType;

    void Start()
    {
        car = GetComponent<RCC_CarControllerV3>();
        isLevelComplete = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish_Point")
        {
            isLevelComplete = true;
            int Level = PlayerPrefs.GetInt("Level", 1);
            var btnLevel = PlayerPrefs.GetInt("BtnLevel", 0);
            if (Level == btnLevel)
            {
                Level++;
            }

            PlayerPrefs.SetInt("Level", Level);
            other.GetComponent<Collider>().enabled = false;
            car.canControl = false;
            InputManager.Instace.LevelComplete();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            if (!isLevelComplete)
            {
                collision.gameObject.GetComponent<MeshRenderer>().material = GameManger.Instance.CollisionMaterial;
                InputManager.Instace.Gameover();
            }
        }
    }
}