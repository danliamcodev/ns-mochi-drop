using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Mochi Combiner")]
    [SerializeField] float _combineSpeed;
    [SerializeField] float _scaleSpeed;
    [SerializeField] float _combineDelay;
    [SerializeField] float _scaleDelay;

    public float combineSpeed { get { return _combineSpeed; } }
    public float scaleSpeed { get { return _scaleSpeed; } }
    public float combineDelay { get { return _combineDelay; } }
    public float scaleDelay { get { return _scaleDelay; } }
}
