using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	[SerializeField] AudioSource _music;
	[SerializeField] AudioSource _sfx;

	[Range(0, 1)] public float masterVolume = 1f;
	[Range(0, 1)] public float musicVolume = 1f;
	[Range(0, 1)] public float sfxVolume = 1f;

	float MusicVolume { get { return musicVolume * masterVolume; } }
	float SFXVolume { get { return sfxVolume * masterVolume; } }

	private void Update()
	{
		SetVolumes();
	}

	public void SetVolumes()
	{
		_sfx.volume = SFXVolume;
		_music.volume = MusicVolume;
	}

	public void SetMasterVolume(float volume)
	{
		masterVolume += volume;
		SetVolumes();
	}

	public void SetMusicVolume(float volume)
	{
		musicVolume += volume;
		SetVolumes();
	}

	public void SetSFXVolume(float volume)
	{
		sfxVolume += volume;
		SetVolumes();
	}

	public void PlaySFX(AudioClip audioClip)
	{
		//_sfx.clip = audioClip;
		_sfx.PlayOneShot(audioClip);
	}

}
