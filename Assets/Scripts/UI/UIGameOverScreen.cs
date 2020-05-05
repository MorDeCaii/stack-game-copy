using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class UIGameOverScreen : MonoBehaviour
{
    public RectTransform gameOver;

    public UnityEvent onShowComplete;
    public UnityEvent onHideComplete;

    private TextMeshProUGUI gameOverTM;

    private readonly Color showTextColor = new Color(1, 1, 1, 1);
    private readonly Color hideTextColor = new Color(1, 1, 1, 0);

    private readonly int gameOverOriginY = -210;
    private readonly int gameOverShowY = -150;
    private readonly int gameOverHideY = -100;

    void Awake()
    {
        gameOverTM = gameOver.GetComponent<TextMeshProUGUI>();
        gameOver.anchoredPosition = new Vector2(gameOver.anchoredPosition.x, gameOverOriginY);
        gameOverTM.color = hideTextColor;
    }

    public void Show()
    {
        gameOver.DOAnchorPos(new Vector2(gameOver.anchoredPosition.x, gameOverShowY), 1f);
        gameOverTM.DOColor(showTextColor, 1f).OnComplete(() =>
        {
            onShowComplete.Invoke();
            GameManager.UnblockInput();
        });
    }

    public void Hide()
    {
        GameManager.BlockInput();
        gameOverTM.DOColor(hideTextColor, 0.4f).OnComplete(() =>
        {
            gameOver.anchoredPosition = new Vector2(gameOver.anchoredPosition.x, gameOverOriginY);
            onHideComplete.Invoke();
        });
    }
}
