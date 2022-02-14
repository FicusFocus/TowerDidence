using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PlaceForBuild : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite, _activeSprite;

    private SpriteRenderer _spriteRenderer;
    private Sprite _currentSprite;

    public event UnityAction DontActive;
    public event UnityAction<PlaceForBuild> PlaceChoosing;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetActive(_defaultSprite);
    }

    public void Reset()
    {
        SetActive(_defaultSprite);
    }

    private void SetActive(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _currentSprite = _spriteRenderer.sprite;
    }

    private void OnMouseDown()
    {
        if (_currentSprite == _defaultSprite)
        {
            SetActive(_activeSprite);
            PlaceChoosing?.Invoke(this);
        }
        else
        {
            DontActive?.Invoke();
            SetActive(_defaultSprite);
        }
    }
}
