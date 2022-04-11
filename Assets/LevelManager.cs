using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public Sprite LevelUnlockSprite;
    public Sprite LevelLockSprite;


    public GameObject LevelMangerObject;

    GameObject[] Level;

    int CurrentLevel;

    private void Start()
    {
        CurrentLevel = PlayerPrefs.GetInt("Level", 1);

        Level = new GameObject[LevelMangerObject.transform.childCount];

        for (int i = 0; i < Level.Length; i++)
        {

            Level[i] = LevelMangerObject.transform.GetChild(i).gameObject;
            Level[i].GetComponentInChildren<Text>().text = (i+1).ToString();

            if(CurrentLevel > i)
            {
                Level[i].GetComponent<Button>().image.sprite = LevelUnlockSprite;

            }
            else
            {
                Level[i].GetComponent<Button>().image.sprite = LevelLockSprite;
                Level[i].GetComponent<Button>().interactable = false;

            }



        }
        
    }
}
