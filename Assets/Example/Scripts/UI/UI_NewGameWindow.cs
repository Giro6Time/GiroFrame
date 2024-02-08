using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GiroFrame;
using TMPro;
[UIElement(true, "UI/NewGameWindow", 1)]
public class UI_NewGameWindow : UI_WindowBase
{
    [SerializeField] private TMP_InputField UserName_InputField;
    [SerializeField] private TMP_Text UserName_Text;
    [SerializeField] private TMP_Text UserName_Placeholder_Text;
    [SerializeField] private Text UserName_Title_Text;

    [SerializeField] private Button Back_Button;
    [SerializeField] private Text Back_Text;

    [SerializeField] private Button Play_Button;
    [SerializeField] private Text Play_Text;
    private string LocalSetPackName = "UI_NewGameWindow";
    public override void Init()
    {
        base.Init();
        Back_Button.onClick.AddListener(OnCloseClick);
        Play_Button.onClick.AddListener(OnYesClick);
    }
    protected override void OnUpdateLanguage()
    {
        base.OnUpdateLanguage();
        UserName_Placeholder_Text.GiroLocalSet(LocalSetPackName, "UserName_Placeholder_Text");
        UserName_Title_Text.GiroLocalSet(LocalSetPackName, "UserName_Title_Text");
        Back_Text.GiroLocalSet(LocalSetPackName, "Back_Text");
        Play_Text.GiroLocalSet(LocalSetPackName, "Play_Text");

    }
    public override void Close()
    {
        UserName_InputField.text = "";
        base.Close();

    }
    public override void OnYesClick()
    { 
        if(UserName_InputField.text.Length <1)
        {
            UIManager.Instance.AddTipsByLocalization("CheckName");
        }
        else
        {
            AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
            EventManager.EventTrigger<string>("CreateNewSaveAndEnterGame", UserName_InputField.text);
            base.OnYesClick();
        }
    }

    public override void OnCloseClick()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        base.OnCloseClick();
    }
}
