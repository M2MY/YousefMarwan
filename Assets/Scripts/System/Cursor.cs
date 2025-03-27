using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public static Cursor instance;

    #region Variables

    public Selectable actualSelected;

    #endregion

    private void Awake()
    {
        #region Instance
        if (instance != null)
        {
            Debug.Log("An instance of 'Cursor' already exists.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        #endregion

        // Dontdestroyonload

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        // ---

        if (actualSelected != null)
        {
            if (!actualSelected.isActiveAndEnabled || !actualSelected.interactable)
            {
                actualSelected = null;
                return;
            }

            actualSelected.Select();

            if (Input.GetKeyDown(SixKeysController.instance.mainController.aKey))
            {
                if (actualSelected.GetComponent<Button>() != null)
                {
                    actualSelected.GetComponent<Button>().onClick.Invoke();
                }
            }
            else if (Input.GetKeyDown(SixKeysController.instance.mainController.upKey))
            {
                if (actualSelected.navigation.selectOnUp != null)
                {
                    Select(actualSelected.navigation.selectOnUp);
                }
            }
            else if (Input.GetKeyDown(SixKeysController.instance.mainController.downKey))
            {
                if (actualSelected.navigation.selectOnDown != null)
                {
                    Select(actualSelected.navigation.selectOnDown);
                }
            }
            else if (Input.GetKeyDown(SixKeysController.instance.mainController.leftKey))
            {
                if (actualSelected.navigation.selectOnLeft != null)
                {
                    Select(actualSelected.navigation.selectOnLeft);
                }
            }
            else if (Input.GetKeyDown(SixKeysController.instance.mainController.rightKey))
            {
                if (actualSelected.navigation.selectOnRight != null)
                {
                    Select(actualSelected.navigation.selectOnRight);
                }
            }

        }
        else
        {
            
        }
    }


    #region 

    public void Select(Selectable toSelect)
    {
        actualSelected = toSelect;
    }

    public void SelectNothing()
    {
        actualSelected = null;
    }


    #endregion
}
