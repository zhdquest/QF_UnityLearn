using System;
using UIFrame;
using UnityEngine;

public class PlayerModuleController : UIControllerBase {
    
    public override void ControllerStart(UIModuleBase module)
    {
        base.ControllerStart(module);
        
        _module.FindCurrentModuleWidget(
            "PlayerInputField#")._InputField.onEndEdit.
            AddListener(OnPlayerInputField);
    }

    private void OnPlayerInputField(string text)
    {
        UILocalizationTextManager.GetInstance().LocalizationTextChange(1);
        
        int btnCount = 0;

        try
        {
            btnCount = int.Parse(text);
        }
        catch (Exception e)
        { 
            
        }

        if (btnCount < 0 || btnCount > 6)
            return;
        
        Transform tra = UIManager.GetInstance().FindWidget("MainPanel",
            "ButtomButtons*")._Transform;

        for (int i = 0; i < btnCount; i++)
        {
            tra.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = btnCount; i < 6; i++)
        {
            tra.GetChild(i).gameObject.SetActive(false);
        }
    }
}