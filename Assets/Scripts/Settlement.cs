﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SettlementService;

public enum Faction {
	Pirate,
	Islander,
	Mainlander,
	Empire,
	Player,
	Bootlegger
}

public class FactionFlags {

	public static string GetFlagIconName(Faction faction) {
		switch (faction) {
			default:
			case Faction.Empire:		return "Empire Flag";
			case Faction.Islander:		return "Islander Flag";
			case Faction.Pirate:		return "Pirate Flag";
			case Faction.Mainlander:	return "Mainland Flag";
		}
	}
}

public class Settlement : MonoBehaviour {

	private bool _initialized = false;

	/// <summary>
	/// Name of the settlement
	/// </summary>
	[SerializeField]
	private string _name;

	/// <summary>
	/// The faction the settlement belongs to
	/// </summary>
	[SerializeField]
	private Faction _faction;

	public Faction faction {
		get { return _faction; }
	}

	[SerializeField]
	private Bounds _dockArea;

	/// <summary>
	/// Local coords from which the ship is placed if respawning in settlement
	/// </summary>
	[SerializeField]
	private Vector3 _respawnOffset;

	public Vector3 respawnPos {
		get { return transform.position + _respawnOffset; }
	}

	private Warehouse _warehouse;

	public Warehouse warehouse {
		get { return _warehouse; }
	}

	private Bank _bank;

	public Bank bank {
		get { return _bank; }
	}

	private UI.WorldSpaceFloater _uiIcon;

	// Use this for initialization
	void Awake () {
		_warehouse = GetComponent<Warehouse>();
		_bank = GetComponent<Bank>();
	}
	

	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(transform.position + _dockArea.center, _dockArea.size);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(respawnPos, 0.2f);
	}

	public void Initialize() {
		_initialized = true;

		_uiIcon = UI.UIMain.CreateEmptyFloater(transform);
		UI.UIMain.AddUIIcon(flag, _uiIcon.GetComponent<RectTransform>());

		HideFlag();

		var services = GetComponents<ISettlementService>();

		foreach (var service in services) {
			service.Initialize();
		}
	}

	public void Reset() {
		var services = GetComponents<ISettlementService>();

		for (int i = 0; i < services.Length; i++) {
			services[i].Reset();
		}
	}

	/// <summary>
	/// Returns whether a ship can dock at the settlement at it's present position
	/// </summary>
	/// <param name="ship">The ship to check</param>
	/// <returns>Whether the ship can dock or not</returns>
	public bool CanDock(Ship ship) {
		Vector3 shipPos = ship.transform.position - transform.position;
		return _dockArea.Contains(shipPos);
	}

	/// <summary>
	/// Whether the settlement is in view from a given point
	/// </summary>
	/// <param name="from">The point to be seen from</param>
	/// <returns></returns>
	public bool IsInView(Vector3 from, float viewDistance) {
		Vector3 heading = transform.position - from;

		if (heading.magnitude > viewDistance) {
			return false;
		}

		RaycastHit hit;
		Physics.Raycast(from, heading, out hit, viewDistance);

		if (hit.collider != null && hit.collider.tag == "Terrain") {
			return false;
		}

		return true;
	}

	public void ShowFlag() {
		if (_initialized) {
			_uiIcon.gameObject.SetActive(true);
		}
	}

	public void HideFlag() {
		if (_initialized) {
			_uiIcon.gameObject.SetActive(false);
		}
	}

	public string flag {
		get {
			return FactionFlags.GetFlagIconName(_faction);
		}
	}
}
