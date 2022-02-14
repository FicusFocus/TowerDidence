using UnityEngine;

public class ArchersTower : Tower
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _damage;

    private Enemy _target;
    private bool _enemyFinded;
    private float _timeAfterLastAtteck = 0;

    public int Damage => _damage;

    private void Start()
    {
        AudioSource.clip = _attackClip;
        AudioSource.loop = false;
        AudioSource.playOnAwake = false;
    }

    private void Update()
    {
        _timeAfterLastAtteck += Time.deltaTime;

        if (_target == null)
            return;

        if (_timeAfterLastAtteck >= _attackSpeed && _enemyFinded)
        {
            _timeAfterLastAtteck = 0;
            Attack(_target);
        }
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }

        var towerPosition = transform.position;
        var enemyPosition = _target.transform.position;

        if (Vector3.Distance(towerPosition, enemyPosition) > SearchArea)
        {
            _target = null;
            _enemyFinded = false;
        }
    }

    private void Attack(Enemy enemy)
    {
        var newArrow = Instantiate(_arrow, _shootPoint.position, Quaternion.identity);
        newArrow.InitTarget(enemy, Damage);
        AudioSource.Play();
    }
    
    private void FindTarget()
    {
        var hitList = Physics2D.OverlapCircleAll(transform.position, SearchArea);

        foreach (var collider in hitList)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                _target = enemy;
                _enemyFinded = true;
            }
        }
    }
}
