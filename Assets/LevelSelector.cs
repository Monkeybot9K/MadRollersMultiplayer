﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour {

    public int levelUnlockedID;
    public MissionButton uiButton;
	public MissionIcon missionIcon;

    [SerializeField]
    GameObject container;

	private Data data;
	public int missionActiveID;
	private Missions missions;
	private int missionID;
    private float StartScrollPosition;
    private MissionButton lastButton;
    public bool showJoystickMenu;
	int separation = 50;
	public GameObject cam;
	float lastClickedTime;
	List<MissionButton> all;

	void Start()
	{
		Init ();
		Data.Instance.events.OnJoystickBack += OnJoystickBack;
		Data.Instance.events.OnJoystickClick += OnJoystickClick;
		Data.Instance.events.OnJoystickDown += OnJoystickDown;
		Data.Instance.events.OnJoystickUp += OnJoystickUp;
		Data.Instance.events.OnJoystickLeft += OnJoystickDown;
		Data.Instance.events.OnJoystickRight += OnJoystickUp;
	}
	void OnDestroy()
	{
		Data.Instance.events.OnJoystickBack -= OnJoystickBack;
		Data.Instance.events.OnJoystickClick -= OnJoystickClick;
		Data.Instance.events.OnJoystickDown -= OnJoystickDown;
		Data.Instance.events.OnJoystickUp -= OnJoystickUp;
		Data.Instance.events.OnJoystickLeft -= OnJoystickDown;
		Data.Instance.events.OnJoystickRight -= OnJoystickUp;
	}
	void Update()
	{
		cam.transform.localPosition = Vector3.Lerp (cam.transform.localPosition, new Vector3(0, 0, missionActiveID * separation), 0.1f);
	}
	void OnJoystickClick()
	{
		if (all [missionActiveID].isLocked)
			return;
		data.missions.MissionActiveID = missionActiveID;
		Data.Instance.LoadLevel("MainMenuStory");
	}
	void OnJoystickUp()
	{
		if(missionActiveID<missions.missions.Length-1)
			missionActiveID++;
		SetSelected ();
	}
	void OnJoystickDown()
	{
		if(missionActiveID>0)
			missionActiveID--;
		SetSelected ();
	}
	void Init () {

		Data.Instance.events.VoiceFromResources("juega_solo_asi_te_quedaras");
		Data.Instance.events.OnInterfacesStart();

		data = Data.Instance;		
		missions = data.missions;

		missionActiveID = data.levelUnlockedID;
		// GameObject container = GameObject.Find("Container") as GameObject;

		missionID = 0;

		//si jugas con joystick
		//if (!showJoystickMenu)
		//    GameObject.Find("ContainerJoystick").gameObject.SetActive(false);

		all = new List<MissionButton> ();
		foreach (Mission mission in missions.missions) {
			MissionButton button = Instantiate (uiButton) as MissionButton;
			button.Init(missionID, mission.description.ToUpper());


			lastButton = button;

			if (missionID > data.levelUnlockedID && !Data.Instance.DEBUG)
				button.disableButton();

			missionID++;

			all.Add (button);
		}
		all.Reverse ();
		foreach (MissionButton mission in all) {

			mission.transform.SetParent(container.transform) ;
			mission.transform.localScale = new Vector3(1,1,1);
			mission.transform.localPosition = new Vector3 (0, 0, mission.id * separation);           

		}
		levelUnlockedID = data.levelUnlockedID;   
		all.Reverse ();
		SetSelected ();
	}
	MissionButton lastButtonSelected;
	void SetSelected()
	{
		if(lastButtonSelected!=null)
			lastButtonSelected.SetOn (false);
		lastButtonSelected = all [missionActiveID];
		lastButtonSelected.SetOn (true);

		missionIcon.SetOn (missions.missions [missionActiveID]);
	}
	public void OnJoystickBack()
    {
        Data.Instance.LoadLevel("MainMenu");
    }

}
