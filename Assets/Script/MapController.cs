using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MapController : MonoBehaviour
{
    // eventSystem型の変数を宣言　
    private EventSystem eventSystem;
   
    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;


    // ボタンの名前を取得
    public void GetButtonName()
    {
        if(Map.gameResult == Map.GameResult.NotYet){
            //有効なイベントシステムを取得
            eventSystem=EventSystem.current;

            //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
            button_ob = eventSystem.currentSelectedGameObject;
            Map.GetButtonPosition(button_ob);
        }
    }    
    

}



//ーーーーーーーーーーーーーーーーーーーー
//場面の管理
//ーーーーーーーーーーーーーーーーーーーー

public static class Map
{
    public static map[,] GameMap;
    public static int[,] GamePoint;
    private static int size = 5;
    //ゲームリザルトを取得する
    public static GameResult gameResult = GameResult.NotYet;

    static Map()
    {
        Map.GameMap = new map[size, size];
        Map.GamePoint = new int[size, size];
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //ボタンの位置を取得する
    //ーーーーーーーーーーーーーーーーーーーー

    //数字からボタンの位置を取得する
    public static void GetButtonPosition(GameObject button)
    {
        int n = int.Parse(button.name.Replace("Button", ""));

        int x = (n-1)%size;
        int y = ((n-1)-x)/size;

        if(IsN(x, y)){
            button.GetComponent<Image>().color = new Color(1, 0.5f, 0.2f, 1);
            ChangeToA(x, y);
        }
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //ボタンがクリックされた時の処理
    //ーーーーーーーーーーーーーーーーーーーー

    // Change to A when Clicked
    public static void ChangeToA(int x, int y)
    {
        Map.GameMap[x, y] = map.A;
        ConnectEnemy.instance.AIEnemyTurn(Map.GameMap);

        //ゲームに勝利したか確認する
        CheckResult(Map.GameMap);
    }
    // Change to B when Clicked
    public static void ChangeToB(int x, int y)
    {
        Map.GameMap[x, y] = map.B;

        //ゲームに勝利したか確認する
        CheckResult(Map.GameMap);
    }

    //クリックされた場所がNかどうかを判定する
    public static bool IsN(int x, int y)
    {
        if(Map.GameMap[x, y] == map.N){
            return true;
        }else{
            return false;
        }
    }

    //ボタンの位置から数字を取得する
    public static int GetButtonNumber(int x, int y)
    {
        return x + y*size;
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //勝利条件の判定
    //ーーーーーーーーーーーーーーーーーーーー

    //勝利条件を満たしているかどうかを判定する
    public static void CheckResult(map[,] mapping)
    {
        //横の判定
        for(int i = 0; i < size; i++){
            if((mapping[i, 0] == mapping[i, 1] || mapping[i, 3] == mapping[i, 4]) && (mapping[i, 1] == mapping[i, 2] && mapping[i, 2] == mapping[i, 3])){
                if(mapping[i, 1] == map.A){
                    gameResult = GameResult.AWin;
                }else if(mapping[i, 1] == map.B){
                    gameResult = GameResult.BWin;
                }
            }
        }

        //縦の判定
        for(int i = 0; i < size; i++){
            if((mapping[0, i] == mapping[1, i] || mapping[3, i] == mapping[4, i]) && (mapping[1, i] == mapping[2, i] && mapping[2, i] == mapping[3, i])){
                if(mapping[1, i] == map.A){
                    gameResult = GameResult.AWin;
                }else if(mapping[1, i] == map.B){
                    gameResult = GameResult.BWin;
                }
            }
        }

        //斜めの判定
        if((mapping[0, 0] == mapping[1, 1] || mapping[3, 3] == mapping[4, 4]) && (mapping[1, 1] == mapping[2, 2] && mapping[2, 2] == mapping[3, 3])){
                if(mapping[1, 1] == map.A){
                    gameResult = GameResult.AWin;
                }else if(mapping[1, 1] == map.B){
                    gameResult = GameResult.BWin;
                }
            }
        if((mapping[0, 4] == mapping[1, 3] || mapping[3, 1] == mapping[4, 0]) && (mapping[1, 3] == mapping[2, 2] && mapping[2, 2] == mapping[3, 1])){
                if(mapping[1, 3] == map.A){
                    gameResult = GameResult.AWin;
                }else if(mapping[1, 3] == map.B){
                    gameResult = GameResult.BWin;
                }
            }

        //斜め４つ並びの判定
        if(mapping[1, 0] == mapping[2, 1] && mapping[2, 1] == mapping[3, 2] && mapping[3, 2] == mapping[4, 3]){
            if(mapping[1, 0] == map.A){
                gameResult = GameResult.AWin;
            }else if(mapping[1, 0] == map.B){
                gameResult = GameResult.BWin;
            }
        }
        if(mapping[0, 1] == mapping[1, 2] && mapping[1, 2] == mapping[2, 3] && mapping[2, 3] == mapping[3, 4]){
            if(mapping[0, 1] == map.A){
                gameResult = GameResult.AWin;
            }else if(mapping[0, 1] == map.B){
                gameResult = GameResult.BWin;
            }
        }
        if(mapping[0, 3] == mapping[1, 2] && mapping[1, 2] == mapping[2, 1] && mapping[2, 1] == mapping[3, 0]){
            if(mapping[0, 3] == map.A){
                gameResult = GameResult.AWin;
            }else if(mapping[0, 3] == map.B){
                gameResult = GameResult.BWin;
            }
        }
        if(mapping[1, 4] == mapping[2, 3] && mapping[2, 3] == mapping[3, 2] && mapping[3, 2] == mapping[4, 1]){
            if(mapping[1, 4] == map.A){
                gameResult = GameResult.AWin;
            }else if(mapping[1, 4] == map.B){
                gameResult = GameResult.BWin;
            }
        }
            
    }



    public enum GameResult{
        AWin, BWin, Draw, NotYet, End
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //マップの変数の定義
    //ーーーーーーーーーーーーーーーーーーーー
    public enum map{
        N, A, B
    }
}


