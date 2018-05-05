using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreDisplay : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void OnScoreUpdate(int value)
    {
        if (_text != null)
            _text.text = value.ToString();
    }

    public void ShowScoreOnGameOver()
    {
        if (_text != null)
            _text.text = ScoreManager.Instance.Score.ToString();
    }

    public void ShowHighscoreOnGameOver()
    {
        if (_text != null)
            _text.text = ScoreManager.Instance.Highscore.ToString();
    }
}