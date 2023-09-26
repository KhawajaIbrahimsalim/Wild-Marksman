using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Game Audio Source:")]
    public AudioSource ShootAudio;
    public AudioSource DestroyAudio;
    public AudioSource SifiAudio;
    public AudioSource LaserShootAudio;

    public void PlayAudio(AudioSource Audio_Source)
    {
        // Play the Audio Source
        Audio_Source.Play();
    }
}
