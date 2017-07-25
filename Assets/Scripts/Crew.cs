﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew : MonoBehaviour {

	[SerializeField]
	private int _baseSpottingSkill = 10;

	public int baseSpottingSkill {
		get { return _baseSpottingSkill; }
	}

	[SerializeField]
	private int _spottingSkill = 20;

	public int spottingSkill {
		get { return _spottingSkill; }
	}

	public float GetSpotDistance() {
		float lightLevel = 0;

		if (GameState.time != null) {
			lightLevel = GameState.time.lightLevel;
		}

		return baseSpottingSkill + spottingSkill * lightLevel;
	}
}
