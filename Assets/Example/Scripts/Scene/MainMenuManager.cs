using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GiroFrame;
/// <summary>
/// �˵��������ܹ�����
/// ������������������߼�������
/// </summary>
public class MainMenuManager : LogicManagerBase<MainMenuManager>
{
    private void Start()
    {
        //���ű�������
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
        //�����浵
        SaveItem saveitem = SaveManager.CreateSaveItem();
        //TODO:�����״δ浵ʱ�û�����
        Debug.Log("�����浵");
        //������Ϸ
        EnterGame(saveitem);
    }
    private void EnterGame(SaveItem saveitem)
    {
        //TODO:����GameManagerʵ��
        Debug.Log("������Ϸ");
        SceneManager.LoadScene("Game");
    }
}
