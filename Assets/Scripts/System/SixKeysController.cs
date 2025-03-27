using UnityEngine;

public class SixKeysController : MonoBehaviour
{  
    public static SixKeysController instance;

    public Controller mainController;

    // ---

    private void Awake()
    {
        #region Instance
        if (instance != null)
        {
            Debug.Log("An instance of 'SixKeyController' already exists.");
            
            Destroy(gameObject);
            return;
        }

        instance = this;
        #endregion

        // Setting DontDestroyOnLoad

        DontDestroyOnLoad(gameObject);


        // Initialisation of the main controller

        mainController = new Controller 
        {
            aKey = KeyCode.W,
            bKey = KeyCode.X,
            upKey = KeyCode.UpArrow,
            downKey = KeyCode.DownArrow,
            leftKey = KeyCode.LeftArrow,
            rightKey = KeyCode.RightArrow,
        };
    }

    // ---

    public struct Controller
    {
        public KeyCode aKey;
        public KeyCode bKey;
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode leftKey;
        public KeyCode rightKey;
    }
}
