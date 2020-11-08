using UIFrame;
//using DG.Tweening;
using UnityEngine;

public class TaskModule : UIModuleBase
{
    private RectTransform _rectTransform;

    protected override void Awake()
    {
        base.Awake();
        _rectTransform = GetComponent<RectTransform>();
    }
    protected void Start()
    {
        BindController(new TaskModuleController());
        _rectTransform.anchoredPosition = Vector2.right * -650;
    }

    public override void OnOpen()
    {
        base.OnOpen();
        //_canvasGroup.DOFade(1, 3f);
        //_rectTransform.DOAnchorPosX(0, 2f);
    }

    public override void OnClose()
    {
        base.OnClose();
        //_canvasGroup.DOFade(0, 1f);
        //_rectTransform.DOAnchorPosX(-650, 2f);
    }
}