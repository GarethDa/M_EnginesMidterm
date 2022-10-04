using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    
    [SerializeField] [Range(1.0f, 10.0f)] private float timeBetweenPoints = 2.0f;

    [SerializeField] private bool isMovingHorizontal = false;

    float t = 0f;
    float currentTime = 0f;
    Vector3 startingPoint;
    Vector3 aimTowards;
    Vector3 currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
        aimTowards = targetPoint.position;
        currentPoint = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        t = currentTime / timeBetweenPoints;

        if (t >= 1f)
        {
            //If we just reached the target point, swap to start heading towards the start
            if (aimTowards == targetPoint.position)
            {
                aimTowards = startingPoint;
                currentPoint = targetPoint.position;
            }

            //Otherwise, swap to start heading towards the target point
            else 
            {
                aimTowards = targetPoint.position;
                currentPoint = startingPoint;
            }

            //Reset t and the current time
            t = 0f;
            currentTime = 0f;

            if (isMovingHorizontal) transform.LookAt(aimTowards);
        }

        else
        {
            //Lerp between the two points
            transform.position = Vector3.Lerp(currentPoint, aimTowards, t);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            HealthManager.instance.TakeHit();
        }

        else if (other.collider.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
