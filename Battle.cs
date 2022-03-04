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
    
    private sbyte [] SpeedCompare(){
        short a, b, c, d, e, f, max;
        Random R = new Random();
        a = (short)(Hero1.GetAge() * ((R.NextDouble() - 0.5) * 0.4 + 1));
        b = (short)(Hero2.GetAge() * ((R.NextDouble() - 0.5) * 0.4 + 1));
        c = (short)(Hero3.GetAge() * ((R.NextDouble() - 0.5) * 0.4 + 1));
        d = (short)(Hero4.GetAge() * ((R.NextDouble() - 0.5) * 0.4 + 1));
        e = (short)(Hero5.GetAge() * ((R.NextDouble() - 0.5) * 0.4 + 1));
        f = (short)(Hero6.GetAge() * ((R.NextDouble() - 0.5) * 0.4 + 1));
        sbyte sort[] = new sbyte[6];
        for(int n = 0; n < 6; n++){
            max = Math.Max(Math.Max(a, Math.Max(b, c)), Math.Max(d, Math.Max(e, f)));
            if(max == a){
                sort[n] = 1;
                a = -1;
            }else if(max == b){
                sort[n] = 2;
                b = -1;
            }else if(max == c){
                sort[n] = 3;
                c = -1;
            }else if(max == d){
                sort[n] = 4;
                d = -1;
            }else if(max == e){
                sort[n] = 5;
                e = -1;
            }else if(max == f){
                sort[n] = 6;
                f = -1;
            }
        }
        return sort;
    }
}