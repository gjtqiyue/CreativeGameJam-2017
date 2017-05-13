using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int currentBonusPoints;

    public float timerCombo; // timer that is decreasing over time
    public float originalTimerCombo; // fixed time for triggering a combo

    public bool insectEaten; // trigger boolean when insect eaten
    public bool comboActivated; // combo activated
    public bool emergencyState;

    public int originalBonusPoints;

    public Text textTimer;
    public Text textScore;

    private int score;
    private int consecutivesBites;


    void Start () {
        emergencyState = false;
        timerCombo = originalTimerCombo;
        textTimer.text = timerCombo.ToString();
        score = 0;
        consecutivesBites = 0;
    }

    void Update () {
        if (Input.GetKeyDown("s"))
        {
            insectEaten = true;
            comboActivated = true;
            consecutivesBites++;
        }
        if (comboActivated)
        {
            if (insectEaten)
            {
                ResetTimerCombo(); // reset timer
                AddScore();
                currentBonusPoints *= 2; // increase score for next bite
                emergencyState = false;
            }

            timerCombo -= Time.deltaTime;
            textTimer.text = string.Format("{0:00.00}", timerCombo);
            insectEaten = false;

            if (timerCombo < 0.0f)
            {
                emergencyState = false;
                comboActivated = false;
                currentBonusPoints = originalBonusPoints; // reset bonus points;
                ResetTimerCombo(); // reset timer;
                consecutivesBites = 0;
            }

            if (timerCombo < 2.0f)
            {
                emergencyState = true;
            }
        }
    }

    private void ResetTimerCombo()
    {
        timerCombo = originalTimerCombo;
        textTimer.text = string.Format("{0:00.00}", timerCombo);
    }

    private void AddScore()
    {
        score += currentBonusPoints;
        textScore.text = "Score " + score.ToString(); // update score text
    }
}
