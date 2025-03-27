using UnityEngine;

public class BasicCreature : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator creatureAnimator;
    [SerializeField] private Collider2D playerCollider;


    [Header("Creature's")]
    [SerializeField] private CreatureType typeOfCreature;
    [SerializeField] private float movementsSpeed;
    [SerializeField] private int maxLife = 5;
    [SerializeField] private LayerMask deadLayer_Antiplayer;
    [SerializeField] private LayerMask normalLayer;

    private float life;

    private float deadTime;
    private const float deadDuration = 10;


    public enum CreatureType
    {
        Stable,
        Moving,
    }


    // ---

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody2D>();
        life = maxLife;
        gameObject.layer = GetMaskLayer((int)normalLayer);
    }

    private void Update()
    {
        
        if (life > 0)
        {


            deadTime = Time.time;
            Behaviour();
        }
        else
        {
            if ((deadTime + deadDuration) > Time.time) // Not yet
            {
                gameObject.layer = GetMaskLayer((int)deadLayer_Antiplayer);
            }
            else {
                gameObject.layer = GetMaskLayer((int)normalLayer);


                life = maxLife;
            }
        }
        

        SetAnimatorBooleans();
    }

    private void Behaviour()
    {
        switch (typeOfCreature)
        {
            case CreatureType.Stable:
                StableCreatureBehaviour();
                break;

            case CreatureType.Moving:
                MovingCreatureBehaviour();
                break;

            default:
                break;
        }
    }



    private int Get1or0()
    {
        if (((int)Time.time % 2) == 0)
        {
            return 0;
        } else return 1;
    }

    #region Behaviours


    private void StableCreatureBehaviour()
    {
        DontMove();
    }


    private float delayCounter;
    private int doing;
    private int doingDirection;
    private void MovingCreatureBehaviour()
    {
        if (delayCounter > Time.time)
        {
            if (doing == 1)
            {
                if (doingDirection == 1)
                {
                    GoRight();
                } else {
                    GoLeft();
                }
            } else {
                DontMove();
            }

            return;
        }

        delayCounter = Time.time + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0();

        if (Get1or0() == 1)
        {
            doing = 1;
            if (Get1or0() == 1)
            {
                doingDirection = 1;
                GoRight();
            } else {
                doingDirection = -1;
                GoLeft();
            }

        } else {

            doing = 0;
            DontMove();

        }
    }



    #endregion


    #region Movements

    public bool IsGoingRight;
    public bool IsGoingLeft;

    private Rigidbody2D selfRigidbody;


    private void GoRight()
    {
        IsGoingRight = true;
        IsGoingLeft = false;
        selfRigidbody.velocity = new Vector2 (movementsSpeed, selfRigidbody.velocity.y);
    }

    private void GoLeft()
    {
        IsGoingRight = false;
        IsGoingLeft = true;
        selfRigidbody.velocity = new Vector2 (-movementsSpeed, selfRigidbody.velocity.y);
    }

    private void DontMove()
    {
        IsGoingRight = false;
        IsGoingLeft = false;
        selfRigidbody.velocity = new Vector2 (0, 0);
    }

    #endregion


    // ---

    private void SetAnimatorBooleans()
    {
        creatureAnimator.SetBool("WalkingRight", IsGoingRight);
        creatureAnimator.SetBool("WalkingLeft", IsGoingLeft);
        creatureAnimator.SetBool("Dead", life <= 0);
    }


    // ---


    #region Collisions

    private float lastDamage;
    private const float delayBetweenDamage = 0.1f;


    private void OnCollisionStay2D(Collision2D collision)
    {
        WhenCollision(collision.collider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WhenCollision(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WhenCollision(collider);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        WhenCollision(collider);
    }

    private void WhenCollision(Collider2D collider)
    {
        if ((lastDamage + delayBetweenDamage) > Time.time) return;
        
        if (collider.CompareTag("DamageFromPlayer"))
        {
            AudioHandler.instance.LittleCreatureSound();
            
            if (SkillsHandler.instance.haveTheSkill.doubleEverything)
            {
                life -= SkillsHandler.doubleSlashDamage;
            }
            else {
                life -= SkillsHandler.simpleSlashDamage;
            }
            lastDamage = Time.time;
        }
        
        else if (collider.CompareTag("FireOfDeath"))
        {
            life = 0;
            lastDamage = Time.time;
        }

        if (life <= 0)
        {
            CountersHandler.instance.counters.littleCreaturesKilled += 1;
        }
    }







    #endregion




    private int GetMaskLayer(int layerMask) {

        for(int i = 0; i < 32; i++) {

            int value = 1 << i;
        
            if((layerMask & value) == value)
                return i;
        }
        return 0;
    }

}
