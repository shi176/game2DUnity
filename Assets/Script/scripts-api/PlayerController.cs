using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;
    
    public float collisionOffset = 0.05f;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public GameObject TriggerArea;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // set vị trí đã lưu cho player
        //PlayerPrefs.SetFloat("0", transform.position.x);
        //PlayerPrefs.SetFloat("0", transform.position.y);
        //PlayerPrefs.SetFloat("0", transform.position.z);
        // If movement input is not 0, try moving
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

        }

    }

    private bool TryMove(Vector2 direction)
    {
        // Kiểm tra các va chạm tiềm ẩn
        int count = rb.Cast(movementInput,
            movementFilter, // Các giá trị X và Y trong khoảng từ -1 đến 1 biểu thị hướng từ cơ thể để tìm kiếm va chạm
            castCollisions,// Các cài đặt xác định nơi có thể xảy ra va chạm, chẳng hạn như các lớp va chạm với castCollisions,
                           //Danh sách va chạm để lưu trữ các va chạm được tìm thấy sau khi Truyền xong
            moveSpeed * Time.fixedDeltaTime + collisionOffset);// Số lượng cần truyền bằng chuyển động cộng với phần bù

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

    }

    




}
