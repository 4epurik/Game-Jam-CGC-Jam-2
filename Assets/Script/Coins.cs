using Script;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 200;

    void Start()
    {
        CoinCollector.Instance.Init();
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CoinCollector.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}