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

    public Text textScore;

    public int score;
    private int consecutivesBites;


    void Start() {
        InitializeScoreManager();
    }

    public void InitializeScoreManager()
    {
        emergencyState = false;
        timerCombo = originalTimerCombo;
        score = 0;
        consecutivesBites = 0;
        textScore.text = "0";
    }

    void Update () {
        if (GameManagerScript.Instance.gameActive) { 
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
                insectEaten = false;

                if (timerCombo < 0.0f)
                {
                    emergencyState = false;
                    comboActivated = false;
                    currentBonusPoints = originalBonusPoints; // reset bonus points;
                    ResetTimerCombo(); // reset timer;
                    consecutivesBites = 0;
                }

                if (timerCombo < 4.0f)
                {
                    emergencyState = true;
                }
            }
            switch (consecutivesBites) // play sound
            {
                case 2: break;

                case 5: break;
            }
        }
    }

    private void ResetTimerCombo()
    {
        timerCombo = originalTimerCombo;
    }

    private void AddScore()
    {
        score += currentBonusPoints;
        textScore.GetComponent<Animator>().Play("ZoomInOut");
        textScore.text = score.ToString(); // update score text
    }
}
