using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiLandedDetector : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _mochiLanded;
    [SerializeField] VoidEvent _checkGameOver;

    private bool _mochiHasLanded = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_mochiHasLanded) return;
        _mochiHasLanded = true;
        _mochiLanded.Raise();

        if (collision.gameObject.TryGetComponent<Mochi>(out Mochi mochi))
        {
            if (mochi.mochiType != GetComponent<Mochi>().mochiType)
            {
                _checkGameOver.Raise();
            }
        }
    }

    public void Reset()
    {
        _mochiHasLanded = false;
    }
}
