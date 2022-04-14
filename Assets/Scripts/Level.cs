using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    Button btn;


    private void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(BtnClick);
    }

    void BtnClick()
    {
        PlayerPrefs.SetInt("BtnLevel", transform.GetSiblingIndex() + 1);
        PlayerPrefs.SetInt("FromBtn", 1);
        MainManager.Instance.LoadLevelScene();
    }
}
