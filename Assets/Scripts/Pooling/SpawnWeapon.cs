using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] WeaponPool weaponPool;
    [SerializeField] float spawnTimerDelayInSeconds = 1f;
    float spawnTimer;

    [SerializeField] float removalTimeDelayInSeconds = 5f;
    float removeTimer;

    List<GameObject> weaponInUse = new List<GameObject>();

	private void Update()
	{
		Spwan();
	}

	public void Spwan()
	{
		spawnTimer += Time.deltaTime;
		if (spawnTimer >= spawnTimerDelayInSeconds)
		{
			spawnTimer = 0;
			GameObject obj = weaponPool.SpawnWeapon(transform.position, transform.rotation);
			if (obj)
			{
				weaponInUse.Add(obj);
			}
		}
		foreach (GameObject obj in weaponInUse)
		{
			obj.transform.position += transform.up * 10 * Time.deltaTime;
		}
		removeTimer += Time.deltaTime;
		if (removeTimer >= removalTimeDelayInSeconds)
		{
			removeTimer = 0;
			if (weaponInUse.Count > 0)
			{
				GameObject obj = weaponInUse[0];
				weaponPool.ReturnObject(obj);
				weaponInUse.Remove(obj);
			}
		}
	}
}
