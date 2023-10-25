using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioMixer audioMixer;

	public Slider bgmSlider;
	public Slider seSlider;

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

    private void Start()
    {
        // ���� ���� �� ����� ������ �ҷ����� (�⺻���� 1)
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sevolume = PlayerPrefs.GetFloat("SEVolume", 1f);
        BGMVolume(bgmVolume);
		SEVolume(sevolume);
        bgmSlider.value = bgmVolume;
		seSlider.value = sevolume;
    }

    // BGM ���
    public void PlayBGM(AudioClip clip)
	{
		bgmSource.clip = clip;
		bgmSource.Play();
	}

	// BGM ����
	public void StopBGM()
	{
		bgmSource.Stop();
	}

	// SE ���
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

	// ��� ������ SE ����� �ҽ��� ������
	private AudioSource GetAvailableSESource()
	{
		foreach (AudioSource source in seSources)
		{
			if (!source.isPlaying)
			{
				return source;
			}
		}

		// ��� ������ SE ����� �ҽ��� ������ ���� ����
		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		seSources.Add(newSource);
		return newSource;
	}

	public void BGMVolume(float volume)
	{
		audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();

    }

	public void SEVolume(float volume)
	{
		audioMixer.SetFloat("SE", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SEVolume", volume);
        PlayerPrefs.Save();
    }



}

