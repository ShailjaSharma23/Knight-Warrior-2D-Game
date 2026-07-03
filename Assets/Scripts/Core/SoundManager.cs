using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        // instance = this;
        source = GetComponent<AudioSource>();

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
        
    }


    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
