using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource roosterSound;
    [SerializeField] private AudioSource walkingSound;

    private AudioSource _currentPlayingSound;
    
    public enum Sound
    {
        Walking,
        BackgroundMusic,
        RoosterSound
    }

    private Dictionary<Sound, AudioSource> _audioSources;

    private void Awake()
    {
        _audioSources = new Dictionary<Sound, AudioSource>();
        
        _audioSources.Add(Sound.Walking, walkingSound);
        _audioSources.Add(Sound.BackgroundMusic, backgroundMusic);
        _audioSources.Add(Sound.RoosterSound, roosterSound);

        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    public void PlaySound(Sound sound)
    {
        _currentPlayingSound = _audioSources[sound];
        _currentPlayingSound.Play();
    }

    public void StopSound(Sound sound)
    {
        _audioSources[sound].Stop();
    }

    public void StopSound()
    {
        _currentPlayingSound.Stop();
    }
}
