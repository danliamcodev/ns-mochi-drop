using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MochiCombiner : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] ObjectPoolController _mochiPoolController;

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

        MochiType newMochiType = p_mochiPair.mochi1.mochiType;
        //check if there's still a level after
        for (int i = 0; i < _gameSettings.combineMatrix.Count; i++)
        {
            if (p_mochiPair.mochi1.mochiType == _gameSettings.combineMatrix[i])
            {
                if ((i + 1) < _gameSettings.combineMatrix.Count)
                {
                    newMochiType = _gameSettings.combineMatrix[i + 1];
         
                    break;
                } else
                {
                    yield break;
                }
            } 
        }

        yield return new WaitForSeconds(_gameSettings.combineDelay);

        DeactivateMochi(p_mochiPair.mochi1);
        DeactivateMochi(p_mochiPair.mochi2);

        Vector2 middlePoint = Vector2.Lerp(p_mochiPair.mochi1.transform.position, p_mochiPair.mochi2.transform.position, 0.5f);
        p_mochiPair.mochi1.transform.DOMove(middlePoint, _gameSettings.combineSpeed);
        p_mochiPair.mochi2.transform.DOMove(middlePoint, _gameSettings.combineSpeed);

        yield return new WaitForSeconds(_gameSettings.scaleDelay);

        _mochiPoolController.ReturnObject(p_mochiPair.mochi2.gameObject);


        p_mochiPair.mochi1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Vector2 oldScale = p_mochiPair.mochi1.transform.localScale;

        p_mochiPair.mochi1.ConfigureMochi(newMochiType);
        p_mochiPair.mochi1.transform.localScale = oldScale;
        p_mochiPair.mochi1.transform.DOScale(newMochiType.scale, _gameSettings.scaleSpeed);

        _combineQueue.Remove(p_mochiPair.mochi1);
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
