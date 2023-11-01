using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaZoneDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] MochiSet _mochisInPlayArea;

    private void Start()
    {
        _mochisInPlayArea.items.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Mochi>(out Mochi mochi))
        {
            _mochisInPlayArea.Add(mochi);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Mochi>(out Mochi mochi))
        {
            _mochisInPlayArea.Remove(mochi);
        }
    }
}
