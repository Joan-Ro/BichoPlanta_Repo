using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolumen(float volumen)
    {
        audioSource.volume = volumen;
    }
}

