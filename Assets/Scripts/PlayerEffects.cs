using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour 
{
	[SerializeField] AudioSource source;
	[SerializeField] AudioClip blowtorch;
	[SerializeField] ParticleSystem particle;
	
	public void PlayBlowtorch()
	{
		source.clip = blowtorch;
		source.Play();
		particle.Play();
	}

	public void Stop()
	{
		source.Stop();
		particle.Stop();
	}

}
