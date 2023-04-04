using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurn : MonoBehaviour
{
    //ターンのテキスト
    [SerializeField] private Text turnText;

    //全体に出てくるボタン
    [SerializeField] public GameObject NextButtonPanel;

    //バトルの状態
    public BattleState battleState;

    public void TurnStart(string name, BattleState state){
        battleState = state;

        if(state == BattleState.Pl2Start){

            NextButtonPanel.SetActive(true);
        }
            
        turnText.text = name + "のターン";
    }

    public void TurnAttack(string name, int damage, BattleState state){
        battleState = state;
        NextButtonPanel.SetActive(true);

        turnText.text = name + "に" + damage + "のダメージ";
    }

    public void GameWin(string name){
        NextButtonPanel.SetActive(true);
        turnText.text = name + "の勝利";
    }


    //ボタンの利用
    public void NextButton(){
        switch (battleState){
            case BattleState.Pl2Start:
                TurnAttack(GetStatus.MainPlayer.Name, RPGBattle.Player2Atk(), BattleState.Pl2Attack);
                battleState = BattleState.Pl2Attack;
                break;
            case BattleState.Pl2Attack:
                battleState = BattleState.None;
                NextButtonPanel.SetActive(false);

                //自分のHPが0以下になったら敗北
                if (GetStatus.MainPlayer.Hp <= 0)
                {
                    NextButtonPanel.SetActive(true);
                    GameWin(GetStatus.Enemy.Name);
                    GetStatus.MainPlayer.Hp = 0;
                    battleState = BattleState.Pl2Win;
                } else {

                    if(RPGSlider.instance.GetTurn(0) == GetStatus.Enemy.Name)
                    BattleTurn.instance.TurnStart(GetStatus.Enemy.Name, BattleTurn.BattleState.Pl2Start);
                    else if (RPGSlider.instance.GetTurn(0) == GetStatus.MainPlayer.Name) 
                    BattleTurn.instance.TurnStart(GetStatus.MainPlayer.Name, BattleTurn.BattleState.Pl1Start);
                }

                break;
            case BattleState.Pl1Attack:
                battleState = BattleState.Pl1Start;
                NextButtonPanel.SetActive(false);

                //相手のHPが0以下になったら勝利
                if (GetStatus.Enemy.Hp <= 0)
                {
                    NextButtonPanel.SetActive(true);
                    GameWin(GetStatus.MainPlayer.Name);
                    GetStatus.Enemy.Hp = 0;
                    battleState = BattleState.Pl1Win;
                    
                } else {

                    if(RPGSlider.instance.GetTurn(0) == GetStatus.Enemy.Name)
                    BattleTurn.instance.TurnStart(GetStatus.Enemy.Name, BattleTurn.BattleState.Pl2Start);
                    else if (RPGSlider.instance.GetTurn(0) == GetStatus.MainPlayer.Name) 
                    BattleTurn.instance.TurnStart(GetStatus.MainPlayer.Name, BattleTurn.BattleState.Pl1Start);
                }

                break;
            case BattleState.Pl1Win:
            
                Result.instance.Win();
                break;
            case BattleState.Pl2Win:

                Result.instance.Lose();
                break;
        }
    }


    //バトルの状態を管理する
    public enum BattleState{
        Pl1Start,
        Pl2Start,
        Pl1Attack,
        Pl2Attack,
        Pl1Win,
        Pl2Win,
        None
    }

    //インスタンス化
    public static BattleTurn instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
}
