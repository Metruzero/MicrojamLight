using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource shopMusic, gameMusic;

    public void PlayShopMusic()
    {
        gameMusic.Stop();
        shopMusic.Play();

    }

    public void PlayGameMusic()
    {
        shopMusic.Stop();
        gameMusic.Play();
    }
}
