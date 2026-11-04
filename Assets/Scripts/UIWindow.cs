using DG.Tweening;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private string _id;

    [Header("UI Settings")]
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _hideOnStart;

    [Header("Animation Settings")]
    [SerializeField] private float showDuration = 0.5f;
    [SerializeField] private float hideDuration = 0.5f;

    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;

    public CanvasGroup CanvasGroup => _canvasGroup;
    public RectTransform CanvasRectTransform => _canvasRectTransform;
    public string Id => _id;

    private Tween _currentTween;

    void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (_hideOnStart)
        {
            Hide(true);
        }
    }

    public virtual void Show(bool instant = false)
    {
        _currentTween?.Kill();
        _canvasRectTransform.gameObject.SetActive(true);

        if (instant)
        {
            _canvasRectTransform.localScale = Vector3.one;
            if (_canvasGroup != null) _canvasGroup.alpha = 1f;
        }
        else
        {
          
            _canvasRectTransform.localScale = Vector3.zero;
            if (_canvasGroup != null) _canvasGroup.alpha = 0f;

         
            Sequence seq = DOTween.Sequence();
            seq.Join(_canvasRectTransform.DOScale(Vector3.one, showDuration).SetEase(showEase));
            if (_canvasGroup != null)
            {
                seq.Join(_canvasGroup.DOFade(1f, showDuration));
            }
            _currentTween = seq;
        }
    }

    public virtual void Hide(bool instant = false)
    {
        _currentTween?.Kill();

        if (instant)
        {
            _canvasRectTransform.localScale = Vector3.zero;
            if (_canvasGroup != null) _canvasGroup.alpha = 0f;
            _canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            Sequence seq = DOTween.Sequence();
            seq.Join(_canvasRectTransform.DOScale(Vector3.zero, hideDuration).SetEase(hideEase));
            if (_canvasGroup != null)
            {
                seq.Join(_canvasGroup.DOFade(0f, hideDuration));
            }

            seq.OnComplete(() =>
            {
                _canvasRectTransform.gameObject.SetActive(false);
            });

            _currentTween = seq;
        }
    }
}