using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GiroFrame;
/// <summary>
/// 菜单场景的总管理器
/// 负责调度整个场景的逻辑、流程
/// </summary>
public class MainMenuManager : LogicManagerBase<MainMenuManager>
{
    private void Start()
    {
        //播放背景音乐
        AudioManager.Instance.PlayBGAudio("Audio/BG/Menu");
        UIManager.Instance.Show<UI_MainMenuMainWindow>();
        RegisterEventListener();    
    }
    protected override void RegisterEventListener()
    {
        EventManager.AddEventListener<string>("CreateNewSaveAndEnterGame", CreateNewSaveAndEnterGame);
    }
    
    protected override void CancelEventListener()
    {
        EventManager.RemoveEventListener<string>("CreateNewSaveAndEnterGame", CreateNewSaveAndEnterGame);
    }
    private void CreateNewSaveAndEnterGame(string userName)
    {
        //建立存档
        SaveItem saveitem = SaveManager.CreateSaveItem();
        //TODO:创建首次存档时用户数据
        Debug.Log("创建存档");
        //进入游戏
        EnterGame(saveitem);
    }
    private void EnterGame(SaveItem saveitem)
    {
        //TODO:交给GameManager实现
        Debug.Log("进入游戏");
        SceneManager.LoadScene("Game");
    }
}
