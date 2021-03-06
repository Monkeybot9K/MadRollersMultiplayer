﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarMultiplayer : MonoBehaviour {

	public GameObject panel;
	public GameObject hiscoreFields;
	public Text myScoreFields;
	public Image bar;
	public RawImage hiscoreImage;
	public int score;
	public int hiscore;
    float newVictoryAreaScore;

    bool hiscoreWinned;
    

    void Start () {
//		if (Data.Instance.playMode == Data.PlayModes.STORY) {
//			panel.SetActive (false);
//			return;
//		} else {
//			panel.SetActive (true);
//		}
	//	hiscoreWinned = false;
      //  newVictoryAreaScore = Data.Instance.multiplayerData.newVictoryAreaScore;

        Data.Instance.events.OnScoreOn += OnScoreOn;

		score = 0;
//		bar.fillAmount = 0;
//
//		ArcadeRanking arcadeRanking = Data.Instance.GetComponent<ArcadeRanking> ();
//		if (arcadeRanking.all.Count > 0) {
//			hiscore = arcadeRanking.all [0].score;
//			foreach (Text textfield in hiscoreFields.GetComponentsInChildren<Text>())
//				textfield.text = hiscore.ToString ();
//
//			hiscoreImage.material.mainTexture = Data.Instance.GetComponent<ArcadeRanking>().all[0].texture;
//		}
		UpdateScore ();
	}
	void OnDestroy()
	{
		Data.Instance.events.OnScoreOn -= OnScoreOn;
	}
	void OnScoreOn(int playerID, Vector3 pos, int total)
	{
		score += total;
		UpdateScore ();
        if(score>newVictoryAreaScore)
        {
			print ("__newVictoryAreaScore " + newVictoryAreaScore + "  newVictoryAreaScore" + Data.Instance.multiplayerData.newVictoryAreaScore);
            Data.Instance.events.SetVictoryArea();
			Data.Instance.multiplayerData.newVictoryAreaScore *= 1.5f;
            this.newVictoryAreaScore += Data.Instance.multiplayerData.newVictoryAreaScore;
        }
	}
	void UpdateScore()
	{	
		myScoreFields.text = score.ToString ("00000");

		return;

		if(hiscoreWinned) return;
		
		float barValue = (float)score/(float)hiscore;
		if (barValue >= 1) {
			hiscoreWinned = true;
			barValue = 1;
			Data.Instance.events.ShowNotification ("NUEVO HISCORE!");
		}
		bar.fillAmount = barValue;
	}
}
