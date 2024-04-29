using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [Header("�÷��̾� ������Ʈ")]
    [SerializeField]
    private GameObject player;
    [Header("���� ���� Ʈ����")]
    [SerializeField]
    private GameEnding gameEnding;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            // direction : �������� �Ĵٺ��� ����
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            // Ray : � �������� ������ �߻�
            Ray ray = new Ray(transform.position, direction);
            // RaycastHit : ���� ��
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.gameObject.Equals(player))
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
