﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LandingForArcade : MonoBehaviour {

    public MultiplayerCompetitionButton button;
    public Transform container;
    public Toggle switchPlayers;
    public Toggle musicOn;

    void Start () {
        Invoke("Next", 1);
	}
    void Next()
    {
        print("Data.Instance.isArcadeMultiplayer " + Data.Instance.isArcadeMultiplayer);
        Data.Instance.playMode = Data.PlayModes.COMPETITION;
        if (Data.Instance.isArcadeMultiplayer)
            LoopUntilCompetitionsReady(); // Data.Instance.LoadLevel("MainMenuArcade");
        else
            Data.Instance.LoadLevel("GameForArcade");
    }
    void LoopUntilCompetitionsReady()
    {
        print(Data.Instance.GetComponent<MultiplayerCompetitionManager>().competitions.Count);
        if (Data.Instance.GetComponent<MultiplayerCompetitionManager>().competitions.Count==0)
            Invoke("LoopUntilCompetitionsReady", 1);
        else
        {
            LoadCompetitions();
        }
    }
    void LoadCompetitions()
    {
        Cursor.visible = true;
        print("LoadCompetitions");
        foreach (string title in Data.Instance.GetComponent<MultiplayerCompetitionManager>().competitions)
        {
            MultiplayerCompetitionButton newButton = Instantiate(button);
            newButton.Init(this, title);
            newButton.transform.SetParent(container);
        }
    }
    public void Selected(string competitionTitle)
    {
        if (!musicOn.isOn)
            Data.Instance.GetComponent<MusicManager>().TurnOff();

        Data.Instance.switchPlayerInputs = switchPlayers.isOn;
        Cursor.visible = false;
        Data.Instance.GetComponent<MultiplayerCompetitionManager>().actualCompetition = competitionTitle;
        Data.Instance.GetComponent<PhotosManager>().LoadPhotos();
        Data.Instance.LoadLevel("MainMenuArcade");
    }
    
}
