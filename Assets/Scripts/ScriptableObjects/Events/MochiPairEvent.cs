using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New MochiPair Event", menuName = "Events/MochiPair Event")]
public class MochiPairEvent : BaseGameEvent<MochiPair>
{

}

[System.Serializable]
public class MochiPairUnityEvent : UnityEvent<MochiPair>
{

}
