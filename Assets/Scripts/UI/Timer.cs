using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
    public TextMeshProUGUI timerText;
    public Slider slider;
    private float maxMinutes;
    private GameManager game;
    private float minutes, seconds;
    private float timerValueAtPause;
    private float elapsedTimeWhenPaused;
    private bool isPaused;

    public void Awake() {
        game = this.GetComponentInParent<GameManager>();
        initTimerValue = Time.time;
        slider.maxValue = maxMinutes * 60;
    }

    // Start is called before the first frame update
    public void Start() {
        elapsedTimeWhenPaused = 0f;
        timerValueAtPause = 0f;
        initTimerValue = 0f;
        slider.minValue = 0f;
        timerText.text = "TIMER : " + string.Format("{0:00}:{1:00}", 0, 0);
        slider.value = 0f;
        isPaused = false;
    }

    // Update is called once per frame
    public void Update() {
        //to get the actual time since the start of the timer
        if (game.GetGameRunningStatus())
        {
            if (!verifyIfTimeIsUp() && !isPaused)
            {
                float time = Time.time - initTimerValue - elapsedTimeWhenPaused;
                UpdateTimerDislay(time);
                verifyIfTimeIsUp();
            }
        }
    }

    // To give the timer max value
    public void SetTimer(float timer)
    {
        maxMinutes = timer;
    }

    // To pause the timer
    public void PauseTimer()
    {
        isPaused = true;
        timerValueAtPause = Time.time;
    }

    // To resume the timer
    public void ResumeTimer()
    {
        elapsedTimeWhenPaused += Time.time - timerValueAtPause;
        isPaused = false;
    }

    // To update the display of the timer
    private void UpdateTimerDislay(float time)
    {
        slider.value = time;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        timerText.text = "TIMER : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // To verify if the time's up according the maximum previously fixed
    public bool verifyIfTimeIsUp()
    {
        return (minutes >= maxMinutes);
    }

    // To reset the timer
    public void resetTimer()
    {
        initTimerValue = 0f;
        timerValueAtPause = 0f;
        elapsedTimeWhenPaused = 0f;
        minutes = 0f;
        seconds = 0f;
    }

    // To get the elapsed time in seconds
    public int GetElapsedTime()
    {
        return (int) (minutes * 60 + seconds);
    }
}
