using UnityEngine;

public class GroundedState : MonoBehaviour
{
    #region Variables
    public static GroundedState instance;

    public bool IsOnGround;
    public bool IsGroundedUsingCoyoteTime()
    {
        if (IsOnGround)
        {
            return true;
        }
        else {
            return coyoteTimeCounter > Time.time;
        }
    }
    
    
    private const float coyoteTime = 0.2f;
    
    
    private float coyoteTimeCounter;

    #endregion

    private void Awake()
    {
        #region Instance
        if (instance != null)
        {
            Debug.Log("An instance of 'GroundedState' already exists.");
            return;
        }

        instance = this;

        #endregion


    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            IsOnGround = true;
            coyoteTimeCounter = Time.time + coyoteTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            IsOnGround = false;
        }
    }
}
