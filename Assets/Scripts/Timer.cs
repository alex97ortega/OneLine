using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    // in secs
    public int initialTimeInSecs;
    public LevelManager levelManager;

    float _startTime;
    float _time;

    bool stop = false;

    int secsCortesia = 1;

	// Use this for initialization
	void Start () {
        // damos 1 segundo de cortesía, porque si no prácticamente 
        // aparece el cronómetro con un segundo ya descontado
        initialTimeInSecs += secsCortesia;
        _startTime = Time.time + initialTimeInSecs;
	}
	
	// Update is called once per frame
	void Update () {
        if(!stop)
            _time = _startTime - Time.time;
        if (_time > 0)
        {
            // por el seg de cortesía, para que nunca pueda aparecer por pantalla 
            // un valor mayor al comienzo del cronómetro
            if (_time > initialTimeInSecs - secsCortesia)
                _time = initialTimeInSecs - secsCortesia;

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
            if (levelManager)
                levelManager.Lose();
            GetComponent<Text>().text = "00:00";
        }
    }
    public void Stop() { stop = true; }
}
