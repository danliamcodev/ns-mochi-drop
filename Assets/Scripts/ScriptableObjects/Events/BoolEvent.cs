using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Bool Event", menuName = "Events/Bool Event")]
public class BoolEvent : BaseGameEvent<bool>
{

}

[System.Serializable]
public class BoolUnityEvent : UnityEvent<bool>
{

}
