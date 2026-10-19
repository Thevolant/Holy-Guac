using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private int checkpointNum;
    private Checkpoint checkpoint;
    private int lastCheckpoint;
    public GameObject player;
    public Transform resetPoint;


    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Checkpoint")
        {
            checkpoint = collider.gameObject.GetComponent<Checkpoint>();
            checkpointNum = checkpoint.checkpointNum + 1;

            
            if (checkpointNum > lastCheckpoint || lastCheckpoint == 0)
            {
                lastCheckpoint = checkpointNum;
                resetPoint = collider.gameObject.transform;
            }
        }
    }

    public void RespawnAtLastCheckpoint()
    {
        Debug.Log(resetPoint.position);
        player.transform.position = resetPoint.position + new Vector3(0f, 1f, 0f);
        player.SetActive(true);
        Debug.Log("Respawn");
        
    }
}
