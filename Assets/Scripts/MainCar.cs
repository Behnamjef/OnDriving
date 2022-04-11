using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCar : MonoBehaviour
{
    public RCC_CarControllerV3 car;

    bool isLevelComplete;

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
            int Level = PlayerPrefs.GetInt("Level", 0);
            if(Level == PlayerPrefs.GetInt("BtnLevel",0))
            {
            Level++;
            }
            PlayerPrefs.SetInt("Level", Level);
            other.GetComponent<Collider>().enabled = false;
            car.canControl = false;
            InputManager.Instace.LevelComplete();

            InputManager.CoinValue += 100;
            InputManager.Instace.UpdateCoinsText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Barrier")
        {
            if(!isLevelComplete)
            {
                collision.gameObject.GetComponent<MeshRenderer>().material = GameManger.Instance.CollisionMaterial;
            InputManager.Instace.Gameover();
            }
        }
    }
}
