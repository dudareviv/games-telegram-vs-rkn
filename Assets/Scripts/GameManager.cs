using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameObject Player;

    public UnityEvent OnRestart = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();

    public void GameOver()
    {
        OnGameOver.Invoke();

        Time.timeScale = 0;

        PlayerPrefs.Save();
    }

    public void Restart()
    {
        OnRestart.Invoke();

        Time.timeScale = 1;

        Player.transform.position = Vector2.zero;
        Player.transform.rotation = Quaternion.identity;
        HealthManager.Instance.Reset();
        ScoreManager.Instance.Reset();
        GameObjectsPoolsManager.Instance.Reset();
    }
}