using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject mapPanel;

    // eventSystem型の変数を宣言　
    private EventSystem eventSystem;
   
    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;


    // ボタンの名前を取得
    public void GetButtonName()
    {
        if(Map.gameResult == Map.GameResult.Connect){
            //有効なイベントシステムを取得
            eventSystem=EventSystem.current;

            //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
            button_ob = eventSystem.currentSelectedGameObject;
            Map.GetButtonPosition(button_ob);
        }
    }    

    //シーン移行した時にMapを表示する
    private void Start()
    {
        //場面の色を変更する
        Map.ShowMap(mapPanel);

        //敵の位置を設定する
        if(Map.EnemyMap == null)    Map.SetEnemyPosition();
    }
    

}



//ーーーーーーーーーーーーーーーーーーーー
//場面の管理
//ーーーーーーーーーーーーーーーーーーーー

public static class Map
{
    public static map[,] GameMap;
    public static int[,] GamePoint;
    public static Enemyrank[,] EnemyMap;
    public static int size = 5;
    //ゲームリザルトを取得する
    public static GameResult gameResult = GameResult.Connect;

    static Map()
    {
        Map.GameMap = new map[size, size];
        Map.GamePoint = new int[size, size];
    }

    //Mapを表示する
    public static void ShowMap(GameObject mapPanel)
    {
        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                if(GameMap[i, j] == map.A){
                    mapPanel.transform.GetChild(Map.GetButtonNumber(i, j)).GetComponent<Image>().color =
                    new Color(0.26f, 0.80f, 0.90f, 0.80f); //青色に変える

                }else if(GameMap[i, j] == map.B){
                    mapPanel.transform.GetChild(Map.GetButtonNumber(i, j)).GetComponent<Image>().color =
                    new Color(0.92f, 0.36f, 0.21f, 0.80f); // ピンク色に変える
                }
            }
        }
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //ボタンの位置を取得する
    //ーーーーーーーーーーーーーーーーーーーー

    //数字からボタンの位置を取得する
    public static void GetButtonPosition(GameObject button)
    {
        //ボタンの位置を取得する
        int n = int.Parse(button.name.Replace("Button", ""));


        int x = (n-1)%size;
        int y = ((n-1)-x)/size;

        if(IsN(x, y)){  //選択可能な場面だった場合

            //ステータス等を表示する
            //Level, MaxExp, HP, ATK, DEF, Name, CR, CD
            if(Map.EnemyMap[x, y] == Enemyrank.Rank1){
                //イラストの変更
                MapSlider.instance.SetPlayer2Image(Resources.Load<Sprite>("Char/" + GetStatus.EnemyRank1.CharCode.ToString() + "_Card"));

                //ステータスの変更
                GetStatus.Enemy = new Status(GetStatus.EnemyRank1);
                StatusInit.instance.ShowStatus(GetStatus.MainPlayer, GetStatus.Enemy);
            } else if(Map.EnemyMap[x, y] == Enemyrank.Rank2){
                //イラストの変更
                MapSlider.instance.SetPlayer2Image(Resources.Load<Sprite>("Char/" + GetStatus.EnemyRank2.CharCode.ToString() + "_Card"));
                
                //ステータスの変更
                GetStatus.Enemy = new Status(GetStatus.EnemyRank2);
                StatusInit.instance.ShowStatus(GetStatus.MainPlayer, GetStatus.Enemy);
            } else if(Map.EnemyMap[x, y] == Enemyrank.Rank3){
                //イラストの変更
                MapSlider.instance.SetPlayer2Image(Resources.Load<Sprite>("Char/" + GetStatus.EnemyRank3.CharCode.ToString() + "_Card"));

                //ステータスの変更
                GetStatus.Enemy = new Status(GetStatus.EnemyRank3);
                StatusInit.instance.ShowStatus(GetStatus.MainPlayer, GetStatus.Enemy);
            }

            //選択した場所を表示する
            ActiveBattle.Instance.SetSelected(x, y);
        }
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //ボタンがクリックされた時の処理
    //ーーーーーーーーーーーーーーーーーーーー

    // Change to A when Clicked
    public static void ChangeToA(int x, int y)
    {
        Map.GameMap[x, y] = map.A;
 
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
    //選択した場所のマップの位置を判定する
    //ーーーーーーーーーーーーーーーーーーーー


    //敵の位置を指定する
    public static void SetEnemyPosition()
    {
        Map.EnemyMap = new Enemyrank[size, size];
        
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                    //乱数を設定する
                    int rand = Random.Range(0, 100);

                //敵の出現ランクをランダムで設定する
                if((y == 0 || y == 4 || x == 0 || x == 4) && GameMap[x, y] == map.N)
                {
                    if(rand < 90)   EnemyMap[x, y] = Enemyrank.Rank1;
                    else            EnemyMap[x, y] = Enemyrank.Rank2;
                }
                else if((y == 1 || y == 3 || x == 1 || x == 3) && GameMap[x, y] == map.N)
                {
                    if(rand < 50)       EnemyMap[x, y] = Enemyrank.Rank1;
                    else if(rand < 90)  EnemyMap[x, y] = Enemyrank.Rank2;
                    else                EnemyMap[x, y] = Enemyrank.Rank3;
                }
                else if (GameMap[x,y] == map.N)
                {
                    if(rand < 30)       EnemyMap[x, y] = Enemyrank.Rank1;
                    else if(rand < 70)  EnemyMap[x, y] = Enemyrank.Rank2;
                    else                EnemyMap[x, y] = Enemyrank.Rank3; 
                }       
            }
        }
        
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

    public enum Enemyrank{
        Rank1, Rank2, Rank3
    }

    public enum GameResult{
        AWin, BWin, Draw, Connect, End, RpgGame
    }

    //ーーーーーーーーーーーーーーーーーーーー
    //マップの変数の定義
    //ーーーーーーーーーーーーーーーーーーーー
    public enum map{
        N, A, B
    }
}


