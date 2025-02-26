using UnityEngine;

public class GameObjectKnower : MonoBehaviour
{
    public ElementInfo elementInfo;

    void Start()
    {
        bool isPlayer = elementInfo is PlayerInfo;
        elementInfo.SetOwner(isPlayer);
    }
}