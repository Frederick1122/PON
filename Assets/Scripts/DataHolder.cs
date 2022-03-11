using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour
{
    public static DataHolder main;

    [Header("UIElements")]
    [SerializeField] private Slider carmaSlider;
    [SerializeField] private Text FPScore;
    [SerializeField] private Text SPScore;
    [SerializeField] private Text timerText;
    [Space]
    [Header("LevelSettings")]
    public float rebirthTime;
    public int RangeSpeed;
    public int minutes;

    [Space(5)]
    [Header("FOR DEBUG")]
    public int[] coins;
    [SerializeField]private int carma;

    //FP - first player, SP - second player

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
        coins[0] = 0; coins[1] = 0;
        RefreshScore();
        StartCoroutine(Timer());
    }
    public void TakeCoin(string tag, int quantity = 1)
    {
        if (tag == "Player1")
        {
            coins[0] += quantity;

        }
        else
        {
            coins[1] += quantity;
        }
        RefreshScore();
    }

    public void RefreshScore()
    {
        FPScore.text = $"Score: \n{coins[0].ToString()}";
        SPScore.text = $"Score: \n{coins[1].ToString()}";
    }

    IEnumerator Timer()
    {
        while (seconds > 0)
        {
            yield return new WaitForSeconds(1f);
            seconds--;
            timerText.text = (seconds % 60 < 10) ? $"{seconds / 60}:0{seconds % 60}" : $"{seconds / 60}:{seconds % 60}";
            if (seconds == 0)
            {
                break;
            }
        }

        Restart();
    }

    public void ChangeCarma(int _modificationCarma)
    {
        carma += _modificationCarma;
        if(carma >= 100)
        {
            PlatformManager.main.RegionCapture(true);
            carma = 0;
        } else if (carma <= -100)
        {
            PlatformManager.main.RegionCapture(false);
            carma = 0;
        }
        carmaSlider.value = carma;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
