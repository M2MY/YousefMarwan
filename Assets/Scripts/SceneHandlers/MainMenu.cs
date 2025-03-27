using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // References
    [SerializeField] private GameObject creditsWindow;
    
    private const string sceneToLoadPlay = "Level0";


    // Variables / Constants
    private const float creditsTime = 10;

    // Buttons
    [SerializeField] private Selectable playButton; // To select at the beginning.
    [SerializeField] private Selectable creditsButton; // To select after showing.

    // ---

    private void Awake()
    {
        // Initialisation
        creditsWindow.SetActive(false);

    }

    private void Start()
    {
        Cursor.instance.Select(playButton);

    }



    #region Main Menu Buttons

    public void PlayButton()
    {
        SceneManager.LoadScene(sceneToLoadPlay);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditsCopyrightsButton()
    {
        StartCoroutine(ShowCreditsCopyrights());
    }

    #endregion


    // Coroutine

    private IEnumerator ShowCreditsCopyrights()
    {
        Cursor.instance.SelectNothing();
        creditsWindow.SetActive(true);

        yield return new WaitForSeconds(creditsTime);

        creditsWindow.SetActive(false);
        Cursor.instance.Select(creditsButton);
    }

}
