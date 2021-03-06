﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{

	[HideInInspector]
	public AudioSource source;



	void Awake()
	{
		source = GetComponent<AudioSource>();
	}



	void Update()
	{
		source.pitch = Time.timeScale;
	}



	public void PlayOneShot(AudioClip clip)
	{
		source.PlayOneShot(clip);
	}



	public void RandomizePitch(float radius)
	{
		source.pitch = 1 + Random.Range(-radius, radius);
	}



	public void RandomizeVolume(float radius)
	{
		source.volume = 1 + Random.Range(-radius, radius);
	}
}
