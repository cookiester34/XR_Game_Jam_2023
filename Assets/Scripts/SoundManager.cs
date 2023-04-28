using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioClip mainMusic;

    [SerializeField]
    private AudioClip backgroundNoise;

    [SerializeField]
    private AudioSource mainAudioSource;

    [SerializeField]
    private AudioSource backgroundAudioSource;

    [SerializeField]
    private AudioSource soundEffectsAudioSource;

    private void Start()
    {
        instance ??= this;

        mainAudioSource.clip = mainMusic;
        mainAudioSource.loop = true;
        mainAudioSource.Play();

        backgroundAudioSource.clip = backgroundNoise;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();
    }

    private void triggerSoundEffect(AudioClip soundEffect)
    {
        soundEffectsAudioSource.clip = soundEffect;
        soundEffectsAudioSource.Play();
    }
}