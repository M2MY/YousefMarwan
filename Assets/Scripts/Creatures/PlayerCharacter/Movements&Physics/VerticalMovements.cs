using UnityEngine;

public class VerticalMovements : MonoBehaviour
{
    public static bool JumpEnabled;

    private Rigidbody2D selfRigidbody;

    private const float jumpForce = 4f;
    
    private const float maxFallingVelocity = -20;

    #region Jump buffering

    private const float bufferDuration = 0.2f;
    private float bufferCounter;

    #endregion

    private bool jumping;

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody2D>();

        JumpEnabled = true;
    }

    private void Update()
    {
        if (selfRigidbody.velocity.y < maxFallingVelocity)
        {
            selfRigidbody.velocity = new Vector2 (selfRigidbody.velocity.x, maxFallingVelocity);
        }

        // ---
        // Jump

        if (JumpEnabled)
        {
            if (Input.GetKeyDown(SixKeysController.instance.mainController.aKey))
            {
                bufferCounter = Time.time + bufferDuration;
            }

            // ---

            if (GroundedState.instance.IsGroundedUsingCoyoteTime())
            {
                if (JumpWanted())
                {
                    selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, jumpForce);
                    jumping = true;
                    
                    CountersHandler.instance.counters.jump += 1;
                } else {
                    jumping = false;
                }
            }

            if (jumping && (selfRigidbody.velocity.y <= 0))
            {
                jumping = false;
            }

            // ---

            if (jumping && !Input.GetKey(SixKeysController.instance.mainController.aKey))
            {
                selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, 0);
            }
        }
    }

    private bool JumpWanted()
    {
        return Input.GetKeyDown(SixKeysController.instance.mainController.aKey) || (bufferCounter > Time.time);
    }
}
