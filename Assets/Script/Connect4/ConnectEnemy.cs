using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConnectEnemy : MonoBehaviour
{
    //インスタンス化
    public static ConnectEnemy instance;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject mapPanel;


    // c#内のAIを用いて色を変える
    public void AIEnemyTurn(Map.map[,] map)
    {
        //最も高いGamePointがいくつか保存しておく
        int max = 0;

        for (int i = 0; i < Map.size; i++){
            for (int j = 0; j < Map.size; j++){
                if (map[i, j] == Map.map.N){
                    // AIのターンに色をおく
                    if(CEnemyAI.Is3SameColor(i, j, Map.map.B)) Map.GamePoint[i, j] = 200;
                    else if (CEnemyAI.Is3SameColor(i, j, Map.map.A)) Map.GamePoint[i, j] = 100;
                    else if(CEnemyAI.Is2SameColor(i, j, Map.map.B)) Map.GamePoint[i, j] = 20;
                    else if (CEnemyAI.Is2SameColor(i, j, Map.map.A)) Map.GamePoint[i, j] = 80;
                    else Map.GamePoint[i, j] = 10; 
                    max = Mathf.Max(max, Map.GamePoint[i, j]);
                }else {
                    Map.GamePoint[i, j] = 0;
                }
            }
        }

        int n = 0;
        List<int> x = new List<int>();
        List<int> y = new List<int>();

        
        for (int i = 0; i < Map.size; i++){
            for (int j = 0; j < Map.size; j++){

                //GamePointが最大の場所をリストアップする
                if (Map.GamePoint[i, j] == max){
                    x.Add(i);
                    y.Add(j);
                }

                //マップの位置が空いているかどうかを判定する
                if (map[i, j] == Map.map.A || map[i, j] == Map.map.B){
                    n++;
                }
            }
        }

            //マップが埋まったらゲームオーバー
            if (n == 25){
                Map.gameResult = Map.GameResult.Draw;
                Debug.Log("Game Over");
            } else{
                //複数ある場合はその中からランダム
                int a = Random.Range(0, x.Count);
                Map.ChangeToB(x[a], y[a]);

                //タイルの色を変える
                mapPanel.transform.GetChild(Map.GetButtonNumber(x[a], y[a])).GetComponent<Image>().color =
                new Color(0.92f, 0.36f, 0.21f, 0.80f); // ピンク色に変える
            }
    }
}