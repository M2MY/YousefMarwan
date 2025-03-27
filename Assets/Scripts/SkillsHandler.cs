using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillsHandler : MonoBehaviour
{
    // All skills :
    /*
        1- Slash
        2- Dash
        3- Teleportation
        4- Double (Jump, dash, slash)
        5- Fire of Death
    */

    public static SkillsHandler instance;

    private const float timeScaleArriveInSeconds = 0.3f;
    private const float slowedTimeScale = 0.1f;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("An instance of 'SkillsHandler' already exists.");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        haveTheSkill = SaveHandler.instance.GetSaveData().haveSkills;
    }

    private void Update()
    {
        if (GroundedState.instance.IsOnGround)
        {
            numberOfDashUsed = 0;
        }

        // ---

        // Slow motion effect
        if (Input.GetKey(SixKeysController.instance.mainController.bKey))
        {
            if (Time.timeScale > slowedTimeScale)
            {
                Time.timeScale -= Time.deltaTime / timeScaleArriveInSeconds;
            }
        } else {
            if (Time.timeScale < 1)
            {
                Time.timeScale += Time.deltaTime / timeScaleArriveInSeconds;
                if (Time.timeScale > 1) Time.timeScale = 1;
            }
        }

        // ---

        CheckIfHaveSelectedSkill();
        Navigate();

        // ---
        #region Direction calculations
        Direction direction = (HorizontalMovements.lastDirectionLookingAt == 1) ? Direction.East : Direction.West;

        if (Input.GetKey(SixKeysController.instance.mainController.upKey) && !Input.GetKey(SixKeysController.instance.mainController.downKey))
        {
            if (Input.GetKey(SixKeysController.instance.mainController.rightKey) && !Input.GetKey(SixKeysController.instance.mainController.leftKey))
            {
                direction = Direction.NorthEast;
            }
            else if (Input.GetKey(SixKeysController.instance.mainController.leftKey) && !Input.GetKey(SixKeysController.instance.mainController.rightKey))
            {
                direction = Direction.NorthWest;
            }
            else
            {
                direction = Direction.North;
            }
        }
        else if (Input.GetKey(SixKeysController.instance.mainController.downKey) && !Input.GetKey(SixKeysController.instance.mainController.upKey))
        {
            if (Input.GetKey(SixKeysController.instance.mainController.rightKey) && !Input.GetKey(SixKeysController.instance.mainController.leftKey))
            {
                direction = Direction.SouthEast;
            }
            else if (Input.GetKey(SixKeysController.instance.mainController.leftKey) && !Input.GetKey(SixKeysController.instance.mainController.rightKey))
            {
                direction = Direction.SouthWest;
            }
            else
            {
                direction = Direction.South;
            }
        } else {
            if (Input.GetKey(SixKeysController.instance.mainController.rightKey) && !Input.GetKey(SixKeysController.instance.mainController.leftKey))
            {
                direction = Direction.East;
            }
            else if (Input.GetKey(SixKeysController.instance.mainController.leftKey) && !Input.GetKey(SixKeysController.instance.mainController.rightKey))
            {
                direction = Direction.West;
            }
        }
        #endregion

        // ---


        if (Input.GetKeyUp(SixKeysController.instance.mainController.bKey))
        {
            UseSkill(selectedSkill, direction);
        }
    }



    // ---
        // ------
    // ---

    [Serializable]
    public struct HavingSkills
    {
        public bool slash;
        public bool dash;
        public bool teleportation;
        public bool doubleEverything;
        public bool fireOfDeath;
    }

    public enum SkillsToGet
    {
        slash,
        dash,
        teleportation,
        doubleEverything,
        fireOfDeath,
    }

    public enum SelectableSkills
    {
        LeaveGame,
        Slash,
        Dash,
        Teleportation,
        FireOfDeath,
    }

    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
    }

    // ---

    public HavingSkills haveTheSkill;
    public SelectableSkills selectedSkill;


    

    #region Funcions

    private void CheckIfHaveSelectedSkill()
    {
        switch (selectedSkill)
        {
            case SelectableSkills.Slash:
                if (haveTheSkill.slash) return;
                else selectedSkill = SelectableSkills.LeaveGame;
                break;
            
            case SelectableSkills.Dash:
                if (haveTheSkill.dash) return;
                else selectedSkill = SelectableSkills.LeaveGame;
                break;
            
            case SelectableSkills.Teleportation:
                if (haveTheSkill.teleportation) return;
                else selectedSkill = SelectableSkills.LeaveGame;
                break;
            
            case SelectableSkills.FireOfDeath:
                if (haveTheSkill.fireOfDeath) return;
                else selectedSkill = SelectableSkills.LeaveGame;
                break;
        }
    }


    // ---


    private void UseSkill(SelectableSkills skillToExecute, Direction direction)
    {
        switch (skillToExecute)
        {
            case SelectableSkills.LeaveGame:
                LeaveGame();
                break;

            case SelectableSkills.Slash:
                Slash(direction);
                break;

            case SelectableSkills.Dash:
                Dash(direction);
                break;

            case SelectableSkills.Teleportation:
                Teleportation(direction);
                break;

            case SelectableSkills.FireOfDeath:
                FireOfDeath();
                break;

            default:
                break;
        }
    }


    #region Skills functions

    // ---
    // Skills variables

    #region LeaveGame()
    private const string sceneToLoadAfterLeave = "MainMenu";
    #endregion

    #region Slash()

    [Header("Slash")]

    public bool isSlashing;

    [SerializeField] private GameObject slashGO_North;
    [SerializeField] private SpriteRenderer slashGraphics_North;
    
    [SerializeField] private GameObject slashGO_NorthEast;
    [SerializeField] private SpriteRenderer slashGraphics_NorthEast;
    
    [SerializeField] private GameObject slashGO_East;
    [SerializeField] private SpriteRenderer slashGraphics_East;
    
    [SerializeField] private GameObject slashGO_SouthEast;
    [SerializeField] private SpriteRenderer slashGraphics_SouthEast;
    
    [SerializeField] private GameObject slashGO_South;
    [SerializeField] private SpriteRenderer slashGraphics_South;
    
    [SerializeField] private GameObject slashGO_SouthWest;
    [SerializeField] private SpriteRenderer slashGraphics_SouthWest;
    
    [SerializeField] private GameObject slashGO_West;
    [SerializeField] private SpriteRenderer slashGraphics_West;
    
    [SerializeField] private GameObject slashGO_NorthWest;
    [SerializeField] private SpriteRenderer slashGraphics_NorthWest;

    [SerializeField] private Sprite firstHalfSprite;
    [SerializeField] private Sprite secondHalfSprite;

    private const float slashDuration = 0.1f;

    private float slashCounter;
    public const int simpleSlashDamage = 1;
    public const int doubleSlashDamage = 5;

    #endregion

    #region Dash()

    [Header("Dash")]
    [SerializeField] private Rigidbody2D selfRigidbody;
    private const float dashDuration = 0.25f;
    private const float dashForce = 10;
    private const float dashDelay = 0.5f;
    private int numberOfDashUsed;
    private float dashCounter;
    public bool IsDashing;

    #endregion

    #region Teleportation()

    [Header("Teleportation")]
    [SerializeField] private GameObject teleportationToolPrefab; // Add the player to a layer and this one too, make them not collidable.
    public Transform selfTransform; // The player's

    [SerializeField] private float toolNormalSpeed = 5;

    private const float teleportationDelay = 3f;
    public const float teleporationToolDuration = 5f; // The time for the teleportationTool to do its work is 5f.
    private float teleportationCounter;


    #endregion

    #region FireOfDeath()

    [Header("Fire of Death")]
    [SerializeField] private GameObject fireOfDeathPrefab;
    public const float fireOfDeathDuration = 10f; // To use in the new class.

    private const float fireOfDeathDelay = 20f;
    private float fireOfDeathCounter;

    // Also using the transform of the player that is in the Teleportation() region.

    #endregion

    // ---

    private void LeaveGame()
    {
        SaveHandler.instance.AutoSaveGame();
        SceneManager.LoadScene(sceneToLoadAfterLeave);
    }

    private void Slash(Direction direction)
    {
        if (slashCounter > Time.time) return;

        slashCounter = Time.time + slashDuration;

        CountersHandler.instance.counters.slash += 1;

        // ---

        SpriteRenderer toGive_SR;
        GameObject toGive_GO;

        #region Calculations with direction

        switch (direction)
        {

            case Direction.North:
                toGive_GO = slashGO_North;
                toGive_SR = slashGraphics_North;
                break;
            
            case Direction.NorthEast:
                toGive_GO = slashGO_NorthEast;
                toGive_SR = slashGraphics_NorthEast;
                break;
            
            case Direction.East:
                toGive_GO = slashGO_East;
                toGive_SR = slashGraphics_East;
                break;
            
            case Direction.SouthEast:
                toGive_GO = slashGO_SouthEast;
                toGive_SR = slashGraphics_SouthEast;
                break;
            
            case Direction.South:
                toGive_GO = slashGO_South;
                toGive_SR = slashGraphics_South;
                break;
            
            case Direction.SouthWest:
                toGive_GO = slashGO_SouthWest;
                toGive_SR = slashGraphics_SouthWest;
                break;
            
            case Direction.West:
                toGive_GO = slashGO_West;
                toGive_SR = slashGraphics_West;
                break;
            
            case Direction.NorthWest:
                toGive_GO = slashGO_NorthWest;
                toGive_SR = slashGraphics_NorthWest;
                break;

            default:
                toGive_GO = slashGO_East;
                toGive_SR = slashGraphics_East;
                break;
        }

        #endregion

        // ---

        StartCoroutine(SlashCoroutine(toGive_GO, toGive_SR));
    }

    private void Dash(Direction direction)
    {
        int maxDashUse;
        if (haveTheSkill.doubleEverything) maxDashUse = 2;
        else maxDashUse = 1;

        if (numberOfDashUsed >= maxDashUse) return;

        // ---
        
        if (dashCounter > Time.time) return;

        if (numberOfDashUsed >= maxDashUse)
            dashCounter = Time.time + dashDuration + dashDelay;

        numberOfDashUsed ++;

        // ---

        CountersHandler.instance.counters.dash += 1;

        StartCoroutine(DashCoroutine(GetDirectionVector(direction)));
    }

    private void Teleportation(Direction direction)
    {
        if (teleportationCounter > Time.time) return;

        teleportationCounter = Time.time + teleporationToolDuration + teleportationDelay;

        // ---

        CountersHandler.instance.counters.teleportation += 1;

        TeleportationTool actualTool = Instantiate(teleportationToolPrefab, selfTransform.position, Quaternion.identity)
            .GetComponent<TeleportationTool>();

        actualTool.LoadData(haveTheSkill.doubleEverything ? toolNormalSpeed*2 : toolNormalSpeed, GetDirectionVector(direction), selfTransform);
    }

    private void FireOfDeath()
    {
        if (fireOfDeathCounter > Time.time) return;

        fireOfDeathCounter = Time.time + fireOfDeathDuration + fireOfDeathDelay;
        // ---

        CountersHandler.instance.counters.fireOfDeath += 1;


        AudioHandler.instance.FireOfDeathEffect();

        FireOfDeath newFire = Instantiate(fireOfDeathPrefab, selfTransform.position, Quaternion.identity)
            .GetComponent<FireOfDeath>();

        newFire.LoadData(selfTransform);
    }

    #region Complementary functions / coroutines

    private IEnumerator SlashCoroutine(GameObject slashGO, SpriteRenderer slashGraphics)
    {
        HorizontalMovements.CanMove = false;
        VerticalMovements.JumpEnabled = false;

        isSlashing = true;

        // ---

        AudioHandler.instance.SlashEffect();

        // ---

        slashGraphics.sprite = firstHalfSprite;
        slashGO.SetActive(true);

        yield return new WaitForSeconds(slashDuration/2);

        slashGraphics.sprite = secondHalfSprite;

        yield return new WaitForSeconds(slashDuration/2);
        
        slashGO.SetActive(false);

        isSlashing = false;

        HorizontalMovements.CanMove = true;
        VerticalMovements.JumpEnabled = true;
    }

    private IEnumerator DashCoroutine(Vector2 vectorDirector)
    {
        HorizontalMovements.CanMove = false;
        IsDashing = true;

        // ---

        AudioHandler.instance.DashEffect();

        // ---

        float stopWhen = Time.time + dashDuration;

        while (Time.time < stopWhen)
        {
            selfRigidbody.velocity = vectorDirector * dashForce;

            yield return null;
        }

        selfRigidbody.velocity = Vector2.zero;

        IsDashing = false;
        HorizontalMovements.CanMove = true;
    }


    private Vector2 GetDirectionVector(Direction direction)
    {
        // Diagonal ~ 0.71

        switch (direction)
        {
            case Direction.North:
                return Vector2.up;

            case Direction.NorthEast:
                return new Vector2(0.71f, 0.71f);

            case Direction.East:
                return Vector2.right;

            case Direction.SouthEast:
                return new Vector2(0.71f, -0.71f);

            case Direction.South:
                return Vector2.down;

            case Direction.SouthWest:
                return new Vector2(-0.71f, -0.71f);

            case Direction.West:
                return Vector2.left;

            case Direction.NorthWest:
                return new Vector2 (-0.71f, 0.71f);

            default:
                return Vector2.zero;
        }
    }

    #endregion

    #endregion




    #region UI functions

    #region Variables

    [Header("Skill Navigation")]

    [SerializeField] private Image actualSkillImage;
    [SerializeField] private Image arrowUp;
    [SerializeField] private Image arrowDown;

    // Sprites
    [SerializeField] private Sprite LeaveGameSprite;
    [SerializeField] private Sprite SlashSprite;
    [SerializeField] private Sprite DashSprite;
    [SerializeField] private Sprite TeleportationSprite;
    [SerializeField] private Sprite FireOfDeathSprite;


    #endregion

    private void ReloadSkillImage()
    {
        actualSkillImage.sprite = GetSkillSprite(selectedSkill);
    }

    private Sprite GetSkillSprite(SelectableSkills skill)
    {
        switch (skill)
        {
            case SelectableSkills.Slash:
                return SlashSprite;

            case SelectableSkills.Dash:
                return DashSprite;

            case SelectableSkills.Teleportation:
                return TeleportationSprite;

            case SelectableSkills.FireOfDeath:
                return FireOfDeathSprite;

            default:
                return LeaveGameSprite;
        }
    }


    // ---
    // Navigation

    private bool CanNavigate()
    {
        return haveTheSkill.slash || haveTheSkill.dash || haveTheSkill.teleportation || haveTheSkill.fireOfDeath;
    }

    #region Hide/Show
    private void HideAll()
    {
        actualSkillImage.color = new Color(actualSkillImage.color.r, actualSkillImage.color.g, actualSkillImage.color.b, 0);
        
        HideArrows();
    }

    private void HideArrows()
    {
        arrowDown.color = new Color(arrowDown.color.r, arrowDown.color.g, arrowDown.color.b, 0);
        arrowUp.color = new Color(arrowUp.color.r, arrowUp.color.g, arrowUp.color.b, 0);
    }

    private void ShowAll()
    {
        actualSkillImage.color = new Color(actualSkillImage.color.r, actualSkillImage.color.g, actualSkillImage.color.b, 1);
        arrowDown.color = new Color(arrowDown.color.r, arrowDown.color.g, arrowDown.color.b, 1);
        arrowUp.color = new Color(arrowUp.color.r, arrowUp.color.g, arrowUp.color.b, 1);
    }
    #endregion


    // Tree Root
    private void Navigate()
    {
        if (CanNavigate())
        {
            ShowAll();

            if (Input.GetKeyDown(SixKeysController.instance.mainController.upKey) && Input.GetKey(SixKeysController.instance.mainController.bKey))
            {
                selectedSkill = GetUpSkill_Have(selectedSkill);
            }
            
            else if (Input.GetKeyDown(SixKeysController.instance.mainController.downKey) && Input.GetKey(SixKeysController.instance.mainController.bKey))
            {
                selectedSkill = GetDownSkill_Have(selectedSkill);
            }

        }
        else
        {
            ShowAll();
            HideArrows();
        }
        
        
        ReloadSkillImage();
    }



    private bool HaveTheSkill(SelectableSkills skill)
    {
        if (skill == SelectableSkills.LeaveGame)
        {
            return true;
        } else if (skill == SelectableSkills.Slash)
        {
            return haveTheSkill.slash;
        } else if (skill == SelectableSkills.Dash)
        {
            return haveTheSkill.dash;
        } else if (skill == SelectableSkills.Teleportation)
        {
            return haveTheSkill.teleportation;
        } else if (skill == SelectableSkills.FireOfDeath)
        {
            return haveTheSkill.fireOfDeath;
        }

        return false;
    }

    private SelectableSkills GetUpSkill_Have(SelectableSkills actualSkill)
    {
        SelectableSkills toReturn = actualSkill;

        for (int i = 0; i < 5; i++)
        {
            if (HaveTheSkill(GetUpSkill(toReturn)))
                return GetUpSkill(toReturn);
            else
                toReturn = GetUpSkill(toReturn);
        }

        if (!HaveTheSkill(toReturn))
            return SelectableSkills.LeaveGame;

        return toReturn;
    }
    
    private SelectableSkills GetDownSkill_Have(SelectableSkills actualSkill)
    {
        SelectableSkills toReturn = actualSkill;

        for (int i = 0; i < 5; i++)
        {
            if (HaveTheSkill(GetDownSkill(toReturn)))
                return GetDownSkill(toReturn);
            else
                toReturn = GetDownSkill(toReturn);
        }

        if (!HaveTheSkill(toReturn))
            return SelectableSkills.LeaveGame;

        return toReturn;
    }

    private SelectableSkills GetUpSkill(SelectableSkills actualSkill)
    {
        switch (actualSkill)
        {
            case SelectableSkills.LeaveGame:
                return SelectableSkills.FireOfDeath;

            case SelectableSkills.Slash:
                return SelectableSkills.LeaveGame;

            case SelectableSkills.Dash:
                return SelectableSkills.Slash;

            case SelectableSkills.Teleportation:
                return SelectableSkills.Dash;

            case SelectableSkills.FireOfDeath:
                return SelectableSkills.Teleportation;
            
            default:
                return SelectableSkills.LeaveGame;
        }
    }

    private SelectableSkills GetDownSkill(SelectableSkills actualSkill)
    {
        switch (actualSkill)
        {
            case SelectableSkills.LeaveGame:
                return SelectableSkills.Slash;

            case SelectableSkills.Slash:
                return SelectableSkills.Dash;

            case SelectableSkills.Dash:
                return SelectableSkills.Teleportation;

            case SelectableSkills.Teleportation:
                return SelectableSkills.FireOfDeath;

            case SelectableSkills.FireOfDeath:
                return SelectableSkills.LeaveGame;
            
            default:
                return SelectableSkills.LeaveGame;
        }
    }

    /*
    Order:
        LeaveGame,
        Slash,
        Dash,
        Teleportation,
        FireOfDeath,
    */


    #endregion


    #endregion




    public void ResetSkills()
    {
        haveTheSkill = new HavingSkills();
    }

}
