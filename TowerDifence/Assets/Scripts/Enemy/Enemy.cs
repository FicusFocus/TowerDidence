using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _healthPoint, _revard;
    [SerializeField] private float _baseMoveSpeed;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private GameObject _helthBar;
    [SerializeField] private AudioClip _damageClip, _dieClip;
 
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private List<Transform> _path;
    private Vector3 _targetPoint;
    private Color _baseColor;
    private int _currentHealthPoint;
    private float _currentMoveSpeed;
    private float _minMoveSpeed = 0.01f;

    public int Revard => _revard;
    public Color BaceColor => _baseColor;
    public float CurrentMoveSpeed => _currentMoveSpeed;
    public float BaseMoveSpeed => _baseMoveSpeed;

    public event UnityAction<Enemy> Died;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.playOnAwake = false;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _currentHealthPoint = _healthPoint;
        _currentMoveSpeed = _baseMoveSpeed;
        _baseColor = _spriteRenderer.color;
    }

    private void Update()
    {
        if (transform.position != _targetPoint)
            Move();
        else
            _targetPoint = FindNextTargetPoint(_targetPoint);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _currentMoveSpeed * Time.deltaTime);
    }

    private Vector3 FindNextTargetPoint(Vector3 point)
    {
        int currentPointIndex = 0;

        for (int i = 0; i < _path.Count; i++)
        {
            if (point == _path[i].position)
                currentPointIndex = i;
        }

        if (_path.Count - 1 <= currentPointIndex)
            return point;
        else
        {
            SetRotationToTarget(_path[currentPointIndex + 1].position);
            return _path[currentPointIndex + 1].position;
        }
    }

    private void SetRotationToTarget(Vector2 targetPoint)
    {
        Vector2 lookDiraction = targetPoint - _rigidbody.position;
        float angle = Mathf.Atan2(lookDiraction.y, lookDiraction.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = angle;
    }

    private void Die()
    {
        _audioSource.clip = _dieClip;
        _audioSource.Play();
        Destroy(gameObject, _dieClip.length);
    }

    public void TakeDamage(int damage)
    {
        _currentHealthPoint -= damage;
        _audioSource.clip = _damageClip;
        _audioSource.Play();
        
        if (_currentHealthPoint <= 0)
        {
            Died?.Invoke(this);
            Die();
            return;
        }

        _hitEffect.Play();
        _helthBar.transform.localScale = new Vector3((float)_currentHealthPoint / (float)_healthPoint, 0.5f, 0);
    }

    public void InitPath(List<Transform> path)
    {
        _path = path;
        _targetPoint = path[0].position;
    }

    public void SetMovespeed(float newMoveSpeed)
    {
        if (newMoveSpeed < _minMoveSpeed)
            return;

        _currentMoveSpeed = newMoveSpeed;
    }

    public void SetNewColor(Color newColor)
    {
        _spriteRenderer.color = newColor;
    }

    public void SetAudioSourseVolumeValue(float value)
    {
        _audioSource.volume = value;
    }
}
