using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Configuração de Áudio")]
    public AudioMixerGroup mixerGroup;

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
        public float volume = 1;
        public float pitch = 1;
        [HideInInspector] public AudioSource source;
        public bool isMusic;
    }

    public Sound[] sounds;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som não encontrado: " + name);
            return;
        }

        // Se for música, para outras antes
        if (s.isMusic)
        {
            foreach (var snd in sounds)
            {
                if (snd.isMusic && snd.source.isPlaying)
                    snd.source.Stop();
            }
        }

        s.source.Play();
    }

    public void StopMusic()
    {
        foreach (var snd in sounds)
        {
            if (snd.isMusic)
                snd.source.Stop();
        }
    }
}
