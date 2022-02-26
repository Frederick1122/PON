using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour
{
    public static DataHolder main;
    //FP - first player, SP - second player
    [Header("FOR DEBUG")]
    public int FPCoins;
    public int SPCoins;
    public int carma;
    public int minutes;
    [SerializeField] private Text FPScore;
    [SerializeField] private Text SPScore;
    [SerializeField] private Text timerText;

    private int seconds;
    private void Awake()
    {
        if (main != null && main != this)
        {

            Destroy(this);
            return;
        }

        main = this;
    }
    private void Start()
    {
        seconds = minutes * 60;
        timerText.text = (seconds % 60 < 10) ? $"{seconds / 60}:0{seconds % 60}" : $"{seconds / 60}:{seconds % 60}";
        FPCoins = 0; SPCoins = 0;
        RefreshScore();
        StartCoroutine(Timer());
    }
    public void TakeCoin(string tag, int quantity = 1)
    {
        if (tag == "Player1")
        {
            FPCoins += quantity;

        }
        else
        {
            SPCoins += quantity;
        }
        RefreshScore();
    }

    public void RefreshScore()
    {
        FPScore.text = FPCoins.ToString();
        SPScore.text = SPCoins.ToString();
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            seconds--;
            timerText.text = (seconds % 60 < 10) ? $"{seconds / 60}:0{seconds % 60}" : $"{seconds / 60}:{seconds % 60}";
            if (seconds == 0)
            {
                break;
            }
        }
    }
}
