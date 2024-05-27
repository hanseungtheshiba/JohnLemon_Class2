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

    // HeaderAttribute : ������ ������ ���� �� ���
    [HeaderAttribute("ȸ�� �ӵ�")]
    public float turnSpeed = 20f;

    void Start()
    {
        // GetComponent : ���۳�Ʈ�� �ҷ���
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	void FixedUpdate()
	{
        // horizontal : ������ �Է�
        // vertical : ������ �Է�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ���� �Է�
        movement.Set(horizontal, 0f, vertical);

        // ����ȭ
        movement.Normalize();
        
        // Mathf.Approximately : �ٻ簪 (�Է°�, ���ϴ� ��)
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("IsWalking", isWalking);
        // ���� �� ���ڱ� �Ҹ� ���Բ� üũ : isWalking
        if (isWalking)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        // desiredFoward : �ٶ� ���� ���
        Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, 
            movement, turnSpeed * Time.fixedDeltaTime, 0f);
        // Quaternion.LookRotation : ���� �������� �Ĵٺ���
        rotation = Quaternion.LookRotation(desiredFoward);
	}

    private void OnAnimatorMove()
    {
        // rb.MovePosition : ��ġ �̵�
        // rb.position : ���� ��ġ,
        // movement : �Է� �޾� �����̴� ����
        // aniamtor.deltaPosition.magnitude : 3D�ִϸ��̼ǿ��� �����̴� ��
        rb.MovePosition(rb.position + 
            movement * animator.deltaPosition.magnitude);
        // rb.MoveRotation : ȸ��
        rb.MoveRotation(rotation);
    }
}
