﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [HideInInspector]
	public int ammo;
	[SerializeField]
	float cycleTime = 0.1f;
	[SerializeField]
	bool automatic = false;
	[SerializeField]
	int magazineCapacity = 30;
	[SerializeField]
	bool infiniteAmmo = false;
	IWeaponAction[] actions;
	public float recoilMagnitude = 1;
	Disconnector sear = new Disconnector();
	TimedDisconnector autoSear = new TimedDisconnector();



	void Start()
	{
		// set up auto sear
		autoSear.releaseTime = cycleTime;
		// load magazine
		//ammo = magazineCapacity;
		// get child actions
		actions = GetComponentsInChildren<IWeaponAction>();
	}



	public bool PullTrigger(float trigger)
	{
		if(ammo == 0 && !infiniteAmmo)
		{
			return false;
		}

		// sear checks
		var isCycleDone = autoSear.CanTrip();
		var isTriggerUp = sear.Trip(trigger);

		// cycle rate check
		if(isCycleDone)
		{
			if(automatic && trigger > 0)
			{
				// auto
				TriggerActions();
				autoSear.Trip();
				ammo = Mathf.Clamp(ammo - 1, 0, magazineCapacity);
				return true;
			}
			else if(isTriggerUp)
			{
				// semi
				TriggerActions();
				autoSear.Trip();
				ammo = Mathf.Clamp(ammo - 1, 0, magazineCapacity);
				return true;
			}
		}

		// cannot fire
		return false;
	}



	public bool CanReload()
	{
		return ammo < magazineCapacity;
	}



	public bool IsEmpty()
	{
		return ammo == 0;
	}



	public void Reload(int amt)
	{
		ammo = Mathf.Clamp(ammo + amt, 0, magazineCapacity);
	}



	void TriggerActions()
	{
		foreach(var a in actions)
		{
			a.Fire();
		}
	}



	public string AmmoDisplay()
	{
		return ammo.ToString();
	}

	

	public bool IsEquipped()
	{
		return gameObject.activeInHierarchy;
	}



	public void Equip(bool isEquipped)
	{
		gameObject.SetActive(isEquipped);
	}
}
