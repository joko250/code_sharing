
using System;

int SkillNum = 64; //スキル数を64としている

struct Status{
    string name;
    sbyte pattern[2]; //敵のAIの設定
    short hp, mp, att, def, age, luc;
    bool[] aType = new bool[4];
    float buf, deb;
    float[] dType = new float[4];
    bool[] skill = new bool[SkillNum];
    public Status(string n, bool[] p, short a, short b, short c, short d, short e, float f, float g, short h, bool[] x, float[] y){
        name = n;
        pattern = p;
        hp = a;
        mp = b;
        att = c;
        def = d;
        age = e;
        luc = f;
        aType = x;
        buf = g;
        deb = h;
        dType = y;
    }
}

struct Skill{
    string name;
    byte pattern; //スキルの種類
    float magnification;
    sbyte costMP;
}

class Data{
    Status[] jobStatus = new Status[7]{
        new Status("剣士", {0, 0}, 95, 30, 150, 95, 65, 40, 0.65, 0.65, {TRUE, FALSE, TRUE, FALSE},
        new Status("盗賊", {0, 0}, 85, 35, 90, 40, 100, 100, 0.35, 0.35, {TRUE, FALSE, TRUE, FALSE}, );
        new Status("狩人", {0, 0}, 90, 50, 120, 70, 80, 70, 0.75, 0.75, {FALSE, TRUE, TRUE, FALSE}, );
        new Status("魔法使い", {0, 0}, 90, 100, 105, 80, 40, 60, 0.45, 0.45, {FALSE, TRUE, FALSE, TRUE}, );
        new Status("魔法剣士", {0, 0}, 95, 70, 110, 100, 50, 45, 55, 0.55, 0.55, {TRUE, FALSE, FALSE, TRUE}, );
        new Status("エンチャンター", {0, 0}, 80, 85, 60, 20, 45, 85, 30, 0.85, 0.15, {FALSE, TRUE, FALSE, TRUE}, );
        new Status("ヒーラー", {0, 0}, 80, 90, 30, 50, 70, 50, 75, 0.50, 0.50, {FALSE, TRUE, FALSE, TRUE}, );//被ダメ倍率のみ未設定
    };
    Status[] enemyStatus = new Status[100];　//敵数を100としている
    //　ここに初期値を入力してステータスを設定する
}
    float[] dType = new float[4];
    bool[] skill = new bool[SkillNum];
    public Status(aType, dType){


abstract Mono {
    
    Status status;
    short nowHP, nowMP;
    
    static void Changestatus(byte n){
        //オーバーライド用
    }
    
    static short GetNowHP(){
        return nowHP;
    }
    
    static short GetNowMP(){
        return nowMP;
    }
    
    static short GetName(){
        return status.name;
    }
    
    static short GetHP(){
        return status.hp;
    }
    
    static short GetMP(){
        return status.mp;
    }
    
    static short GetAtt(){
        return status.att;
    }
    
    static short GetDef(){
        return status.age;
    }
    
    static short {
        return status.age;
    }
    
    static short GetLuc(){
        return status.luc;
    }
    
    static sbyte GetAttackType(){
        return status.aType[0] + 2 * status.aType[1];
    }
    
    static float GetBuf(){
        return status.buf;
    }
    
    static float GetDeb(){
        return status.deb;
    }
    
    static float GetDefenceType(byte n){
        return status.dType[n];
    }
    
    static bool [] GetSkillflug(){
        return status.skill;
    }
    
    static int GetSkillNumber(){
        return status.skill.Length;
    }
    
}

abstract Me : Mono{
    byte job;
    
    static void changestatus(byte n){
        status = Data.jobStatus[n]; //jobStatusはジョブ毎のステータスを管理している配列
    }
    
    static void changeJob(byte name){
        job = name;
        changestatus(name);
        nowHP = status.hp;
        nowMP = status.mp;
    }
    
    static void changeHP(short damage){
        nowHP -= damage;
        if(nowHP < 0) nowHP = 0;
        else if(nowHP > status.hp) nowHP = status.hp;
    }
    
    static void changeHP(short use){
        nowMP -= use;
        else if(nowMP > status.mp) nowMP = status.mp;
    }
}

abstract You : Mono{
    static void changestatus(byte n){
        status = Data.enemyStatus[n]; //enemyStatusは敵毎のステータスを管理している配列
        nowHP = status.hp;
        nowMP = status.mp;
    }

    static void changeHP(short damage){
        nowHP -= damage;
        if(nowHP < 0) nowHP = 0;
        else if(nowHP > status.hp) nowHP = status.hp;
    }
    
    static void changeHP(short use){
        nowMP -= use;
        else if(nowMP > status.mp) nowMP = status.mp;
    }
}

class Hero1 : Me{
    
}

class Hero2 : Me{
    
}

class Hero3 : Me{
    
}

class Enemy1 : You{
    
}

class Enemy2 : You{
    
}

class Enemy3 : You{
    
}

//Enemyは同時に出現する最大の敵の数の分だけ実装しておく