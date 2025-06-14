using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Coin 
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
    protected override void OnStartInteract(CharacterController fps)
    {
        CoinCollector.Instance.AddCoin();
        Destroy(gameObject);

    }
}
