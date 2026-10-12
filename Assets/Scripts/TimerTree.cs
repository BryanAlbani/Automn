using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerTree : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public float targetTime = 20.0f;

    private bool timerDone = false;

    private void Start()
    {
        targetTime = DataManager.Instance.maxSpawnDuration;
        timerText.text = "Time left: " + Mathf.Round(targetTime).ToString() + "s";
    }

    private void Update()
    {
        if (!timerDone)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
            timerText.text = "Time left: " + Mathf.Max(0, Mathf.Round(targetTime)).ToString() + "s";
        }
    }

    void timerEnded()
    {
        
        SceneManager.LoadScene("UpgradeScene");
        
    }
}
