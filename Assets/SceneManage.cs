using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManage : MonoBehaviour
{
    private GameObject sceneSelection;

    void Awake() {
        sceneSelection = GameObject.Find("Dropdown_SceneSelection");
        // sceneSelection.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("Scene"); 

    }

    void Start()
    {
        
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName=="Scene1"){
            PlayerPrefs.SetInt("Scene",0);
        }

        sceneSelection.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("Scene"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int n){
        Debug.Log("准备切换场景");
        if(PlayerPrefs.GetInt("Scene")!=n){
        switch(n){
            case 0 :
            SceneManager.LoadScene("Scene1");
            PlayerPrefs.SetInt("Scene",0); 
            break;
            case 1:
            SceneManager.LoadScene("Scene2");
            PlayerPrefs.SetInt("Scene",1); 
            break;
        }
        }
    }
}
