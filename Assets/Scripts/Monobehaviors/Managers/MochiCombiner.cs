using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MochiCombiner : MonoBehaviour
{
    List<Mochi> _combineQueue = new List<Mochi>();

    public void OnMochiCollided(MochiPair p_mochiPair)
    {
        if (!_combineQueue.Contains(p_mochiPair.mochi1) && !_combineQueue.Contains(p_mochiPair.mochi2))
        {
            _combineQueue.Add(p_mochiPair.mochi1);
            StartCoroutine(CombineMochiTask(p_mochiPair));
        } 
    }

    private IEnumerator CombineMochiTask(MochiPair p_mochiPair)
    {
        DeactivateMochi(p_mochiPair.mochi1);
        DeactivateMochi(p_mochiPair.mochi2);

        Vector2 middlePoint = Vector2.Lerp(p_mochiPair.mochi1.transform.position, p_mochiPair.mochi2.transform.position, 0.5f);
        p_mochiPair.mochi1.transform.DOMove(middlePoint, 1f);
        p_mochiPair.mochi2.transform.DOMove(middlePoint, 1f);

        yield return new WaitForSeconds(1f);

        p_mochiPair.mochi2.gameObject.SetActive(false);


        p_mochiPair.mochi1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //p_mochiPair.mochi2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        p_mochiPair.mochi1.transform.DOScale(2f, 1f);
        p_mochiPair.mochi2.transform.DOScale(2f, 1f);

        yield return null;
    }

    private void DeactivateMochi(Mochi p_mochi)
    {
        p_mochi.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        p_mochi.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

[System.Serializable]
public class MochiPair
{
    [SerializeField] Mochi _mochi1;
    [SerializeField] Mochi _mochi2;

    public Mochi mochi1 { get { return _mochi1; } }
    public Mochi mochi2 { get { return _mochi2; } }
    public MochiPair (Mochi p_mochi1, Mochi p_mochi2)
    {
        _mochi1 = p_mochi1;
        _mochi2 = p_mochi2;
    }
}
