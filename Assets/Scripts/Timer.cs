using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    // in secs
    public int initialTimeInSecs;
    public LevelManager levelManager;
    public GameObject challengeLocked;
    public bool timeIsSaved;

    float _startTime;
    float _time;

    bool stop = false;

    int secsCortesia = 1;

	// Use this for initialization
	void Start () {
        // damos 1 segundo de cortesía, porque si no prácticamente 
        // aparece el cronómetro con un segundo ya descontado
        initialTimeInSecs += secsCortesia;

        if(timeIsSaved) // cogemos el tiempo del gameManager
        {
            GameManager gm = FindObjectOfType<GameManager>();
            int lastTime = gm.GetLastChallengeTime().Hour * 3600 + gm.GetLastChallengeTime().Minute * 60 + gm.GetLastChallengeTime().Second;
            int nowTime = System.DateTime.Now.Hour * 3600 + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Second;
            _startTime = Time.time + (initialTimeInSecs - (nowTime-lastTime));
        }
        else // cogemos el tiempo actual
            _startTime = Time.time + initialTimeInSecs;
	}
	
	// Update is called once per frame
	void Update () {
        if (!stop)
        {
            _time = _startTime - Time.time;
        }
        if (_time > 0)
        {
            // por el seg de cortesía, para que nunca pueda aparecer por pantalla 
            // un valor mayor al comienzo del cronómetro
            if (_time > initialTimeInSecs - secsCortesia)
                _time = initialTimeInSecs - secsCortesia;

            // tensión de cronómetro de challenge
            if(levelManager && !stop && _time < 10)
            {
                SoundManager sm = FindObjectOfType<SoundManager>();
                if (sm)
                    sm.PlayTimerSound();
            }
            string min, seg;
            //min
            if (_time >= 60)
            {
                if (_time >= 600)
                    min = ((int)_time / 60).ToString();
                else
                    min = "0" + ((int)_time / 60).ToString();
            }

            else
                min = "00";

            //seg
            if (_time % 60 == 0)
                seg = "00";
            else if (_time % 60 < 10)
                seg = "0" + ((int)_time % 60).ToString();
            else
                seg = ((int)_time % 60).ToString();

            GetComponent<Text>().text = min + ":" + seg;
        }
        // se acabó
        else
        {
            GetComponent<Text>().text = "00:00";

            if (levelManager)
                levelManager.Lose();
            else if (challengeLocked)
                challengeLocked.SetActive(false);
        }
    }
    public void Stop() { stop = true; }
}
