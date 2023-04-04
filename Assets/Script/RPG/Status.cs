using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステータス親クラス
public class Status
{

    
    //ーーーーーーーーーーーーーーーーーーーー
    //レベルアップ
    //ーーーーーーーーーーーーーーーーーーーー

    public void LevelUp()
    {   
        if(Level < MaxLevel)
        {
            //レベルアップ時固定値のステータス
            Level++;
            MaxHp += 5;
            MaxAtk += 2;
            MaxDef += 2;
            CritDamage += 0.1f;
            
            //レベルアップ時レベルを変数とした関数を用いるステータス
            MaxExp += MaxExpFun(Level);
            MaxSpeed += MaxSpeedFun(Level);
            CritRate += CritRateFun(Level);
        }

        //レベルアップ時にHP等を全てMaxにリセットする
        Hp = MaxHp;
        Atk = MaxAtk;
        Def = MaxDef;
        Speed = MaxSpeed;
    }

    //レベルを減らす
    public void SubLevel(int subLev)
    {
        //subLev分だけレベルを減らす
        for (int i = 1; i <= subLev; i++)
        {
            //レベルが1以下になったら終了
            if(Level <= 1)  break;

            MaxHp -= 5;
            MaxAtk -= 2;
            MaxDef -= 2;
            CritDamage -= 0.1f;

            MaxExp -= MaxExpFun(Level);
            MaxSpeed -= MaxSpeedFun(Level);
            CritRate -= CritRateFun(Level);

            Level--;
        }

        //レベルが減る度にステータスをリセットする
        Hp = MaxHp;
        Atk = MaxAtk;
        Def = MaxDef;
        Speed = MaxSpeed;
        Exp = 0;
    }

    public void ResetStatus()
    {
        Hp = MaxHp;
        Atk = MaxAtk;
        Def = MaxDef;
        Speed = MaxSpeed;
    }

    //経験値を増やす
    public void AddExp(int addExp)
    {
        Exp += addExp;
        while (Exp >= MaxExp)
        {
            Exp -= MaxExp;
            LevelUp();
        }
    }

    //レベルアップ時に関数を用いる
    private int MaxExpFun(int level){
        return 5 * (int)Mathf.Log(level) + 3;
    }
    private int MaxSpeedFun(int level){
        return 3 * (int)Mathf.Log(level) + 1;
    }
    private float CritRateFun(int level){
        return 30.0f/Mathf.Pow((float)level + 30.0f, 2.0f);
    }

    //ーーーーーーーーーーーーーーーーーーー
    //プレイヤー行動時に呼び出す
    //ーーーーーーーーーーーーーーーーーーー

    public int Attacked(int Atk, float cr, float cd)
    {
        //通常のダメージ量の計算
        int damage = Atk - Def;
        if (damage < 0)
        {
            damage = 0;
        }

        //クリティカルの計算
        float crit = Random.Range(0.0f, 1.0f);
        if (crit <= cr)
        {
            damage = (int)(damage * cd);
        }

        Hp -= damage;
        return damage;
    }

    //ーーーーーーーーーーーーーーーーーーー
    //コンストラクタ・初期設定
    //ーーーーーーーーーーーーーーーーーーー

    //メインコンストラクタ・デフォルトのステータスを設定
    public Status(string name, int maxlevel, int maxexp,
    int maxhp, int maxatk, int maxdef, int maxspeed, 
    float cr, float cd, int code)
    {
        Name = name;
        Level = 1;
        Exp = 0;
        Hp = maxhp;
        Atk = maxatk;
        Def = maxdef;
        Speed = maxspeed;
        CritRate = cr;
        CritDamage = cd;

        MaxExp = maxexp;
        MaxLevel = maxlevel;
        MaxHp = maxhp;
        MaxAtk = maxatk;
        MaxDef = maxdef;
        MaxSpeed = maxspeed;

        CharCode = code;
    }
    
    public Status(Status status)
    {
        Name = status.Name;
        Level = status.Level;
        Exp = status.Exp;
        Hp = status.Hp;
        Atk = status.Atk;
        Def = status.Def;
        Speed = status.Speed;
        CritRate = status.CritRate;
        CritDamage = status.CritDamage;

        MaxExp = status.MaxExp;
        MaxLevel = status.MaxLevel;
        MaxHp = status.MaxHp;
        MaxAtk = status.MaxAtk;
        MaxDef = status.MaxDef;
        MaxSpeed = status.MaxSpeed;

        CharCode = status.CharCode;
    }
    

    //ーーーーーーーーーーーーーーーーーーーー
    //ステータスのゲッターセッター
    //ーーーーーーーーーーーーーーーーーーーー

    //キャラの名前
    public string Name { get; set; }

    //キャラの現在のステータス
    public int Level { get; set; }
    public int Exp { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Speed { get; set; }
    public float CritRate { get; set; }
    public float CritDamage { get; set; }

    //キャラのステータスの最大値
    public int MaxExp { get; set; }
    public int MaxLevel { get{
        return (int)((float)_maxLevel*BuffLevelRate) + BuffLevel;
    } set{
        _maxLevel = value;}}
    public int MaxHp { get{
        return (int)((float)_maxHp*BuffHpRate) + BuffHp;
    } set{
        _maxHp = value;
    } }
    public int MaxAtk { get{
        return (int)((float)_maxAtk*BuffAtkRate) + BuffAtk;
    } set{
        _maxAtk = value;
    } }
    public int MaxDef { get{
        return (int)((float)_maxDef*BuffDefRate) + BuffDef;
    } set{
        _maxDef = value;
    } }
    public int MaxSpeed { get{
        return (int)((float)_maxSpeed*BuffSpeedRate) + BuffSpeed;
    } set{
        _maxSpeed = value;
    } }
    //ステータス最大値のフィールド
    private int _maxLevel;
    private int _maxHp;
    private int _maxAtk;
    private int _maxDef;
    private int _maxSpeed;

    //キャラステータスの倍率バフ
    public float BuffLevelRate = 1.0f;
    public float BuffHpRate = 1.0f;
    public float BuffAtkRate = 1.0f;
    public float BuffDefRate = 1.0f;
    public float BuffSpeedRate = 1.0f;
    //キャラステータスの固定値バフ
    public int BuffLevel = 0;
    public int BuffHp = 0;
    public int BuffAtk = 0;
    public int BuffDef = 0;
    public int BuffSpeed = 0;


    public int CharCode{ get; set; }
}
