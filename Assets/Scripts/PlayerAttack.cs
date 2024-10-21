using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
    private Animator animator;
    private PlayerCharacter playerCharacter;

    [SerializeField] private float attackDistance = 1f; // Distance for the attack detection
    [SerializeField] private LayerMask targetLayer; // Layer for the enemies to detect during attack

    private Vector2 lastAttackPosition; // Store the last attack position for Gizmos

    private void Start() 
    {
        animator = GetComponent<Animator>();
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    public void Attack(string direction) 
    {
        StartCoroutine(HandleAttack(direction)); 
    }

    private IEnumerator HandleAttack(string direction) 
    {
        playerCharacter.isAttacking = true;
        ActivateWeapon(true);

        animator.Play($"Swing {direction}");
        PlayChildAnimation($"Swing {direction}");

        lastAttackPosition = (Vector2)transform.position + GetCircleOffset(direction); // Store position for Gizmos
        PerformOverlapCircle(direction);

        yield return new WaitForSeconds(GetAnimationDuration($"Swing {direction}"));

        ActivateWeapon(false);
        playerCharacter.isAttacking = false; 
    }

    private void ActivateWeapon(bool state) 
    {
        transform.GetChild(0).gameObject.SetActive(state);
    }

    private void PlayChildAnimation(string animationName) 
    {
        transform.GetChild(0).GetComponent<Animator>().Play(animationName);
    }

    private void PerformOverlapCircle(string direction) 
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(lastAttackPosition, attackDistance, targetLayer);

        foreach (var hit in hits) 
        {
            HandleHit(hit);
        }
    }

    private Vector2 GetCircleOffset(string direction) 
    {
        return direction switch 
        {
            "Up" => new Vector2(0, attackDistance / 2),
            "Down" => new Vector2(0, -attackDistance / 2),
            "Left" => new Vector2(-attackDistance / 2, 0),
            "Right" => new Vector2(attackDistance / 2, 0),
            _ => Vector2.zero
        };
    }

    private void HandleHit(Collider2D collider) 
    {
        Debug.Log($"Hit {collider.name}");
        // Additional hit logic here, such as dealing damage
    }

    private float GetAnimationDuration(string animationName) 
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips) 
        {
            if (clip.name == animationName) 
            {
                return clip.length; 
            }
        }
        return 0f; 
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lastAttackPosition, attackDistance);
    }
}
