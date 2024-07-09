using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine.Networking;



public class UpdateCharacter : MonoBehaviour
{
    public TMP_Text txtScore;
    int score = Bringvalue.score;

    int select = Bringvalue.select;
    float positionY = Bringvalue.positionY;
    float positionZ = Bringvalue.positionZ;
    float positionX = Bringvalue.positionX;
    public NotificationManager notificationManager;



    public GameObject player;


    // Start is called before the first frame update
    private void Start()
    {
        if ((select == 1) && (player != null))
        {
            player.transform.position = new Vector3(positionX, positionY, positionZ);
            string number = score.ToString();
            txtScore.text = number;
            Time.timeScale=1;
        }

    }
    // Update is called once per fram



    public void SaveDataUser()
    {
        if (player != null)
        {
            // Đẩy dữ liệu lên API khi ứng dụng thoát
            StartCoroutine(UploadPosition());
            StartCoroutine(UploadScore());
        }

    }


    // upload vị trí
    IEnumerator UploadPosition()
    {

        // Chuẩn bị dữ liệu cần gửi lên API

        string email = Bringvalue.email;
        Vector3 position = player.transform.position;
        float positionX = position.x;
        float positionY = position.y;
        float positionZ = position.z;

        // data can gui len api
        var data = new
        {
            email,
            positionX,
            positionY,
            positionZ

        };


        string jsonStringRequest = JsonConvert.SerializeObject(data);
        var request = new UnityWebRequest("https://mystic-wood.onrender.com/user/save-position", "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();


        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else{
            notificationManager.ShowNotification("Saved");

        }
        request.Dispose();

    }

    //update điểm
    IEnumerator UploadScore()
    {

        // Chuẩn bị dữ liệu cần gửi lên API

        string email = Bringvalue.email;
        string number = txtScore.text;
        int score = int.Parse(number);

        // data can gui len api
        var scoreData = new
        {
            email,
            score

        };

        string jsonStringRequest = JsonConvert.SerializeObject(scoreData);
        var request = new UnityWebRequest("https://mystic-wood.onrender.com/user/save-score", "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else{
            Debug.Log("update-score-success");

        }
        request.Dispose();

    }



}
