using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imgKey_1, imgKey_2, imgKey_3;
    public GameObject UIPause, UIGameOver;
    public TMP_Text txtScore;

    public GameObject boss;
    public GameObject spacePortal;
    public Vector3 positionEnemyCreate;
    private int sumKey;

    public GameObject [] enemies;


    void Start(){
        StartCreateEnemy();
    }

    public void UpdateQuantityKey()
    {
        sumKey += 1;
        ActiveImgKey(sumKey);
        BossAppear();
    }

    void ActiveImgKey(int sumKey)
    {
        Debug.Log(sumKey);
        switch (sumKey)
        {
            case 1:
                imgKey_1.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
            case 2:
                imgKey_2.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
            case 3:
                imgKey_3.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
        }
    }

    void BossAppear()
    {
        if (boss)
        {
            if (sumKey == 3)
            {
                boss.SetActive(true);
            }
        }
    }

    public void NextSceneGame()
    {
        GameObject player = GameObject.Find("Player");
        Instantiate(spacePortal, player.transform.position, Quaternion.identity);
        Destroy(player,1f);
        Invoke("LoadScenePlayer", 2f);
    }

    void LoadScenePlayer()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex < 4)
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
    }

    void CreateEnemy(){
        int enemyIndex= UnityEngine.Random.Range(0,2);
        Instantiate(enemies[enemyIndex],positionEnemyCreate,Quaternion.identity);
    }

    public void OpenPauseGame(){
        UIPause.SetActive(true);
        Time.timeScale=0;
    }

    public void ClosePauseGame(){
        UIPause.SetActive(false);
        Time.timeScale=1;
    }

    public void GameOver(){
        UIGameOver.SetActive(true);
        Time.timeScale=0;
    }

    public void UpPoint(){
        int pointCurrent=Int32.Parse(txtScore.text);
        pointCurrent +=1;
        txtScore.text = pointCurrent.ToString();

    }

    public void TroVeManHinhDangNhap(){
        SceneManager.LoadScene(0);
    }

    void StartCreateEnemy()
    {
        StartCoroutine("CreateEnemyAuto");
    }

    IEnumerator CreateEnemyAuto()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            CreateEnemy();
        }
    }

    

}
