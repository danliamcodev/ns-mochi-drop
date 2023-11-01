using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverConditionChecker : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _gameOver;

    List<Mochi> _mochisInArea = new List<Mochi>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Mochi>(out Mochi mochi))
        {
            _mochisInArea.Add(mochi);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Mochi>(out Mochi mochi))
        {
            _mochisInArea.Remove(mochi);
        }
    }

    public void OnMochiLanded()
    {
        if (_mochisInArea.Count > 0)
        {
            _gameOver.Raise();
        }
    }
}
