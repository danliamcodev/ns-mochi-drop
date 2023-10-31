using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New MochiType Event", menuName = "Events/MochiType Event")]
public class MochiTypeEvent : BaseGameEvent<MochiType>
{

}

[System.Serializable]
public class MochiTypeUnityEvent : UnityEvent<MochiType>
{

}
