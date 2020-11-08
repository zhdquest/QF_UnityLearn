using UnityEngine;
using UIFrame;
using System;

public class DemoWidget : UIWidgetBase, IText,IImage,IButton
{
    public void SetTextText(string text)
    {
        throw new NotImplementedException();
    }

    public string GetTextText()
    {
        throw new NotImplementedException();
    }

    public void SetImageSprite(Sprite sprite)
    {
        throw new NotImplementedException();
    }

    public void SetImageColor(Color color)
    {
        throw new NotImplementedException();
    }

    public void SetImageFillAmount(float fillAmount)
    {
        throw new NotImplementedException();
    }

    public void AddButtonOnClickListener(Action action)
    {
        throw new NotImplementedException();
    }
}