﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player's personal fortune
/// </summary>
public class PlayerAssets {

	/// <summary>
	/// Cash on hand
	/// </summary>
	private int _cash = 200;

	/// <summary>
	/// Cash in each bank in the world
	/// </summary>
	private Dictionary<string, int> _bank = new Dictionary<string, int>();

	/// <summary>
	/// Debts owed to each bank in the world
	/// </summary>
	private Dictionary<string, int> _debts = new Dictionary<string, int>();

	public int cash {
		get { return _cash; }
	}

	/// <summary>
	/// Modifies the cash on hand by a certain amount. The cash on hand cannot
	/// go below 0.
	/// </summary>
	/// <param name="amount"></param>
	public void ModifyCash(int amount) {
		_cash += amount;

		if (_cash < 0) {
			_cash = 0;
		}
	}
}
