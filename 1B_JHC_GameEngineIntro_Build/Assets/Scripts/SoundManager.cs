using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] clip;

    public int currentClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayCurrentAudio();
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }
    public void PlayCurrentAudio()
    {
        audioSource.PlayOneShot(clip[currentClip]);
        audioSource.loop = true;
    }
}
