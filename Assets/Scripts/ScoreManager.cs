using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private Text moneyText;
    [SerializeField] private int winValue = 10;
    [SerializeField] private Text winText;

    private int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        winText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= winValue)
        {
            winText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        Debug.Log(score);

        moneyText.text = "Money: " + score;
    }
}
