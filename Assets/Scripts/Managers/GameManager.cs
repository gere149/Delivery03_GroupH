using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerInfo.OnDie += LoseEndScene;
    }

    private void OnDisable()
    {
        PlayerInfo.OnDie -= LoseEndScene;
    }

    public void Play()
    {
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