using UnityEngine;

public class HorizontalMovements : MonoBehaviour
{
    public static bool CanMove;
    public static int lastDirectionLookingAt = 1;

    // Walking booleans
    public static bool WalkingRight;
    public static bool WalkingLeft;

    private Rigidbody2D selfRigidbody;
    private const float horizontalSpeed = 3f;

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody2D>();

        if (selfRigidbody == null)
        {
            Debug.Log("There is an error while taking the <Rigidbody2D> component. -HorizontalMovements");
        }

        CanMove = true;
    }

    private void Update()
    {
        if (CanMove)
        {
            if (Input.GetKey(SixKeysController.instance.mainController.rightKey) && !Input.GetKey(SixKeysController.instance.mainController.leftKey))
            {
                selfRigidbody.velocity = new Vector2(horizontalSpeed, selfRigidbody.velocity.y);
                lastDirectionLookingAt = 1;
                
                WalkingRight = true;
                WalkingLeft = false;
            } else if (Input.GetKey(SixKeysController.instance.mainController.leftKey) && !Input.GetKey(SixKeysController.instance.mainController.rightKey))
            {
                selfRigidbody.velocity = new Vector2(-1 * horizontalSpeed, selfRigidbody.velocity.y);
                lastDirectionLookingAt = -1;
                
                WalkingRight = false;
                WalkingLeft = true;
            } else {
                if (!SkillsHandler.instance.IsDashing)
                    selfRigidbody.velocity = new Vector2(0, selfRigidbody.velocity.y);

                WalkingRight = false;
                WalkingLeft = false;
            }
        }
        else {
            if (!SkillsHandler.instance.IsDashing)
                selfRigidbody.velocity = new Vector2(0, selfRigidbody.velocity.y);

            WalkingRight = false;
            WalkingLeft = false;
        }


        if (WalkingRight)
        {
            CountersHandler.instance.counters.timeGoRight += Time.deltaTime;
        } else if (WalkingLeft)
        {
            CountersHandler.instance.counters.timeGoLeft += Time.deltaTime;
        }
    }
}
