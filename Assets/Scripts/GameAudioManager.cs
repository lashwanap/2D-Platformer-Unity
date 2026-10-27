using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager instance;

    public AudioClip introMusic;
    public AudioClip gameplayMusic;
    public AudioClip clickSound;
    public AudioClip gameOverSound;
    public AudioClip coinSound;
    public AudioClip jumpSound;

    private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource = gameObject.AddComponent<AudioSource>(); // Automatique
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioClip chosenMusic = (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Menu") ? introMusic : gameplayMusic;
        musicSource.clip = chosenMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    // 🔊 Lecture automatique, sans source assignée
    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    // Raccourcis pratiques
    public void PlayClick() => PlaySound(clickSound);
    public void PlayCoin() => PlaySound(coinSound);
    public void PlayJump() => PlaySound(jumpSound);
    public void PlayGameOver() => PlaySound(gameOverSound);
}
