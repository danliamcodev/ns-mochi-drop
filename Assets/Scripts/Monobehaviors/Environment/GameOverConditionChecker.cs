using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverConditionChecker : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _gameOver;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Mochi>())
        {
            _gameOver.Raise();
        }
    }
}
