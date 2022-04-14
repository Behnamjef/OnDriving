using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public GameObject LoadingPanel;
    public GameObject LevelManager;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        AdManager.Instance.BannerShow();
    }

    public void PlayGame()
    {
        LevelManager.SetActive(true);
    }

    public void LoadLevelScene()
    {
        LevelManager.SetActive(false);
        LoadingPanel.SetActive(true);
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
