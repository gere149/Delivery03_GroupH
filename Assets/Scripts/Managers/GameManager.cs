using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public ElementInfo shopInfo;

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
        playerInfo.ResetHealth();
        playerInfo.ResetMoney();

        shopInfo.ResetMoney();
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