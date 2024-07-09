using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;

public class LoginResponse
{
    public LoginResponse(string email, int score, string password, float positionX, float positionY, float positionZ, int scene)
    {
        this.email = email;
        this.score = score;
        this.password = password;
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
        this.scene = scene;
        
     
    }

    public string email {  get; set; }
    public int score { get; set; }
    public string password { get; set; }
    public float positionX {  get; set; }
    public float positionY { get; set; }

    public float positionZ { get; set; }
    public int scene { get; set; }



}
