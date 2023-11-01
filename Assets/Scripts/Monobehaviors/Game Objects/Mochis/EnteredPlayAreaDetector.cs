using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnteredPlayAreaDetector : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] UnityEvent _onPlayAreaEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayAreaZoneDetection>())
        {
            _onPlayAreaEntered.Invoke();
        }
    }
}
