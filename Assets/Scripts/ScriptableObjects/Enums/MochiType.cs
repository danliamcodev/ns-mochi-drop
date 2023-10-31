using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mochi Type", menuName = "Mochis/Mochi Type")]
public class MochiType : ScriptableObject
{
    [Header("Variables")]
    [SerializeField] Color _color;
    [SerializeField] float _scale;
    [SerializeField] int _points;

    public Color color { get { return _color; } }
    public float scale { get { return _scale; } }
    public int points { get { return _points; } }
}
