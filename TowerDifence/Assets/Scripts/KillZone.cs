using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class KillZone : MonoBehaviour
{
    public event UnityAction EnemyPassed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(int.MaxValue);
            EnemyPassed?.Invoke();
        }
    }
}
