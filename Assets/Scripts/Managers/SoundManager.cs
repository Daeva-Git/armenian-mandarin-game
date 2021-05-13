using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource OST1;
    [SerializeField] private AudioSource OST2;
    [SerializeField] private AudioSource OST3;
    [SerializeField] private AudioSource OST4;
    [SerializeField] private AudioSource OST5;
    [SerializeField] private AudioSource OST6;
    
    [SerializeField] private AudioSource roosterSound;
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioSource debrisSound;
    [SerializeField] private AudioSource rocksSound;
    [SerializeField] private AudioSource ratsSound;

    private Sound _currentPlayingSound;
    private OST _currentPlayingOST;
        
    public enum Sound
    {
        Default,
        Walking,
        Rooster,
        Debris,
        Rocks,
        Rats
    }
    public enum OST
    {
        Default,
        OST1,
        OST2,
        OST3,
        OST4,
        OST5,
        OST6
    }

    private Dictionary<Sound, AudioSource> _soundList;
    private Dictionary<OST, AudioSource> _ostList;

    private void Awake()
    {
        _soundList = new Dictionary<Sound, AudioSource>();
        _ostList = new Dictionary<OST, AudioSource>();
        
        _ostList.Add(OST.OST1, OST1);
        _ostList.Add(OST.OST2, OST2);
        _ostList.Add(OST.OST3, OST3);
        _ostList.Add(OST.OST4, OST4);
        _ostList.Add(OST.OST5, OST5);
        _ostList.Add(OST.OST6, OST6);
        
        _soundList.Add(Sound.Walking, walkingSound);
        _soundList.Add(Sound.Rooster, roosterSound);
        _soundList.Add(Sound.Debris, debrisSound);
        _soundList.Add(Sound.Rocks, rocksSound);
        _soundList.Add(Sound.Rats, ratsSound);

        Play(OST.OST1);
    }

    public void Play(Sound sound)
    {
        if (sound == Sound.Default) return;
        if (sound == _currentPlayingSound) return;
        Stop(_currentPlayingSound);
        _currentPlayingSound = sound;
        _soundList[_currentPlayingSound] = _soundList[sound];
        _soundList[_currentPlayingSound].Play();
    }
    
    public void Play(OST ost)
    {
        if (ost == OST.Default) return;
        if (ost == _currentPlayingOST) return;
        Stop(_currentPlayingOST);
        _currentPlayingOST = ost;
        _ostList[_currentPlayingOST] = _ostList[ost];
        _ostList[_currentPlayingOST].Play();
        _ostList[_currentPlayingOST].loop = true;
    }

    public void Stop(Sound sound)
    {
        if (sound == Sound.Default) return;
        _soundList[sound].Stop();
    }

    public void Stop(OST ost)
    {
        if (ost == OST.Default) return;
        _ostList[ost].Stop();
    }
}
