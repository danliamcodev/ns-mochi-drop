using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] MochiPairEvent _mochiCollided;

    [Header("Variables")]
    [SerializeField] MochiType _mochiType;

    public MochiType mochiType { get { return _mochiType; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Mochi>(out Mochi mochi))
        {
            if (mochi.mochiType == _mochiType)
            {
                MochiPair mochiPair = new MochiPair(this, mochi);
                _mochiCollided.Raise(mochiPair);
            }
        }
    }
}
