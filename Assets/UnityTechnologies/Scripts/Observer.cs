using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [Header("플레이어 오브젝트")]
    [SerializeField]
    private GameObject player;
    [Header("게임 엔딩 트리거")]
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
            // direction : 가고일이 쳐다보는 방향
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            // Ray : 어떤 방향으로 레이저 발사
            Ray ray = new Ray(transform.position, direction);
            // RaycastHit : 맞은 것
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
