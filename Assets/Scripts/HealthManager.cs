using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] private Text healthText;

    int health = 2;
    float invTime = 2f;
    float currentInvTime = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Application.Quit();

            //UnityEditor.EditorApplication.isPlaying = false;
        }

        currentInvTime += Time.deltaTime;
    }

    public void TakeHit()
    {
        if (currentInvTime >= invTime)
        {
            health--;
            Debug.Log(health);
            currentInvTime = 0f;

            healthText.text = "Health: " + health;
        }
    }

    public void GetHealth(int value)
    {
        health += value;

        healthText.text = "Health: " + health;
    }
}
