using UnityEngine;

public class GiveNewPowerObject : MonoBehaviour
{
    private SpriteRenderer selfSR;

    [SerializeField] private SkillsHandler.SkillsToGet skillToGive;
    [SerializeField] private Collider2D playerCollider;

    [SerializeField] private bool GivesThePower = true;

    [SerializeField] private Sprite slashObjectSprite;
    [SerializeField] private Sprite dashObjectSprite;
    [SerializeField] private Sprite teleportationObjectSprite;
    [SerializeField] private Sprite doubleEverythingObjectSprite;
    [SerializeField] private Sprite fireOfDeathObjectSprite;


    // ---

    private void Start()
    {
        selfSR = GetComponent<SpriteRenderer>();

        selfSR.sprite = GetGoodSprite(skillToGive);

        // ---
    }


    // ---


    private void OnCollisionEnter2D(Collision2D collision)
    {
        WhenCollision(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WhenCollision(collider);
    }


    private void WhenCollision(Collider2D collider)
    {
        if (collider == playerCollider)
        {
            AfterThisOne();

            SaveHandler.instance.AutoSaveGame();

            Destroy(gameObject);
        }
    }


    // ---

    private void AfterThisOne()
    {
        switch (skillToGive)
        {
            case SkillsHandler.SkillsToGet.slash:
                SkillsHandler.instance.haveTheSkill.slash = GivesThePower;
                break;

            case SkillsHandler.SkillsToGet.dash:
                SkillsHandler.instance.haveTheSkill.dash = GivesThePower;
                break;

            case SkillsHandler.SkillsToGet.teleportation:
                SkillsHandler.instance.haveTheSkill.teleportation = GivesThePower;
                break;

            case SkillsHandler.SkillsToGet.doubleEverything:
                SkillsHandler.instance.haveTheSkill.doubleEverything = GivesThePower;
                break;

            case SkillsHandler.SkillsToGet.fireOfDeath:
                SkillsHandler.instance.haveTheSkill.fireOfDeath = GivesThePower;
                break;
        }
    }

    private Sprite GetGoodSprite(SkillsHandler.SkillsToGet skill)
    {
        switch (skill)
        {
            case SkillsHandler.SkillsToGet.dash:
                return dashObjectSprite;

            case SkillsHandler.SkillsToGet.teleportation:
                return teleportationObjectSprite;

            case SkillsHandler.SkillsToGet.doubleEverything:
                return doubleEverythingObjectSprite;

            case SkillsHandler.SkillsToGet.fireOfDeath:
                return fireOfDeathObjectSprite;

            default:
                return slashObjectSprite;
        }
    }
}
