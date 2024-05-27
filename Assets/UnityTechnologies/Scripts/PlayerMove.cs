using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 movement;
    private Animator animator;
    private Rigidbody rb;
    private Quaternion rotation = Quaternion.identity;
    private AudioSource audioSource;

    // HeaderAttribute : 변수에 설명을 넣을 때 사용
    [HeaderAttribute("회전 속도")]
    public float turnSpeed = 20f;

    void Start()
    {
        // GetComponent : 컴퍼넌트를 불러옴
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	void FixedUpdate()
	{
        // horizontal : 가로축 입력
        // vertical : 세로축 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 벡터 입력
        movement.Set(horizontal, 0f, vertical);

        // 정규화
        movement.Normalize();
        
        // Mathf.Approximately : 근사값 (입력값, 원하는 값)
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("IsWalking", isWalking);
        // 걸을 때 발자국 소리 나게끔 체크 : isWalking
        if (isWalking)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        // desiredFoward : 바라볼 방향 계산
        Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, 
            movement, turnSpeed * Time.fixedDeltaTime, 0f);
        // Quaternion.LookRotation : 벡터 방향으로 쳐다본다
        rotation = Quaternion.LookRotation(desiredFoward);
	}

    private void OnAnimatorMove()
    {
        // rb.MovePosition : 위치 이동
        // rb.position : 현재 위치,
        // movement : 입력 받아 움직이는 벡터
        // aniamtor.deltaPosition.magnitude : 3D애니메이션에서 움직이는 양
        rb.MovePosition(rb.position + 
            movement * animator.deltaPosition.magnitude);
        // rb.MoveRotation : 회전
        rb.MoveRotation(rotation);
    }
}
