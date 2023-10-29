using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Mochi Event", menuName = "Events/Mochi Event")]
public class MochiEvent : BaseGameEvent<Mochi>
{

}

[System.Serializable]
public class MochiUnityEvent : UnityEvent<Mochi>
{

}
