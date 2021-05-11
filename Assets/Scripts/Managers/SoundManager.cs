using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource OST1;
    [SerializeField] private AudioSource OST2;
    [SerializeField] private AudioSource OST3;
    [SerializeField] private AudioSource roosterSound;
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioSource debrisSound;

    private AudioSource _currentPlayingSound;
    
    public enum Sound
    {
        OST1,
        OST2,
        OST3,
        Walking,
        Rooster,
        Debris
    }

    private Dictionary<Sound, AudioSource> _audioSources;

    private void Awake()
    {
        _audioSources = new Dictionary<Sound, AudioSource>();
        
        _audioSources.Add(Sound.OST1, OST1);
        _audioSources.Add(Sound.OST2, OST2);
        _audioSources.Add(Sound.OST3, OST3);
        _audioSources.Add(Sound.Walking, walkingSound);
        _audioSources.Add(Sound.Rooster, roosterSound);
        _audioSources.Add(Sound.Debris, debrisSound);

        OST1.loop = true;
        OST2.loop = true;
        OST3.loop = true;
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
