﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadeUILevelTransitions : MonoBehaviour {

    public GameObject panel;
    public GameObject texts;
    public GameObject texts2;

    private int level;

	void Start () {
        level = 1;
        panel.SetActive(false);
        Data.Instance.events.OnListenerDispatcher += OnListenerDispatcher;
	}
    void OnDestroy()
    {
        Data.Instance.events.OnListenerDispatcher -= OnListenerDispatcher;
    }
    int percent = 0;
    bool ready;

    void OnListenerDispatcher(string type)
    {
        print("OnListenerDispatcher : " + type);
        if (type == "Ralenta")
        {
            panel.SetActive(true);
            foreach (Text field in texts.GetComponentsInChildren<Text>())
                field.text = "Bien hecho!";
             foreach (Text field in texts2.GetComponentsInChildren<Text>())
                field.text = "";
            return;
        }
        else if (type == "BonusEntrande")
        {
            panel.SetActive(true);
            foreach (Text field in texts.GetComponentsInChildren<Text>())
                field.text = "B O N U S !!!";
            foreach (Text field in texts2.GetComponentsInChildren<Text>())
                field.text = "";
            StartCoroutine(DoFade());
            return;
        }

        if (type == "LevelFinish_hard") percent += 100;
        else if (type == "LevelFinish_medium") percent += 66;
        else if (type == "LevelFinish_easy") percent += 33;

        if(percent==0) return;
        Invoke("Delay", 0.2f);
	}
    
    void Delay()
    {
        if (ready) return;
        int totalCharacters = Game.Instance.level.charactersManager.getTotalCharacters();

        //puede que se hayan muerto todos antes
        if (totalCharacters == 0)
        {
            Reset();
            return;
        }

        float suma = (percent / totalCharacters);
        Game.Instance.level.SetDificultyByScore( (int)suma );

        panel.SetActive(true);
        //  panel.GetComponent<Animation>().Play("levelTransition");

        StartCoroutine(DoFade());
        foreach (Text field in texts.GetComponentsInChildren<Text>())
            field.text = "Nivel " + level;
        foreach (Text field in texts2.GetComponentsInChildren<Text>())
        {
            switch( Game.Instance.level.Dificulty)
            {
                case Level.Dificult.EASY: field.text = "modo FáCIL"; break;
                case Level.Dificult.MEDIUM: field.text = "dificultad MEDIA"; break;
                case Level.Dificult.HARD: field.text = "modo EXTREMO!"; break;
            }
        }

        level++;
        ready = true;
        Invoke("Reset", 1);
    }
    void Reset()
    {
        percent = 0;
        ready = false;
    }
    void SetOff()
    {
        panel.SetActive(false);
    }

    public IEnumerator DoFade()
    {
        float t = 1;
        while (t > 0)
        {
            yield return new WaitForEndOfFrame();
            t -= Time.deltaTime/1.5f;
            RenderSettings.ambientIntensity = t;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (Text field in texts.GetComponentsInChildren<Text>())
        {
            field.text = "Yeah!";
        }
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime/1.5f;
            RenderSettings.ambientIntensity = t;
        }
        SetOff();
    }
}
