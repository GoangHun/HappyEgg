using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioMixer audioMixer;

	
	private AudioSource bgmSource;
	private List<AudioSource> seSources;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		seSources = new List<AudioSource>();
		bgmSource = GetComponent<AudioSource>();
	}

	// BGM 재생
	public void PlayBGM(AudioClip clip)
	{
		bgmSource.clip = clip;
		bgmSource.Play();
	}

	// BGM 멈춤
	public void StopBGM()
	{
		bgmSource.Stop();
	}

	// SE 재생
	public void PlaySE(AudioClip clip)
	{
		AudioSource source = GetAvailableSESource();
		if (source != null)
		{
			source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SE")[0];
			source.clip = clip;
			source.Play();
		}
	}

	// 사용 가능한 SE 오디오 소스를 가져옴
	private AudioSource GetAvailableSESource()
	{
		foreach (AudioSource source in seSources)
		{
			if (!source.isPlaying)
			{
				return source;
			}
		}

		// 사용 가능한 SE 오디오 소스가 없으면 새로 생성
		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		seSources.Add(newSource);
		return newSource;
	}

	public void BGMVolume(float volume)
	{
		audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20f);
	}

	public void SEVolume(float volume)
	{
		audioMixer.SetFloat("SE", Mathf.Log10(volume) * 20f);
	}

}

