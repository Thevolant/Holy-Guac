using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 coinRotation = new Vector3(0f, 50f, 0f);

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(coinRotation * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
