using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public TextMeshProUGUI notificationText;

    public void ShowNotification(string message)
    {
        // Hiển thị thông báo trên UI Text
        notificationText.text = message;

        // Gọi coroutine để tự động ẩn thông báo sau một khoảng thời gian
        StartCoroutine(HideNotification());
    }

    IEnumerator HideNotification()
    {
        // Chờ 3 giây trước khi ẩn thông báo
        yield return new WaitForSeconds(3f);

        // Ẩn thông báo bằng cách đặt văn bản thành chuỗi rỗng
        notificationText.text = "";
    }
}
