using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatusInit : MonoBehaviour
{

    void Start()
    {
        
        //メインプレイヤーのステータスだけ表示する
        ShowPlayer1(GetStatus.MainPlayer);   
    }




    //画面にステータス情報を表示する
    public void ShowStatus(Status player1, Status player2)
    {
        //スライダーを動かす
        MapSlider.instance.SetPlayer1AtkSlider( (float)player1.Atk / (float)player1.MaxAtk);
        MapSlider.instance.SetPlayer1HpSlider( (float)player1.Hp / (float)player1.MaxHp);
        MapSlider.instance.SetPlayer1ExpSlider( (float)player1.Exp / (float)player1.MaxExp);

        MapSlider.instance.SetPlayer2AtkSlider( (float)player2.Atk / (float)player2.MaxAtk);
        MapSlider.instance.SetPlayer2HpSlider( (float)player2.Hp / (float)player2.MaxHp);
        MapSlider.instance.SetPlayer2ExpSlider( (float)player2.Exp / (float)player2.MaxExp);

        //ステータスのテキストを表示
        MapSlider.instance.SetPlayer1AtkText(player1.Atk, player1.MaxAtk);
        MapSlider.instance.SetPlayer1HpText(player1.Hp, player1.MaxHp);
        MapSlider.instance.SetPlayer1LevText(player1.Level);

        MapSlider.instance.SetPlayer2AtkText(player2.Atk, player2.MaxAtk);
        MapSlider.instance.SetPlayer2HpText(player2.Hp, player2.MaxHp);
        MapSlider.instance.SetPlayer2LevText(player2.Level);

        //名前を表示
        MapSlider.instance.SetPlayer1NameText(player1.Name);
        MapSlider.instance.SetPlayer2NameText(player2.Name);
    }

    //メインプレイヤーのステータスだけ表示する
    private void ShowPlayer1(Status player1)
    {
        //スライダーを動かす
        MapSlider.instance.SetPlayer1AtkSlider( (float)player1.Atk / (float)player1.MaxAtk);
        MapSlider.instance.SetPlayer1HpSlider( (float)player1.Hp / (float)player1.MaxHp);
        MapSlider.instance.SetPlayer1ExpSlider( (float)player1.Exp / (float)player1.MaxExp);

        //ステータスのテキストを表示
        MapSlider.instance.SetPlayer1AtkText(player1.Atk, player1.MaxAtk);
        MapSlider.instance.SetPlayer1HpText(player1.Hp, player1.MaxHp);
        MapSlider.instance.SetPlayer1LevText(player1.Level);

        //名前を表示
        MapSlider.instance.SetPlayer1NameText(player1.Name);

        //画像を表示
        MapSlider.instance.SetPlayer1Image(Resources.Load<Sprite>("Char/" + player1.CharCode.ToString() + "_Card"));
    }


    //インスタンス化
    public static StatusInit instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
}

    