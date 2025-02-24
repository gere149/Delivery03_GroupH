using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private AudioSource audioSource;

    //private System.Collections.IEnumerator WaitForSoundAndChangeScene()
    //{
    //    yield return new WaitForSeconds(audioSource.clip.length);
    //    SceneManager.LoadScene("Gameplay");
    //}

    //private void Awake()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}

    private void OnEnable()
    {
        //ACTIVAR CUANDO NO TENGAMOS VIDA
    }

    private void OnDisable()
    {
        //ACTIVAR CUANDO NO TENGAMOS VIDA
    }

    public void Play()
    {
        //if (audioSource != null)
        //{
        //    audioSource.Play();
        //    StartCoroutine(WaitForSoundAndChangeScene());
        //}
        //else
        //{
        //    SceneManager.LoadScene("Gameplay");
        //}

        SceneManager.LoadScene("Gameplay");
    }

    private void LoseEndScene()
    {
        SceneManager.LoadScene("Ending");
    }

    void OnEnter()
    {
        Play();
    }

    void OnExit()
    {
        Application.Quit();
    }
}