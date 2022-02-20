
using System;

struct Status{
        short hp, mp, att, def, age, luc;
        bool[] aType = new bool[4];
        float buf, deb;
        float[] dType = new float[4];
}

abstract Mono {
    
    Status status;
    short nowHP, nowMP;
    
    static void changestatus(byte n){
        //オーバーライド用
    }
    
    static short GetNowHP(){
        return nowHP;
    }
    
    static short GetNowMP(){
        return nowMP;
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
    
    static short GetAge(){
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
    
}

abstract Me : Mono{
    byte job;
    
    static void changestatus(byte n){
        status = jobStatus[n]; //jobStatusはジョブ毎のステータスを管理している配列
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
        status = enemyStatus[n]; //enemyStatusは敵毎のステータスを管理している配列
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

class Enemy : You{
    
}

//Enemyは同時に出現する最大の敵の数の分だけ実装しておく