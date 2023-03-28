using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBarFill;
    // Start is called before the first frame update
    void Start()
    {
        LoadingScreen = GameObject.Find("LoadingPanel");
        LoadingBarFill = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        LoadingScreen.SetActive(false);
    }

    public void LoadScene(int sceneId){
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadingScreen.SetActive(true);
        while(!operation.isDone){
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.value = progressValue*100;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
