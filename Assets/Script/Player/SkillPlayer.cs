using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPlayer : MonoBehaviour
{
    public GameObject powerHasaki, powerChidori, powerSorugatong;

    public AudioSource hasaki, turn, sorugatong, chidori;
    public GameObject imgSkillHasaki,imgSkillChidori,imgSkillSorugatong;

    public TextMeshProUGUI timeQ,timeE,timeR;
    public float speedSkillHasaki,speedSkillChidori,speedSkillSorugatong;

    private int countdownTimeQ=5,
                countdownTimeE=10,
                countdownTimeR=15;


    private bool activeSkillHasaki=false,
    activeSkillChidori=false,
    activeSkillSorugatong=false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !activeSkillHasaki)
        {
            hasaki.Play();
            FirePowerHasaki();
            activeSkillHasaki=true;//gioi han luot dung skill

            //làm mờ ảnh skill
            imgSkillHasaki.GetComponent<Image>().color = new Color32(103,103,103,255);

            //dem nguoc thoi gian chờ dùng skill
            StartCountdown(countdownTimeQ,timeQ,imgSkillHasaki,"Q");
        }

        if (Input.GetKeyDown(KeyCode.E) && !activeSkillChidori)
        {
            chidori.Play();
            FirePowerChidori();
            activeSkillChidori=true; //gioi han luot dung skill

            //làm mờ ảnh skill
            imgSkillChidori.GetComponent<Image>().color = new Color32(103,103,103,255);

            //dem nguoc thoi gian chờ dùng skill
            StartCountdown(countdownTimeE,timeE,imgSkillChidori,"E");
        }

        if (Input.GetKeyDown(KeyCode.R) && !activeSkillSorugatong)
        {
            sorugatong.Play();
            activeSkillSorugatong=true;//gioi han luot dung skill
            //delay gọi hàm
            FirePowerSorugatong();

            //làm mờ ảnh skill
            imgSkillSorugatong.GetComponent<Image>().color = new Color32(103,103,103,255);


            //dem nguoc thoi gian chờ dùng skill
            StartCountdown(countdownTimeR,timeR,imgSkillSorugatong,"R");
        }


    }

    private void FirePowerHasaki()
    {
        GameObject power = Instantiate(powerHasaki, transform.position, Quaternion.identity);
        Rigidbody2D rb = power.GetComponent<Rigidbody2D>();

        Vector2 scale = power.transform.localScale;

        var navigationPlayer = GameObject.Find("Player").GetComponent<Player>().getNavigationPlayer();

        switch (navigationPlayer)
        {

            case "right":
                scale.x *= scale.x > 0 ? 1 : -1;
                power.transform.localScale = scale;
                rb.AddForce(transform.right * speedSkillHasaki, ForceMode2D.Impulse);
                break;
            case "left":
                scale.x *= scale.x > 0 ? -1 : 1;
                power.transform.localScale = scale;
                rb.AddForce(transform.right * -speedSkillHasaki, ForceMode2D.Impulse);
                break;
            case "up":
                power.transform.Rotate(new Vector3(0, 0, 90));
                rb.AddForce(transform.up * speedSkillHasaki, ForceMode2D.Impulse);
                break;
            case "down":
                power.transform.Rotate(new Vector3(0, 0, -90));
                rb.AddForce(transform.up * -speedSkillHasaki, ForceMode2D.Impulse);
                break;
        }

    }

    private void FirePowerChidori()
    {
        //-1,0 chan
        //-1,1 le
        //0,1 chan
        //-1,1 le
        //-1,0 chan
        //-1,-1 le
        //0,-1 chan
        //1,-1 chan



        for (var i = 0; i <= 7; i++)
        {
            GameObject power = Instantiate(powerChidori, transform.position, Quaternion.identity);
            Rigidbody2D rb = power.GetComponent<Rigidbody2D>();
            switch (i){
                case 0:
                    rb.AddForce(new Vector2(-speedSkillChidori,0), ForceMode2D.Impulse);
                break;

                case 1:
                    rb.AddForce(new Vector2(-speedSkillChidori,speedSkillChidori), ForceMode2D.Impulse);
                break;

                case 2:
                    rb.AddForce(new Vector2(0,speedSkillChidori), ForceMode2D.Impulse);
                break;

                case 3:
                    rb.AddForce(new Vector2(speedSkillChidori,speedSkillChidori), ForceMode2D.Impulse);
                break;

                case 4:
                    rb.AddForce(new Vector2(speedSkillChidori,0), ForceMode2D.Impulse);
                break;

                case 5:
                    rb.AddForce(new Vector2(speedSkillChidori,-speedSkillChidori), ForceMode2D.Impulse);
                break;

                case 6:
                    rb.AddForce(new Vector2(0,-speedSkillChidori), ForceMode2D.Impulse);
                break;

                case 7:
                    rb.AddForce(new Vector2(-speedSkillChidori,-speedSkillChidori), ForceMode2D.Impulse);
                break;
            }
            

        }

    }

    private void FirePowerSorugatong()
    
    {
        GameObject power = Instantiate(powerSorugatong, transform.position, Quaternion.identity);
        Rigidbody2D rb = power.GetComponent<Rigidbody2D>();
        Vector2 scale = power.transform.localScale;
        var navigationPlayer = GameObject.Find("Player").GetComponent<Player>().getNavigationPlayer();

        switch (navigationPlayer)
        {

            case "right":
                scale.x *= scale.x > 0 ? 1 : -1;
                power.transform.localScale = scale;
                rb.AddForce(transform.right * speedSkillSorugatong, ForceMode2D.Impulse);
                break;
            case "left":
                scale.x *= scale.x > 0 ? -1 : 1;
                power.transform.localScale = scale;
                rb.AddForce(transform.right * -speedSkillSorugatong, ForceMode2D.Impulse);
                break;
            case "up":
                power.transform.Rotate(new Vector3(0, 0, 90));
                rb.AddForce(transform.up * speedSkillSorugatong, ForceMode2D.Impulse);
                break;
            case "down":
                power.transform.Rotate(new Vector3(0, 0, -90));
                rb.AddForce(transform.up * -speedSkillSorugatong, ForceMode2D.Impulse);
                break;
        }


    }


    //countdown
    void StartCountdown(int countdownTime,TextMeshProUGUI textTime,GameObject image,string typeSkill)
    {
        StartCoroutine(Countdown(countdownTime,textTime,image,typeSkill));
    }

    IEnumerator Countdown(int countdownTime,TextMeshProUGUI textTime,GameObject image,string typeSkill)
    {
        var countdown=countdownTime;
        while (countdown > 0)
        {
            textTime.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        image.GetComponent<Image>().color = new Color32(255,255,225,255);
        // Clear the countdown text or do any other cleanup
        textTime.text = "";
        
        switch(typeSkill){
            case "Q":{
                activeSkillHasaki=false;
                break;
            }
            case "E":{
                activeSkillChidori=false;
                break;
            }
            case "R":{
                activeSkillSorugatong=false;
                break;
            }
        }
    }
}
