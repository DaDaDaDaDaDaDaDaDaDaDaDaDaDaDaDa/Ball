using Assets.Scripts.MISC;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Sound
{
    public Sounds Name;
    public bool IsLooping;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;

    private AudioSource source;

    public void SetSource(AudioSource s)
    {
        source = s;
        source.clip = Clip;
    }

    public void Play(float masterVolume)
    {
        source.volume = Volume * masterVolume;
        source.loop = IsLooping;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    public Sound[] Sounds;
    [Range(0f, 1f)]
    public float MasterVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            var obj = new GameObject($"Sound_{i}_{Sounds[i].Name}");
            Sounds[i].SetSource(obj.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(Sounds name)
    {
        Sounds.FirstOrDefault(s => s.Name == name).Play(MasterVolume);
    }

    public void StopSound(Sounds name)
    {
        Sounds.FirstOrDefault(s => s.Name == name).Stop();
    }
}
