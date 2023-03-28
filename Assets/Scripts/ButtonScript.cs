using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject player;
    public bool ButtonIsUsed=false;
    public GameObject LScript;
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        LScript = GameObject.Find("SceneLoader");
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonIsUsed == true)
        {
            if(this.name == "Button-devRoom"){
                player.transform.position = new Vector3(75.5513306f, 0.925819397f, 76.29702f);
                ButtonIsUsed = false;
            }
            if(this.name == "Button-LevelFinish"){
                LScript.GetComponent<LoadingScript>().LoadScene(scene.buildIndex+1);
            }
        }
    }
}