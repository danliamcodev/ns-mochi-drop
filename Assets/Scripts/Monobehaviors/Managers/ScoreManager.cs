using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _scoreUpdated;
    [Header("References")]
    [SerializeField] IntVariable _playerScore;
    private void Start()
    {
        ResetScore();
    }

    public void OnMochiCombined(MochiType p_mochiType)
    {
        _playerScore.ApplyChange(p_mochiType.points);
        _scoreUpdated.Raise();
    }

    public void ResetScore()
    {
        _playerScore.SetValue(0);
        _scoreUpdated.Raise();
    }
}
