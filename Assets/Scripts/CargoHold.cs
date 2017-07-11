﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The contents of the ship's cargo hold
/// </summary>
public class CargoHold {

	/// <summary>
	/// The total hold size
	/// </summary>
	private int _capacity = 700;	

	/// <summary>
	/// The total hold size
	/// </summary>
	/// <returns>The total hold size</returns>
	public int capacity {
		get { return _capacity; }
	}

	/// <summary>
	/// The amount of cargo in the hold
	/// </summary>
	private int _currentHold = 0;

	/// <summary>
	/// The amount of cargo in the hold
	/// </summary>
	public int currentCargo {
		get { return _currentHold; }
	}

	/// <summary>
	/// The amount of cargo space remaining
	/// </summary>
	/// <returns>The amount of cargo space remaining</returns>
	public int availableSpace {
		get { return _capacity - _currentHold; }
	}

	private Dictionary<string, int> _hold = new Dictionary<string, int>();

	public Dictionary<string, int> manifest {
		get { return _hold; }
	}

	public CargoHold() {
		Add("grain", 10);
		Add("sugar", 15);
	}

	public bool Add(string id, int amount) {
		if (_currentHold + amount > _capacity) {
			return false;
		}

		if (_hold.ContainsKey(id)) {
			_hold[id] += amount;
		} else {
			_hold.Add(id, amount);
		}

		RecalculateCargoHold();
		return true;
	}

	public void Remove(string id, int amount) {
		if (_hold.ContainsKey(id)) {
			_hold[id] -= amount;

			if (_hold[id] < 0) {
				_hold[id] = 0;
			}
		}
	}

	/// <summary>
	/// Returns the count of a particular cargo in the hold
	/// </summary>
	/// <param name="id">The cargo to count</param>
	public int GetItemCount(string id) {
		if (_hold.ContainsKey(id)) {
			return _hold[id];
		} else {
			return 0;
		}
	}

	private void RecalculateCargoHold() {
		_currentHold = 0;

		foreach (var item in _hold) {
			_currentHold += item.Value;
		}
	}
}
