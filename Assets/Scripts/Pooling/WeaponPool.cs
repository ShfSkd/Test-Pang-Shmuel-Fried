using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
	List<GameObject> availableWeapon = new List<GameObject>();
	[SerializeField] WeaponSO weaponSO;

	private void Start()
	{
		if (!weaponSO || !weaponSO.Prefab)
		{
			Debug.LogError("ERROR: missing SO or prefab!");
			return;
		}
		for (int i = 0; i < weaponSO.NumberToSpawn; i++)
		{
			GameObject obj = Instantiate(weaponSO.Prefab);
			ReturnObject(obj);
		}
	}


	public GameObject SpawnWeapon(Vector3 position,Quaternion rotation)
	{
		GameObject obj = RequestWeapon();
		if (obj)
		{
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			obj.SetActive(true);
		}
		return obj;
	}

	public GameObject RequestWeapon()
	{
		GameObject obj = null;
		if (availableWeapon.Count > 0)
		{
			obj = availableWeapon[0];
			availableWeapon.Remove(obj);
		}
		return obj;
	}
	public void ReturnObject(GameObject obj)
	{
		if (!availableWeapon.Contains(obj))
		{
			obj.transform.position = Vector3.zero;
			obj.transform.rotation = Quaternion.identity;
			obj.SetActive(false);
			availableWeapon.Add(obj);
		}
	}
}
