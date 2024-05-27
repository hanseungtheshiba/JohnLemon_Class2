using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [Header("페이드 시간")]
    [SerializeField]
    private float fadeDuration = 1f;
    [Header("이미지가 출력되는 시간")]
    private float displayImageDuration = 1f;
    [Header("플레이어 게임오브젝트")]
    [SerializeField]
    private GameObject player;
    [Header("성공 캔버스 그룹")]
    [SerializeField]
    private CanvasGroup exitBackgroundImageGroup = null;
    [Header("실패 캔버스 그룹")]
    [SerializeField]
    private CanvasGroup caughtBackgroundImageGroup = null;
    [Header("성공 사운드")]
    [SerializeField]
    private AudioSource exitAudio;
    [Header("실패 사운드")]
    [SerializeField]
    private AudioSource caughtAudio;

    private bool isPlayerExit = false;
    private bool isPlayerCaught = false;

    private bool hasAudioPlayed = false;

    private float timer = 0f;

    private void Update()
    {
        if (isPlayerExit == true)
        {
            EndLevel(exitBackgroundImageGroup, false, exitAudio);
        }
        else if(isPlayerCaught == true)
        {
            EndLevel(caughtBackgroundImageGroup, true, caughtAudio);
        }
    }

    private void EndLevel(CanvasGroup imageGroup, bool doRestart, 
        AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        // timer = timer + Time.deltaTime;

        imageGroup.alpha = timer / fadeDuration;

        if(timer > fadeDuration + displayImageDuration)
        {
            if (doRestart == true)
            {
                SceneManager.LoadScene(0);
            }
            else 
            {
                Application.Quit();
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // .Equals : ==과 같으나 성능이 더 좋음
        if (other.gameObject.Equals(player))
        {
            isPlayerExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }
}
