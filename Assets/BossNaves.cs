﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNaves : Boss {

	public GameObject[] all;
	bool started;

	public override void OnSceneObjectUpdated()
	{
		float avatarsDistance = Game.Instance.level.charactersManager.getDistance ();
		if (avatarsDistance + distance_from_avatars < transform.localPosition.z)
			return;
		if (!started) {
			SetTotal( all.Length );
			partID = 0;
			Init ();
			started = true;
		}
		float _z = avatarsDistance + distance_from_avatars;

		Vector3 pos = transform.localPosition;
		pos.z = _z;
		transform.localPosition = pos;
	} 
	int partID;
	void Init()	
	{
		if (partID >= all.Length)
			return;
		all [partID].gameObject.SetActive (true);
		Invoke ("Init", 0.5f);
		partID++;
	}
	public override void OnPartBroken(BossPart part)
	{
		print ("___ breakOut()");
		breakOut ();
		//part.asset.GetComponent<Breakable>().breakOut (part.transform.position);
	}

}
