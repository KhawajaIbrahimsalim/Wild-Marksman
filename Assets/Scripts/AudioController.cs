using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Game Audio Source:")]
    public AudioSource ShootAudio;
    public AudioSource DestroyAudio;
    public AudioSource SifiAudio;
    public AudioSource LaserShootAudio;
    public AudioSource UI_Button_Audio;
    public AudioSource ElectroLaserAudio;

    private static AudioController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudio(AudioSource Audio_Source)
    {
        // Play the Audio Source
        Audio_Source.Play();
    }
}
