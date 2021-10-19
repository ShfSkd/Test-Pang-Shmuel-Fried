using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0,1f)] float shootingVolume = 1f;
	[Header("Damage Sounds")]
	[SerializeField] AudioClip damageClip;
    [SerializeField][Range(0,1f)] float damageVolume = 1f;
	[Header("Dead Sound")]
	[SerializeField] AudioClip deadClip;
	[SerializeField] [Range(0, 1f)] float deadVolume = 1f;

	static AudioPlayer instance;
	private void Awake()
	{
		ManageSingleton();
	}

	private void ManageSingleton()
	{
		/*int instanceCount= FindObjectsOfType(GetType()).Length;
		if (instanceCount > 1)
		{*/
		if (instance != null) 
		{ 
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void PlayShootingClip()
	{
		PlayClip(shootingClip, shootingVolume);
	}
	public void PlayDamageClip()
	{
		PlayClip(damageClip, damageVolume);
	}
	public void PlayDeadClip()
	{
		PlayClip(deadClip, deadVolume);
	}
	void PlayClip(AudioClip clip,float volume)
	{
		if (clip != null)
		{
			Vector3 cameraPos = Camera.main.transform.position;
			AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
		}
	}
}
