using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        // instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // keep this object alive across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // destry duplicate gameobjects
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        
        changeSoundVol(0f);
        changeMusicVol(0f);
    }


    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void changeSoundVol(float change)
    {
        ChangeSourceVolume("SoundVolume", change, soundSource);
    }

    public void changeMusicVol(float change)
    {
        ChangeSourceVolume("MusicVolume", change, musicSource);
    }

    private void ChangeSourceVolume(string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        source.volume = currentVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
