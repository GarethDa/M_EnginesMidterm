using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    [SerializeField] private int worth;

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            ScoreManager.instance.ChangeScore(worth);
            Destroy(gameObject);
        }
    }
}
