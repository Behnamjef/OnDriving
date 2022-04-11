using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    public Slider ProgressSlider;

    public int LoadLevelInt;

    private void Start()
    {
        StartCoroutine(LoadAsynchronously(LoadLevelInt));
    }
    public void Loadlevel( int LevelIndex)
    {
        
    }

    IEnumerator LoadAsynchronously (int LevelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            ProgressSlider.value = progress;

            yield return null;
        }
    }

}
