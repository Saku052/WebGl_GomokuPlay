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

        for (int i = 0; i < 5; i++){
            for (int j = 0; j < 5; j++){
                if (map[i, j] == Map.map.N){
                    // AIのターンに色をおく
                    if(CEnemyAI.Is3SameColor(i, j, Map.map.B)) Map.GamePoint[i, j] = 200;
                    else if (CEnemyAI.Is3SameColor(i, j, Map.map.A)) Map.GamePoint[i, j] = 100;
                    else if(CEnemyAI.Is2SameColor(i, j, Map.map.B)) Map.GamePoint[i, j] = 20;
                    else if (CEnemyAI.Is2SameColor(i, j, Map.map.A)) Map.GamePoint[i, j] = 80;
                    else Map.GamePoint[i, j] = 10; 
                }else {
                    Map.GamePoint[i, j] = 0;
                }
            }
        }

        //GamePointが最も高い場所に色をおく
        int max = 0;
        int x = 0;
        int y = 0;
        for (int i = 0; i < 5; i++){
            for (int j = 0; j < 5; j++){
                if (Map.GamePoint[i, j] > max){
                    max = Map.GamePoint[i, j];
                    x = i;
                    y = j;
                }
            }
        }

        int n = 0;
            for (int i = 0; i < 5; i++){
                for (int j = 0; j < 5; j++){
                    if (map[i, j] == Map.map.A || map[i, j] == Map.map.B){
                        n++;
                    }
                }
            }

            if (n == 25){
                Map.gameResult = Map.GameResult.Draw;
                Debug.Log("Game Over");
            } else{
                Map.ChangeToB(x, y);
                mapPanel.transform.GetChild(Map.GetButtonNumber(x, y)).GetComponent<Image>().color = new Color(0.8f, 0.5f, 1, 1);
            }
    }



    // ランダムに色を変える
    public void RandomEnemyTurn(Map.map[,] map)
    {
        //マップの空いてる所をリストアップする
        bool isPlaced = true;

        while (isPlaced)
        {
            int a = Random.Range(0, 5);
            int b = Random.Range(0, 5);
            try
            {
                if (map[a, b] == Map.map.N)
                {

                    // AIのターンに色をおく
                    Map.ChangeToB(a, b);
                    mapPanel.transform.GetChild(Map.GetButtonNumber(a, b)).GetComponent<Image>().color = new Color(0.5f, 1, 0.8f, 1);

                    isPlaced = false;
                }
            }
            catch
            {

            }

            int n = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (map[i, j] == Map.map.A || map[i, j] == Map.map.B)
                    {
                        n++;
                    }
                }
            }

            if (n == 25)
            {
                isPlaced = false;
                Map.gameResult = Map.GameResult.Draw;
                Debug.Log("Game Over");
            }

        }
    }
}