using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartUnityEvents : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] UnityEvent _actions;
    // Start is called before the first frame update
    void Start()
    {
        _actions.Invoke();
    }
}
