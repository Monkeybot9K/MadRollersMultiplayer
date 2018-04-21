﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VoicesManager : MonoBehaviour
{
	public List<VoiceData> tutorials;
	public List<VoiceData> intros;
	public List<VoiceData> welcome;
	public List<VoiceData> missionComplete;

	public List<VoiceData> lose_bad;
	public List<VoiceData> lose_good;
	public List<VoiceData> lose_great;

	public List<VoiceData> mission_1_2;
	public List<VoiceData> mission_1_3;
	public List<VoiceData> mission_1_4;
	public List<VoiceData> mission_1_5;
	public List<VoiceData> mission_1_6;
	public List<VoiceData> mission_1_7;
	public List<VoiceData> mission_1_8;
	public List<VoiceData> mission_1_9;
	public List<VoiceData> mission_1_10;

	public List<VoiceData> mission_2_1;
	public List<VoiceData> mission_2_2;

	public VoiceData selectMadRollers;

	public AudioSpectrum audioSpectrum;
	[Serializable]
	public class VoiceData
	{
		public string text;
		public AudioClip audioClip;
	}
	public AudioSource audioSource;

    public void Init()
    {
		Data.Instance.events.OnGameStart += OnGameStart;
        Data.Instance.events.OnMissionComplete += OnMissionComplete;
        Data.Instance.events.OnListenerDispatcher += OnListenerDispatcher;
        Data.Instance.events.OnAvatarCrash += OnAvatarCrash;
        Data.Instance.events.OnAvatarFall += OnAvatarFall;
        Data.Instance.events.OnAvatarChangeFX += OnAvatarChangeFX;
        Data.Instance.events.SetVolume += SetVolume;
        Data.Instance.events.VoiceFromResources += VoiceFromResources;        
    }
	void OnGameStart()
	{
		
	}
    void SetVolume(float vol)
    {
        audioSource.volume = vol;
    }
    private void OnMissionComplete(int id)
    {
    }
    private void OnAvatarCrash(CharacterBehavior cb)
    {
		Dead ();
    }
    private void OnAvatarFall(CharacterBehavior cb)
    {
		Dead ();
    }
	void Dead()
	{
		float distance = Game.Instance.level.charactersManager.distance;
		print ("die in distance; " + distance);
		if (distance < 100)
			PlayRandom (lose_bad);
		else if (distance < 1000)
			PlayRandom (lose_good);
		else
			PlayRandom (lose_great);
	}
    private void OnAvatarChangeFX(Player.fxStates state)
    {
    }
    private void OnListenerDispatcher(string message)
    {
		print ("_______OnListenerDispatcher " + message + " "  + Data.Instance.missions.MissionActiveID);
		if (Data.Instance.playMode == Data.PlayModes.COMPETITION || Data.Instance.playMode == Data.PlayModes.GHOSTMODE) return;
        if (message == "ShowMissionId")
        {
			
        }
		else if (message == "ShowMissionName")
		{
			switch (Data.Instance.missions.MissionActiveID) {
			case 1:
				PlaySequence (mission_1_2);
				break;
			case 2:
				PlaySequence (mission_1_3);
				break;
			case 3:
				PlaySequence (mission_1_4);
				break;
			case 4:
				PlaySequence (mission_1_5);
				break;
			case 5:
				PlaySequence (mission_1_6);
				break;
			case 6:
				PlaySequence (mission_1_7);
				break;
			case 7:
				PlaySequence (mission_1_8);
				break;
			case 8:
				PlaySequence (mission_1_9);
				break;
			case 9:
				PlaySequence (mission_1_10);
				break;
			case 10:
				PlaySequence (mission_1_10);
				break;
			case 11:
				PlaySequence (mission_2_1);
				break;
			case 12:
				PlaySequence (mission_2_2);
				break;
			}
		}
    }
	int sequenceID = 0;
	bool onSequence = false;
	List<VoiceData> sequenceSaying;
	public void PlaySequence( List<VoiceData> clips)
	{
		if (clips.Count == 0)
			return;
		sequenceID = 0;
		talking = false;
		audioSource.Stop ();
		this.sequenceSaying = clips;
		onSequence = true;
		PlayNextSequencedClip ();
	}
	void PlayNextSequencedClip()
	{
		VoiceData newAudio = sequenceSaying[sequenceID];
		print (onSequence + " " + newAudio.audioClip + " " + sequenceID + "    count: " + sequenceSaying.Count);
		PlayClip(newAudio.audioClip); 
		sequenceID++;
		if (sequenceSaying.Count == sequenceID)
		{
			print ("SALE");
			onSequence = false;
			Done ();
		}
	}
	public void PlayRandom( List<VoiceData> clips)
    {
		int rand = UnityEngine.Random.Range(0, clips.Count);
		PlayClip(clips[rand].audioClip); 
    }
    public void ComiendoCorazones()
    {
    }
    public void VoiceSecondaryFromResources(string name)
    {
    }
    public void VoiceFromResources(string name)
    {
    }
	bool talking;
	public void PlayClip(AudioClip audioClip)
    {
		talking = true;
		audioSpectrum.SetOn ();
        audioSource.clip = audioClip;
        audioSource.Play();
		Data.Instance.events.OnTalk (true);
    }
	float timer;
	void Update()
	{
		if (!talking)
			return;
		
		if (audioSource.clip != null && audioSource.clip.length>0.1f && audioSource.time >= (audioSource.clip.length-0.02f)) {
			Done ();
		}
	}
	void Done()
	{
		if (onSequence)
			PlayNextSequencedClip ();
		else {
			talking = false;			
			Data.Instance.events.OnTalk (false);
		}
	}
}
