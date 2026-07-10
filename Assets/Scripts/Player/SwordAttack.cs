using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float range = 1f;
    [SerializeField] private int damage = 1;

    [Header("Detection")]
    [SerializeField] private float colliderDistance = 1f;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask enemyLayer;

    public void Attack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.right,
            0,
            enemyLayer);

        if (hit.collider != null)
        {
            Health health = hit.collider.GetComponent<Health>();

            if (health != null)
                health.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range,
                        boxCollider.bounds.size.y,
                        boxCollider.bounds.size.z));
    }
}