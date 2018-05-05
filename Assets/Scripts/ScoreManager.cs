using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    const string HIGHSCORE = "HIGHSCORE";

    public int Score = 0;
    public int Highscore = 0;

    public float ScoreIncrementPerSecond = 1;

    private float TimeElapsed = 0;

    public IntUnityEvent OnScoreUpdate = new IntUnityEvent();

    private void Awake()
    {
        Highscore = PlayerPrefs.GetInt(HIGHSCORE, 0);

        Reset();
    }

    private void FixedUpdate()
    {
        IncrementScore();
    }

    private void IncrementScore()
    {
        TimeElapsed -= Time.fixedDeltaTime;

        if (TimeElapsed > 0)
            return;

        AddScore(1);
        TimeElapsed = 1f / ScoreIncrementPerSecond;
    }

    public void AddScore(int value)
    {
        Score += value;

        OnScoreUpdate.Invoke(Score);
    }


    public void Reset()
    {
        Score = 0;

        OnScoreUpdate.Invoke(Score);
    }

    public void SaveScore()
    {
        if (Score > Highscore) {
            Highscore = Score;
            PlayerPrefs.SetInt(HIGHSCORE, Highscore);
        }
    }

    public int CoinScoreMin = 10;
    public int CoinScoreMax = 50;
    
    public void CollectCoin()
    {
        AddScore(Random.Range(CoinScoreMin, CoinScoreMax));   
    }
}