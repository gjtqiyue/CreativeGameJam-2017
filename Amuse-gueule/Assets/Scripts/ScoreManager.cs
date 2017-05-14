using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public int currentBonusPoints;

    public float timerCombo; // timer that is decreasing over time
    public float originalTimerCombo; // fixed time for triggering a combo

    public bool insectEaten; // trigger boolean when insect eaten
    public bool comboActivated; // combo activated
    public bool emergencyState;
    public GameObject chefImage;

    public int originalBonusPoints;

    public Text textScore;
    public Text highScore;

    public int score;
    private int consecutivesBites;


    void Start() {
        InitializeScoreManager();
    }

    public void InitializeScoreManager()
    {
        comboActivated = false;
        emergencyState = false;
        timerCombo = originalTimerCombo;
        score = 0;
        consecutivesBites = 0;
        textScore.text = "0";
        if (ScoreSaveLoad.sortedScores != null && ScoreSaveLoad.sortedScores.Count > 0)
        {
            highScore.text = ScoreSaveLoad.sortedScores[0].ToString();
        }
        else
        {
            highScore.text = "---";


        }
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

                if (timerCombo < 2.0f)
                {
                    emergencyState = true;
                }
            }
            
        }
    }

    private void ResetTimerCombo()
    {
        timerCombo = originalTimerCombo;
    }

    public void Eat()
    {
        insectEaten = true;
        comboActivated = true;
        consecutivesBites++;
        switch (consecutivesBites) // play sound
        {
            case 2:
                chefImage.GetComponent<Animator>().Play("ChefSliding");
                break;

            case 5:
                chefImage.GetComponent<Animator>().Play("ChefSliding");
                break;
        }
    }

    public void AddScore()
    {
        score += currentBonusPoints;
        textScore.GetComponent<Animator>().Play("ZoomInOut");
        textScore.text = score.ToString(); // update score text
    }
}
