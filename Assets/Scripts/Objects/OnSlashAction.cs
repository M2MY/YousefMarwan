using UnityEngine;

public class OnSlashAction : MonoBehaviour
{
    public ActionType actionType;

    private SpriteRenderer selfSR;


    [Header("OpenDoor")]
    [SerializeField] private GameObject door;

    [Header("Summon")]
    [SerializeField] private GameObject objectToSummon;
    [SerializeField] private Vector2 position;

    [Header("All Level - Sprites")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    public enum ActionType
    {
        OpenDoor,
        Summon,
    }

    private bool active;


    private void Start()
    {
        selfSR = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Collision(collider);
    }

    private void Collision(Collider2D collider)
    {
        if (collider.CompareTag("DamageFromPlayer"))
        {
            active = !active;
            LevelSprite();
            OnAction();
        }
    }

    private void LevelSprite()
    {
        if (active)
        {
            selfSR.sprite = activeSprite;
        } else{
            selfSR.sprite = inactiveSprite;
        }
    }

    private void OnAction()
    {
        AudioHandler.instance.LeverEffect();

        switch (actionType)
        {
            case ActionType.OpenDoor:
                door.SetActive(!door.activeSelf);
                break;

            case ActionType.Summon:
                if (active) Instantiate(objectToSummon, position, Quaternion.identity);
                break;

            default:
                break;
        }
    }
}
