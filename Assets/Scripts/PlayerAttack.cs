using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
    private Animator animator;
    private PlayerCharacter playerCharacter;

    [SerializeField] private float attackDistance = 1f; // Distance for the attack detection
    [SerializeField] private LayerMask targetLayer; // Layer for the enemies to detect during attack

    private Vector2 lastAttackPosition; // Store the last attack position for Gizmos
    private Animator Sword,Body;

    private void Start() 
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        Sword = transform.GetChild(1).GetComponent<Animator>();
        Body = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Attack(string direction) 
    {
        StartCoroutine(HandleAttack(direction)); 
    }

    private IEnumerator HandleAttack(string direction) 
    {
        playerCharacter.isAttacking = true;
        ActivateWeapon(true);

        Body.Play($"Swing {direction}");
        Sword.Play($"Swing {direction}");

        lastAttackPosition = (Vector2)transform.position + GetCircleOffset(direction); 
        PerformOverlapCircle(direction);

        yield return new WaitForSeconds(GetAnimationDuration($"Swing {direction}"));

        ActivateWeapon(false);
        playerCharacter.isAttacking = false; 
    }

    private void ActivateWeapon(bool state) 
    {
        transform.GetChild(1).gameObject.SetActive(state);
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
            "Up" => new Vector2(0, 0.8f  ),
            "Down" => new Vector2(0, -0.8f ),
            "Left" => new Vector2(-0.8f , 0),
            "Right" => new Vector2(0.8f , 0),
            _ => Vector2.zero
        };
    }

    private void HandleHit(Collider2D collider) 
    {
        collider.gameObject.GetComponent<Health>().TakeDamage(5);
    }

    private float GetAnimationDuration(string animationName) 
    {
        foreach (AnimationClip clip in Body.runtimeAnimatorController.animationClips) 
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
