using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiLandedDetector : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] VoidEvent _mochiLanded;

    private bool _mochiHasLanded = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_mochiHasLanded) return;
        _mochiHasLanded = true;
        _mochiLanded.Raise();
    }

    public void Reset()
    {
        _mochiHasLanded = false;
    }
}
