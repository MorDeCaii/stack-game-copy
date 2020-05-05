using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class UIStartScreen : MonoBehaviour, IScreen
{
    public RectTransform title;
    public RectTransform subTitle;
    public RectTransform tip;

    public UnityEvent onShowComplete;
    public UnityEvent onHideComplete;

    private TextMeshProUGUI titleTM;
    private TextMeshProUGUI subTitleTM;
    private TextMeshProUGUI tipTM;

    private readonly Color showTextColor = new Color(1, 1, 1, 1);
    private readonly Color hideTextColor = new Color(1, 1, 1, 0);

    private readonly int titleOriginY = -186;
    private readonly int titleShowY = -80;
    private readonly int titleHideY = -10;

    private readonly int subTitleOriginY = -230;
    private readonly int subTitleShowY = -124;
    private readonly int subTitleHideY = -54;

    void Awake()
    {
        titleTM = title.GetComponent<TextMeshProUGUI>();
        subTitleTM = subTitle.GetComponent<TextMeshProUGUI>();
        tipTM = tip.GetComponent<TextMeshProUGUI>();

        title.anchoredPosition = new Vector2(title.anchoredPosition.x, titleOriginY);
        subTitle.anchoredPosition = new Vector2(subTitle.anchoredPosition.x, subTitleOriginY);

        titleTM.color = hideTextColor;
        subTitleTM.color = hideTextColor;
        tipTM.color = hideTextColor;
    }

    public void Show()
    {
        title.DOAnchorPos(new Vector2(title.anchoredPosition.x, titleShowY), 0.6f);
        titleTM.DOColor(showTextColor, 0.6f);

        subTitle.DOAnchorPos(new Vector2(subTitle.anchoredPosition.x, subTitleShowY), 0.6f);
        subTitleTM.DOColor(showTextColor, 0.6f).OnComplete(() =>
        {
            tipTM.DOColor(showTextColor, 0.4f).OnComplete(() =>
            {
                GameManager.UnblockInput();
                onShowComplete.Invoke();
            });
        });
    }

    public void Hide()
    {
        GameManager.BlockInput();
        title.DOAnchorPos(new Vector2(title.anchoredPosition.x, titleHideY), 0.4f);
        titleTM.DOColor(hideTextColor, 0.4f);
        subTitle.DOAnchorPos(new Vector2(subTitle.anchoredPosition.x, subTitleHideY), 0.4f);
        subTitleTM.DOColor(hideTextColor, 0.4f);
        tipTM.DOColor(hideTextColor, 0.4f).OnComplete(() =>
        {
            subTitle.anchoredPosition = new Vector2(subTitle.anchoredPosition.x, subTitleOriginY);
            title.anchoredPosition = new Vector2(title.anchoredPosition.x, titleOriginY);
            onHideComplete.Invoke();
        });
    }
}
