using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;

    private int _damage;
    private Enemy _target;
    private Vector3 _lasttargetPosition;
    private Rigidbody2D _rigidbody;
    private float _baseAngel = 75;
    
    private void Start()
    {
        _lasttargetPosition = _target.transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_target != null)
        {
            MoveToTarget(_target.transform.position);
            _lasttargetPosition = _target.transform.position;
        }
        else
        {
            MoveToTarget(_lasttargetPosition);

            if (transform.position == _lasttargetPosition)
                Destroy(gameObject);
        }
    }

    private void MoveToTarget(Vector3 point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);
        SetRotationToTarget(point);
    }

    private void SetRotationToTarget(Vector2 targetPoint)
    {
        Vector2 lookDiraction = targetPoint - _rigidbody.position;
        float angle = Mathf.Atan2(lookDiraction.y, lookDiraction.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = angle + _baseAngel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void InitTarget(Enemy enemy, int damage)
    {
        _damage = damage;
        _target = enemy;
    }
}
