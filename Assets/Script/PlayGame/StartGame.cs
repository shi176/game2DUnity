using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject UISelect,UILogin,UIPlay;
    

    void Start(){   
        bool login=Bringvalue.login;
        if(login){
            UILogin.SetActive(false);
            UIPlay.SetActive(true);
        }

    }

    // Start is called before the first frame update
    public void LoadSceneStart()
    {
        int scene = Bringvalue.scene;
        if (scene != null)
        {
            UISelect.SetActive(true);
        }
        else{
            SceneManager.LoadScene("Man-1");
        }

    }

    public void LoadSceneContinue()
    {
        int scene = Bringvalue.scene;
        Bringvalue.select=1;
        SceneManager.LoadScene(scene);

    }

    public void LoadSceneBegin()
    {
        SceneManager.LoadScene("Man-1");

    }


}
