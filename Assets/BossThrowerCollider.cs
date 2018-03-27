﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowerCollider : MonoBehaviour {

	BossThrower boss;

	void Start()
	{
		boss = GetComponentInParent<BossThrower> ();
	}
	void OnTriggerEnter(Collider other)
	{
		print (other);
		BossPart part = other.gameObject.GetComponent<BossPart> ();
		if (part != null) {
			boss.AddEnemy (transform.position);
		}
	}
}
