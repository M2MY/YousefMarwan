using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private const float gameOverTime = 11.78f;



    void Start()
    {
        // Start playing sound
        StartCoroutine(GameOverCoroutine());
    }


    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(gameOverTime);

        SceneManager.LoadScene("MainMenu");
    }
}
