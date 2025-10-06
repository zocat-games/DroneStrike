using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zocat;

public class LoadingBar : MonoBehaviour
{
    private string sceneToLoad = "Movie";
    public Image FillImg;

    void Start()
    {
        // StartCoroutine(LoadSceneAsync());
        // FillImg.DOFillAmount(1, 2);
        Load();
    }


    private void Load()
    {
        // SceneManager.LoadSceneAsync(sceneToLoad);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            FillImg.fillAmount = progress;
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}