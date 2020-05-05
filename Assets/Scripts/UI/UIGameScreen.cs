using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class UIGameScreen : MonoBehaviour, IScreen
{
    public RectTransform score;

    public UnityEvent onShowComplete;
    public UnityEvent onHideComplete;

    private TextMeshProUGUI scoreTM;
    private bool isVisible;

    private readonly Color showTextColor = new Color(1, 1, 1, 1);
    private readonly Color hideTextColor = new Color(1, 1, 1, 0);

    private readonly int scoreOriginY = -75;
    private readonly int scoreHideY = -25;

    void Awake()
    {
        scoreTM = score.GetComponent<TextMeshProUGUI>();
        score.anchoredPosition = new Vector2(score.anchoredPosition.x, scoreOriginY);
        scoreTM.color = hideTextColor;
        isVisible = false;
    }

    void Update()
    {
        if (isVisible && GameManager.score > 0) scoreTM.text = GameManager.score.ToString();
    }

    public void Show()
    {
        scoreTM.text = "";
        isVisible = true;
        GameManager.UnblockInput();
        scoreTM.DOColor(showTextColor, 1f).OnComplete(() =>
        {
            onShowComplete.Invoke();
        });
    }

    public void Hide()
    {
        GameManager.BlockInput();
        isVisible = false;
        score.DOAnchorPos(new Vector2(score.anchoredPosition.x, scoreHideY), 0.4f);
        scoreTM.DOColor(hideTextColor, 0.4f).OnComplete(() =>
        {
            score.anchoredPosition = new Vector2(score.anchoredPosition.x, scoreOriginY);
            onHideComplete.Invoke();
        });
    }
}
