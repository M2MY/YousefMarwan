using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageHandler : MonoBehaviour
{
    // The player has a UNIQUE life.

    // ---

    private Collider2D selfCollider;

    private void Start()
    {
        selfCollider = GetComponent<Collider2D>();
    }


    #region Collision detection

    private void OnTriggerStay2D(Collider2D collider)
    {
        OnCollision(collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnCollision(collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.collider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.collider);
    }

    #endregion

    private const float damageImmuneTime = 0.5f;
    private float lastDamage;
    private void OnCollision(Collider2D collider)
    {
        if (((lastDamage + damageImmuneTime) > Time.time) || !collider.IsTouching(selfCollider)) return;

        if (collider.CompareTag("Damage") || collider.CompareTag("BossDamage"))
        {
            GameOver();
        }
    }


    #region Functions

    private void GameOver()
    {
        Debug.Log("Gameover");
        SkillsHandler.instance.ResetSkills();
        CountersHandler.instance.counters.gameOver += 1;
        SaveHandler.instance.AutoSaveGame();
        SceneManager.LoadScene("GameOver");
    }

    #endregion
}
