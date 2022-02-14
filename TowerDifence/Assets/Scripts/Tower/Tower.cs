using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private GameObject _attackRadius;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] protected float SearchArea;
    [SerializeField] protected AudioSource AudioSource;
    [SerializeField] private string _description;

    private bool _isAttackRadiusVisable = false;
    protected float LostArea;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Price => _price;
    public float SerchArea => SearchArea;
    public string Description => _description;

    private void Start()
    {
        _attackRadius.transform.localScale = new Vector3(SearchArea, SearchArea, SearchArea);
        SetRadiusViasble(false);
        LostArea = SerchArea + 0.3f;
    }

    private void OnMouseDown()
    {
        if (_isAttackRadiusVisable)
            SetRadiusViasble(false);
        else
            SetRadiusViasble(true);
    }

    private void SetRadiusViasble(bool value)
    {
        _attackRadius.SetActive(value);
        _isAttackRadiusVisable = value;
    }

    public void SetAudioSourceValue(float value)
    {
        AudioSource.volume = value;
    }
}
