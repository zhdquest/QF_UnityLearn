using System;
using UnityEngine;
using UnityEngine.UI;

public interface IText
{
    Text _Text { get; }

    void SetTextText(string text);
    string GetTextText();
}

public interface IButton
{
    Button _Button { get; }

    void AddButtonOnClickListener(Action action);
}

public interface IImage
{
    Image _Image { get; }

    void SetImageSprite(Sprite sprite);
    void SetImageColor(Color color);
    void SetImageFillAmount(float fillAmount);
    
}







