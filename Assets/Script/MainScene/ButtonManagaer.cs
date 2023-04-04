using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagaer : MonoBehaviour
{
    
    //ーーーーーーーーーーーーーーーーーーー
    //MainPanelのボタン
    //ーーーーーーーーーーーーーーーーーーー

    //バトル開始ボタン
    public void StartBattle()
    {
        //シーンの読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene("Connect4");
    }

    //チーム編成ボタン
    public void TeamFormation()
    {
    
        //チーム編成画面を表示
        teamformation.SetBool("OpenTeam", true);
    }

    [SerializeField] private Animator teamformation;
}
