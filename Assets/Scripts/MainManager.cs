using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour
{

    public GameObject LoadingPanel;
    public GameObject LevelManager;

    void Start()
    {
        AdManager.Instance.BannerShow();
    }

    public void PlayGame()
    {
        //LoadingPanel.SetActive(true);
        LevelManager.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
