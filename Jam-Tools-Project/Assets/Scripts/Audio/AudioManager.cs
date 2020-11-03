using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { private set; get; }
    #endregion

    [Header("Collections of audio")]
    public AudioData[] musicCollection;
    public AudioData[] sfxCollection;

    private AudioData currentMusic;

    #region PROPERTIES
    public bool IsMusicMuted { get { return MusicVolume == 0; } }
    public bool IsSfxMuted { get { return SfxVolume == 0; } }

    public float MusicVolume
    {
        get => _musicVolume;
        set { _musicVolume = Mathf.Clamp01(value); }
    }
    float _musicVolume = 1f;

    public float SfxVolume
    {
        get => _sfxVolume;
        set { _sfxVolume = Mathf.Clamp01(value); }
    }
    float _sfxVolume = 1f;
    #endregion

    void Awake()
    {
        CreateInstance();

        SetUpAudioArray(musicCollection);
        SetUpAudioArray(sfxCollection);
    }

    void Start()
    {
        PlayMusic("Music");
    }

    private void CreateInstance()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

    private void SetUpAudioArray(AudioData[] _array)
    {
        for (int i = 0; i < _array.Length; i++)
        {
            GameObject child = new GameObject(_array[i].Name);
            child.transform.SetParent(transform);

            AudioSource audioSource = child.AddComponent<AudioSource>();

            _array[i].go = child;
            _array[i].audioSource = audioSource;

            _array[i].audioSource.clip = _array[i].audioClip;
            _array[i].audioSource.volume = _array[i].volume;
            _array[i].audioSource.pitch = _array[i].pitch;
            _array[i].audioSource.loop = _array[i].isLooping;
        }
    }

    #region Play Music
    public void PlayMusic(string _name)
    {
        Debug.Log("Playing Music");
        AudioData data = Array.Find(musicCollection, bgm => bgm.Name == _name);
        if (data == null)
        {
            Debug.LogError("There is no music with name " + _name);
            return;
        }
        else
        {
            currentMusic = data;
            currentMusic.audioSource.Play();
        }
    }

    public void PlayMusic(int index)
    {
        if (index < 0 || index > musicCollection.Length - 1)
        {
            Debug.LogError("There is no music with index " + index);
            return;
        }

        AudioData data = musicCollection[index];
        data.audioSource.Play();
    }
    #endregion

    #region Play SFX
    public void PlaySFX(string _name)
    {
        AudioData data = Array.Find(sfxCollection, sfx => sfx.Name == _name);
        if (data == null)
        {
            Debug.LogError("There is no sfx with name " + _name);
            return;
        }
        else
        {
            data.audioSource.Play();
        }
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index > sfxCollection.Length - 1)
        {
            Debug.LogError("There is no sfx with index " + index);
            return;
        }

        AudioData data = sfxCollection[index];
        data.audioSource.Play();
    }
    #endregion

    #region Volume
    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;

        for (int i = 0; i < musicCollection.Length; i++)
            musicCollection[i].audioSource.volume = MusicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        SfxVolume = volume;

        for (int i = 0; i < sfxCollection.Length; i++)
            sfxCollection[i].audioSource.volume = volume;
    }
    #endregion
}