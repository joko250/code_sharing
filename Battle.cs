using System;

public class Battle {
    sbyte[] meAttDefBuff1 = new sbyte[5];
    sbyte[] meAttDefBuff2 = new sbyte[5];
    sbyte[] meAttDefBuff3 = new sbyte[5];
    sbyte[] enemyAttDefBuff1 = new sbyte[5];
    sbyte[] enemyAttDefBuff2 = new sbyte[5];
    sbyte[] enemyAttDefBuff3 = new sbyte[5];
    sbyte[] meAttDefBuffTurn1 = new sbyte[5];
    sbyte[] meAttDefBuffTurn2 = new sbyte[5];
    sbyte[] meAttDefBuffTurn3 = new sbyte[5];
    sbyte[] enemyAttDefBuffTurn1 = new sbyte[5];
    sbyte[] enemyAttDefBuffTurn2 = new sbyte[5];
    sbyte[] enemyAttDefBuffTurn3 = new sbyte[5];
    
    public void M() {
        double skill;
        short att, def;
        for(att = 10; att <= 200; att += 10){
            for(def = 10; def <= 200; def += 10){
                skill = 1.0;
                short n = DamageB(DamageA(att, def, skill), meAttDefBuff1, enemyAttDefBuff1);
                Console.Write("Skill: {0} Att: {1} Def: {2} Damage: {3}\n", skill, att, def, n);
            }
        }
    }
    
    private int DamageA(short att, short def, double skill){
        double attack;
        double defence;
        double normalDamage;
        double correction;
        attack = att;
        defence = def;
        if(attack >= defence) correction = 0.3 * (2 - Math.Pow(defence / attack, 3));
        else correction = 1 - 0.7 * Math.Sqrt(Math.Sqrt(defence / attack));
        normalDamage = skill * 0.15 *(3 * correction * attack - 0.2 * defence);
        Random R = new Random();
        normalDamage *= 0.01 * R.Next(98, 102) * skill;
        if(normalDamage < 0) normalDamage = 0;
        return (int)normalDamage;
    }

    private short DamageB(int damage, sbyte[] att, sbyte[] def){
        short a = 100, b = 100;
        for(byte n = 0; n < 5; n++){
            a += att[n];
            b += def[n];
        }
        damage *= a * b;
        damage = (int)(0.0001 * damage);
        return (short)damage;
    }
}