using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public int checkpointNum;
    void Start()
    {
        checkpointNum = transform.GetSiblingIndex();
    }
}
