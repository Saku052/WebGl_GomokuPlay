using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBattle : MonoBehaviour
{

    //相手に攻撃をする
    public void Attack()
    {
        //相手のHPを減らす
        if(RPGSlider.instance.GetTurn(0) == GetStatus.MainPlayer.Name){
            BattleTurn.instance.TurnAttack(GetStatus.Enemy.Name, Player1Atk(), BattleTurn.BattleState.Pl1Attack);

        }
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //攻撃メソッド
    //ーーーーーーーーーーーーーーーーーーーー

    public static int Player2Atk()
    {
        int damage;

        damage = 
        GetStatus.MainPlayer.Attacked(GetStatus.Enemy.Atk, GetStatus.Enemy.CritRate, GetStatus.Enemy.CritDamage);

        //ステータスを更新する
        StatusShow();
        NextTurn(GetStatus.MainPlayer.Speed, GetStatus.Enemy.Speed);

        return damage;
    }

    public static int Player1Atk()
    {
        int damage;
        //相手のHPを減らす
        damage = 
        GetStatus.Enemy.Attacked(GetStatus.MainPlayer.Atk, GetStatus.MainPlayer.CritRate, GetStatus.MainPlayer.CritDamage);
        
        //ステータスを更新する
        StatusShow();
        NextTurn(GetStatus.MainPlayer.Speed, GetStatus.Enemy.Speed);

        return damage;
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //ターンのテキスト管理メソッド
    //ーーーーーーーーーーーーーーーーーーーー


    private void Start() {

        //ステータスの初期化
        StatusShow();

        //名前を変更する
        RPGSlider.instance.SetPlayer1Name(GetStatus.MainPlayer.Name);
        RPGSlider.instance.SetPlayer2Name(GetStatus.Enemy.Name);

        //イラストを変更する
        RPGSlider.instance.SetPlayer1Image(Resources.Load<Sprite>("Char/" + GetStatus.MainPlayer.CharCode.ToString() + "_Photo"));
        RPGSlider.instance.SetPlayer2Image(Resources.Load<Sprite>("Char/" + GetStatus.Enemy.CharCode.ToString() + "_Photo"));

        //ターンを設定する
        TurnInit(GetStatus.MainPlayer.Speed, GetStatus.Enemy.Speed);

        //ボタンの非表示
        BattleTurn.instance.NextButtonPanel.SetActive(false);

        //最初のターンを決定する
        if(RPGSlider.instance.GetTurn(0) == GetStatus.Enemy.Name)
            BattleTurn.instance.TurnStart(GetStatus.Enemy.Name, BattleTurn.BattleState.Pl2Start);
        else if (RPGSlider.instance.GetTurn(0) == GetStatus.MainPlayer.Name) 
            BattleTurn.instance.TurnStart(GetStatus.MainPlayer.Name, BattleTurn.BattleState.Pl1Start);
    }


    //ーーーーーーーーーーーーーーーーーーーー
    //ステータスの初期化
    //ーーーーーーーーーーーーーーーーーーーー
    public static void StatusShow()
    {
        //スライダーを動かす
        RPGSlider.instance.SetPlayer1AtkSlider( (float)GetStatus.MainPlayer.Atk / (float)GetStatus.MainPlayer.MaxAtk);
        RPGSlider.instance.SetPlayer1HpSlider( (float)GetStatus.MainPlayer.Hp / (float)GetStatus.MainPlayer.MaxHp);
        RPGSlider.instance.SetPlayer2AtkSlider( (float)GetStatus.Enemy.Atk / (float)GetStatus.Enemy.MaxAtk);
        RPGSlider.instance.SetPlayer2HpSlider( (float)GetStatus.Enemy.Hp / (float)GetStatus.Enemy.MaxHp);

        //テキストを変更する
        RPGSlider.instance.SetPlayer1AtkText(GetStatus.MainPlayer.Atk, GetStatus.MainPlayer.MaxAtk);
        RPGSlider.instance.SetPlayer1HpText(GetStatus.MainPlayer.Hp, GetStatus.MainPlayer.MaxHp);
        RPGSlider.instance.SetPlayer2AtkText(GetStatus.Enemy.Atk, GetStatus.Enemy.MaxAtk);
        RPGSlider.instance.SetPlayer2HpText(GetStatus.Enemy.Hp, GetStatus.Enemy.MaxHp);
    }

    public void TurnInit(int pl1Prob, int pl2Prob)
    {
        //スピードを元にターンを決める
        float pl1Speed = (float)pl1Prob / (float)(pl1Prob + pl2Prob);

        for(int i = 0; i < 5; i++){
            //誰のターンなのか決定する
            if (Random.Range(0f, 1f) <= pl1Speed)
            {
                //プレイヤーのターン
                RPGSlider.instance.SetTurn(GetStatus.MainPlayer.Name, i);
            } else {
                //相手のターン
                RPGSlider.instance.SetTurn(GetStatus.Enemy.Name, i);
            }
        }

    }

    public static void NextTurn(int pl1Prob, int pl2Prob)
    {
        //ターンを進める
        for(int i = 0; i < 4; i++){
            RPGSlider.instance.SetTurn(RPGSlider.instance.GetTurn(i+1), i);
        }

        //スピードを元にターンを決める
        float pl1Speed = (float)pl1Prob / (float)(pl1Prob + pl2Prob);
        
        //誰のターンなのか決定する
        if (Random.Range(0f, 1f) <= pl1Speed)
        {
            //プレイヤーのターン
            RPGSlider.instance.SetTurn(GetStatus.MainPlayer.Name, 4);
        } else {
            //相手のターン
            RPGSlider.instance.SetTurn(GetStatus.Enemy.Name, 4);
        }
    }

}
