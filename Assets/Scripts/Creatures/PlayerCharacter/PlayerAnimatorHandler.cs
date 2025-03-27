using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        // WalkingRight
        if (HorizontalMovements.WalkingRight)
        {
            animator.SetBool("WalkingRight", true);
        } else if (SkillsHandler.instance.IsDashing || SkillsHandler.instance.isSlashing)
        {
            if (HorizontalMovements.lastDirectionLookingAt == 1)
            {
                animator.SetBool("WalkingRight", true);
            }
        } else
        {
            animator.SetBool("WalkingRight", false);
        }

        // WalkingLeft
        if (HorizontalMovements.WalkingLeft)
        {
            animator.SetBool("WalkingLeft", true);
        } else if (SkillsHandler.instance.IsDashing || SkillsHandler.instance.isSlashing)
        {
            if (HorizontalMovements.lastDirectionLookingAt == -1)
            {
                animator.SetBool("WalkingLeft", true);
            }
        } else
        {
            animator.SetBool("WalkingLeft", false);
        }        
    }
}
