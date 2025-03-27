using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource playerSource;






    [SerializeField] private AudioClip Slash_Clip;
    [SerializeField] private AudioClip MainMenu_Clip;
    [SerializeField] private AudioClip Dash_Clip;
    [SerializeField] private AudioClip Teleportation_Clip;
    [SerializeField] private AudioClip FireOfDeath_Clip;
    [SerializeField] private AudioClip LeverSwitch_Clip;
    [SerializeField] private AudioClip LittleCreature_Clip;
    [SerializeField] private AudioClip Boss_Clip;
    [SerializeField] private AudioClip GameOver_Clip;
    [SerializeField] private AudioClip InGame_Clip;






    public static AudioHandler instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // ---

    public void SlashEffect()
    {
        playerSource.clip = Slash_Clip;
        playerSource.Play();
    }

    public void DashEffect()
    {
        playerSource.clip = Dash_Clip;
        playerSource.Play();
    }

    public void TeleportationEffect()
    {
        playerSource.clip = Teleportation_Clip;
        playerSource.Play();
    }

    public void FireOfDeathEffect()
    {
        playerSource.clip = FireOfDeath_Clip;
        playerSource.Play();
    }

    // ---

    public void BossSound()
    {
        effectSource.clip = Boss_Clip;

        effectSource.Play();
    }

    public void LittleCreatureSound()
    {
        effectSource.clip = LittleCreature_Clip;

        effectSource.Play();
    }

    public void LeverEffect()
    {
        effectSource.clip = LeverSwitch_Clip;

        effectSource.Play();
    }


    // ---

    string lastSceneMusicLoaded;
    public void ReloadBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            backgroundSource.clip = MainMenu_Clip;
        } else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            backgroundSource.clip = GameOver_Clip;
        } else {
            backgroundSource.clip = InGame_Clip;
        }

        backgroundSource.Play();
        lastSceneMusicLoaded = SceneManager.GetActiveScene().name;
    }


    private void Update()
    {
        if (lastSceneMusicLoaded != SceneManager.GetActiveScene().name)
        {
            ReloadBackgroundMusic();
        }
    }


}
