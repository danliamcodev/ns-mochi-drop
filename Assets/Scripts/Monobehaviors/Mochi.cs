using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] MochiPairEvent _mochiCollided;

    [Header("References")]
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Variables")]
    [SerializeField] MochiType _mochiType;

    public MochiType mochiType { get { return _mochiType; } }

    private void Start()
    {
        ConfigureMochi(_mochiType);
    }

    public void ConfigureMochi(MochiType p_mochiType)
    {
        _mochiType = p_mochiType;
        _spriteRenderer.color = _mochiType.color;
        transform.localScale = new Vector3(_mochiType.scale, _mochiType.scale, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (collision.gameObject.TryGetComponent<Mochi>(out Mochi mochi))
        {
            print(string.Format("Collided {0} and {1}", _mochiType, mochi.mochiType));
            if (mochi.mochiType == _mochiType)
            {
                MochiPair mochiPair = new MochiPair(this, mochi);
                _mochiCollided.Raise(mochiPair);
            } 
        }
    }
}
