using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;   // ±≥æ∞“Ù¿÷
    public AudioSource sfxSource;   // ∞¥≈•“Ù–ß

    public AudioClip bgmClip;
    public AudioClip clickClip;

    void Awake()
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
    }

    void Start()
    {
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayClick()
    {
        if (clickClip != null)
        {
            sfxSource.PlayOneShot(clickClip);
        }
    }
}