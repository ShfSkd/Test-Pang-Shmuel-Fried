using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pool Weapon", menuName = "ScriptableObject/WeaponPool", order = 1)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] private int numberToSpawn = 5;

    public GameObject Prefab { get { return weaponPrefab; } }
    public int NumberToSpawn { get { return numberToSpawn; } }


}
