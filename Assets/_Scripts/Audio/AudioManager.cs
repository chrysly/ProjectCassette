using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource mainMusicSource, sfxSource, secondaryMusicSource;
    [SerializeField] private Clip[] musicClips, sfxClips;

    private float musicVolume = 1;
    public float MusicVolume { get => musicVolume; set { musicVolume = value; } }

    private float sfxVolume = 1;
    public float SFXVolume { get => sfxVolume; set { sfxVolume = value; } }

    private Coroutine activeInterpolation;

    private static AudioManager instance;
    public static AudioManager Instance => instance;

    void Awake() {
        secondaryMusicSource = Instantiate(mainMusicSource.gameObject, transform).GetComponent<AudioSource>();
        secondaryMusicSource.gameObject.name = "SecondaryMusicSource";
        if (instance != this) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(gameObject);
    }

    public void PlayMusic(string name) {
        Clip clip = FindClip(musicClips, name);
        mainMusicSource.clip = clip.clip;
        StopAllCoroutines();
        mainMusicSource.Play();
    }

    public void InterpolateMusicTracks(string name) {
		Clip clip = FindClip(musicClips, name);
		if (activeInterpolation != null) StopCoroutine(activeInterpolation);
		activeInterpolation = StartCoroutine(_InterpolateMusicTracks(clip.clip));
	}

	IEnumerator _InterpolateMusicTracks(AudioClip newTrack) {
		float lerp = 1;
		SetUpSecondaryMusicClip(newTrack);
		while (lerp > 0) {
			mainMusicSource.volume = musicVolume * lerp;
			secondaryMusicSource.volume = musicVolume * (1 - lerp);
			lerp = Mathf.MoveTowards(lerp, 0, Time.unscaledDeltaTime);
			yield return null;
		} SwapPrimaryMusicSource();
    }

	private void SetUpSecondaryMusicClip(AudioClip newTrack) {
		secondaryMusicSource.clip = newTrack;
		secondaryMusicSource.time = mainMusicSource.time;
		secondaryMusicSource.Play();
	}

	private void SwapPrimaryMusicSource() {
		AudioSource oldSource = mainMusicSource;
		mainMusicSource = secondaryMusicSource;
		secondaryMusicSource = oldSource;
		secondaryMusicSource.Stop();
    }

    public void PlaySFX(string name) {
        Clip clip = FindClip(sfxClips, name);
        sfxSource.PlayOneShot(clip.clip);
    }

    private Clip FindClip(Clip[] clipArr, string name) {
        return clipArr.First(clip => clip.name == name);
    }
}

[System.Serializable]
public struct Clip {
    public string name;
    public AudioClip clip;
}
