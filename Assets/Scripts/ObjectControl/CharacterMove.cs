using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float moveSpeed = 10f;       // 이동속도
    public float jumpForce = 5f;        // 점프힘
    private bool isJumping;
    private Rigidbody playerRigidbody;
    Vector3 playerPosition;             // 플레이어 위치

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();        // Player에서 rigidbody 가져옴
    }

    void Update()
    {
        Move();
        Jump();
        Replace();
    }

    void Move()
    {
        // -1 ~ 1 입력
        float horizontalInput = Input.GetAxis("Horizontal");    // 좌우
        float verticalInput = Input.GetAxis("Vertical");        // 상하

        float fallSpeed = playerRigidbody.linearVelocity.y;        // 떨어지는 속도

        Vector3 velocity = new Vector3(horizontalInput, 0, verticalInput);
        velocity *= moveSpeed;
        velocity.y = fallSpeed;                             // 떨어지는 속도 초기화
        playerRigidbody.linearVelocity = velocity;
    }

    void Jump()
    {
        if (Input.GetKeyDown("space"))
        {
            // 리지드바디에 충격량(점프힘)을 가함
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("점프");
        }
    }

    void Replace()
    {
        playerPosition = this.transform.position;
        
        if (playerPosition.y < 50f)
        {
            // player = GameObject.FindWithTag("Player");
            // player.SetActive(false);
            playerPosition = new Vector3(1f, 3f, 1f);
            // player.SetActive(true);
        }

    }


}
