using UnityEngine;

public class TheBoss : MonoBehaviour
{
    [Header("Components")]
    private Animator creatureAnimator;
    private Transform playerTransform;


    [Header("Boss'")]
    [SerializeField] private float movementsSpeed;
    [SerializeField] private int maxLife = 100;
    [SerializeField] private LayerMask deadLayer_Antiplayer;
    [SerializeField] private LayerMask normalLayer;

    private float life;


    // ---

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody2D>();
        creatureAnimator = GetComponent<Animator>();
        playerTransform = SkillsHandler.instance.selfTransform;
        life = maxLife;
        gameObject.layer = GetMaskLayer((int)normalLayer);
    }

    private void Update()
    {
        
        if (life > 0)
        {
            Behaviour();
        }
        else
        {
            Destroy(gameObject);
        }
        

        SetAnimatorBooleans();
    }

    private void Behaviour()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        BossBehaviour();
    }



    private int Get1or0()
    {
        if (((int)Time.time % 2) == 0)
        {
            return 0;
        } else return 1;
    }

    #region Behaviours


    private float followDuration;
    private float restDuration;
    private int follow1Rest0;
    private void BossBehaviour()
    {
        if ((followDuration > Time.time) && (follow1Rest0 == 1))
        {
            MoveTowards(playerTransform);
        }
        else if ((restDuration > Time.time) && (follow1Rest0 == 0))
        {
            DontMove();
        }
        else
        {
            followDuration = Time.time + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0();
            restDuration = Time.time + Get1or0() + Get1or0() + Get1or0() + Get1or0() + Get1or0();

            follow1Rest0 = Get1or0();

            AudioHandler.instance.BossSound();

            if (follow1Rest0 == 1)
            {
                MoveTowards(playerTransform);
            } else if (follow1Rest0 == 0)
            {
                DontMove();
            }
        }
    }



    #endregion


    #region Movements


    private Rigidbody2D selfRigidbody;


    private void DontMove()
    {
        IsMoving = false;
        selfRigidbody.velocity = new Vector2 (0, 0);
    }


    private void MoveTowards(Transform target)
    {
        IsMoving = true;
        Vector2 velocity = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
        velocity.Normalize();

        selfRigidbody.velocity = velocity * movementsSpeed;
    }

    #endregion


    // ---

    private bool IsMoving;
    private void SetAnimatorBooleans()
    {
        creatureAnimator.SetBool("Moving", IsMoving);
        creatureAnimator.SetBool("Idle", !IsMoving);
    }


    // ---


    #region Collisions

    private float lastDamage;
    private const float delayBetweenDamage = 0.5f;


    private void OnCollisionStay2D(Collision2D collision)
    {
        WhenCollision(collision.collider);
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
            life -= 10;
            lastDamage = Time.time;
        }

        if (life <= 0)
        {
            CountersHandler.instance.counters.bossKilled += 1;
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
