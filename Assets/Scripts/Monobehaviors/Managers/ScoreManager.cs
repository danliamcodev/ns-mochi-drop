using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _scoreUpdated;
    [Header("References")]
    [SerializeField] IntVariable _playerScore;
    [SerializeField] MochiSet _mochisInPlayArea;
    private void Start()
    {
        ResetScore();
    }

    public void CalculateScore()
    {
        int score = 0;
        foreach (Mochi mochi in _mochisInPlayArea.items)
        {
            score += mochi.mochiType.points;
        }
        _playerScore.SetValue(score);
        _scoreUpdated.Raise();
    }

    public void ResetScore()
    {
        _playerScore.SetValue(0);
        _scoreUpdated.Raise();
    }
}
