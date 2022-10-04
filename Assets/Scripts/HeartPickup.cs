using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private int worth;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            HealthManager.instance.GetHealth(worth);
            Destroy(gameObject);
        }
    }
}
