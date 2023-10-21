using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource mainMusicSource, sfxSource, secondaryMusicSource;
    [SerializeField] private Clip[] musicClips, sfxClips;

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

    
}

[System.Serializable]
public struct Clip {
    public string name;
    public AudioClip clip;
}
