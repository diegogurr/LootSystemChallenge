using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public float minPitch = 0.8f;
        public float maxPitch = 1.2f;
        public float minVolume = 0.8f;
        public float maxVolume = 1.2f;
    }

    [SerializeField]
    private List<Sound> sounds;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        Sound sound = sounds.Find(s => s.name == soundName);
        if (sound != null)
        {
            audioSource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
            audioSource.volume = Random.Range(sound.minVolume, sound.maxVolume);
            audioSource.PlayOneShot(sound.clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
}
