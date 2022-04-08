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
    public float maxMinutes = 5;
    private GameManager game;
    private float minutes, seconds;

    public void Awake() {
        game = this.GetComponentInParent<GameManager>();
        initTimerValue = Time.time;
    }

    // Start is called before the first frame update
    public void Start() {
        initTimerValue = 0f;
        slider.minValue = 0f;
        slider.maxValue = maxMinutes * 60;
        timerText.text = "TIMER : " + string.Format("{0:00}:{1:00}", 0, 0);
        slider.value = 0f;
    }

    // Update is called once per frame
    public void Update() {
        //to get the actual time since the start of the timer
        if (game.GetGameRunningStatus())
        {
            if (!verifyIfTimeIsUp())
            {
                float time = Time.time - initTimerValue;
                UpdateTimerDislay(time);
                verifyIfTimeIsUp();
            }
        }
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
        minutes = 0f;
        seconds = 0f;
    }
}
