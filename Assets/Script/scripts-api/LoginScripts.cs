using Newtonsoft.Json;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class LoginScripts1 : MonoBehaviour
{

    public TMP_InputField edtEmailLogin, edtPassLogin,edtEmailSignUp, edtPassSignUp, edtRePassSignUp;
    public GameObject UIPlay,UILogin;
    public NotificationManager notificationManager;
    public TextMeshProUGUI notificationText;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }


    public void CheckLogin()
    {
        string email = edtEmailLogin.text;
        string pass = edtPassLogin.text;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
        {
            Debug.Log("lỗi empty");
            notificationManager.ShowNotification("Vui lòng nhập tài khoản hoặc mật khẩu");
        }
        else
        {
            Debug.Log("start login");
            LoginRequest loginRequest = new LoginRequest(email, pass);
            // SignupRequest signupRequest = new SignupRequest(email, pass);
            CheckLogin(loginRequest);
            // CheckSignup(signupRequest);
            StartCoroutine(CheckLogin(loginRequest));
        }

    }

    public void CheckSignup()
    {
        string email = edtEmailSignUp.text;
        string pass = edtPassSignUp.text;
        string repass = edtRePassSignUp.text;
        Debug.Log("email" + email);
        Debug.Log("pass" + pass);
        Debug.Log("repass" + repass);

        if (pass.Equals(repass))
        {
            SignupRequest signupRequest = new SignupRequest(email, pass);
            StartCoroutine(CheckSignup(signupRequest));
        }
        else
        {
            notificationManager.ShowNotification("Mật khẩu và xác nhận mật khẩu phải giống nhau");
            if (email.Equals("") || pass.Equals(""))
            {
                notificationManager.ShowNotification("Email và mật khẩu không được để trống");

            }
        }
    }




    IEnumerator CheckLogin(LoginRequest loginRequest)
    {
        notificationText.text="Vui lòng chờ ....";

        //…
        string jsonStringRequest = JsonConvert.SerializeObject(loginRequest);
        // api team https://mystic-wood.onrender.com/user/login
        //api cua tui http://127.0.0.1:1762/users/login
        var request = new UnityWebRequest("https://mystic-wood.onrender.com/user/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("lỗi login");

        }
        //respone login
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonString);
            float positionX = loginResponse.positionX;
            float positionY = loginResponse.positionY;
            float positionZ = loginResponse.positionZ;
            string email = loginResponse.email;
            int score = loginResponse.score;
            int scene=loginResponse.scene;

            Bringvalue.email = email;
            Bringvalue.positionX = positionX;
            Bringvalue.positionY = positionY;
            Bringvalue.positionZ = positionZ;
            Bringvalue.score = score;
            Bringvalue.scene = scene;

            UIPlay.SetActive(true);
            UILogin.SetActive(false);
            Bringvalue.login=true;
            notificationManager.ShowNotification("Đăng nhập thành công");
            Debug.Log("login success");

        }
        request.Dispose();
    }


    IEnumerator CheckSignup(SignupRequest signupRequest)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(signupRequest);
        // api team https://mystic-wood.onrender.com/user/login
        //api cua tui http://127.0.0.1:1762/users/login
        var request = new UnityWebRequest("https://mystic-wood.onrender.com/user/register", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
       
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("Error API");
        }


        //respone signup
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            SignupRespone signupRespone = JsonConvert.DeserializeObject<SignupRespone>(jsonString);
            string message = signupRespone.message;
            if (message != null)
            {
                notificationManager.ShowNotification(message.ToString());

            }

        }
        request.Dispose();
    }
}
