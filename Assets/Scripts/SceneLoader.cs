using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreenPrefab;
    private Slider _loadingSlider;

    private static SceneLoader _instance;
    public static SceneLoader GetInstance() => _instance;

    private void Awake()
    {
        _instance = this;
    }


    public void LoadSceneWithLoading(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
        // SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }


    private IEnumerator LoadAsynchronously(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);


        var loadingScreen = Instantiate(loadingScreenPrefab);
        loadingScreen.SetActive(true);
        _loadingSlider = loadingScreen.GetComponentInChildren<Slider>();

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);

            _loadingSlider.value = progress;

            yield return null;
        }
    }
}