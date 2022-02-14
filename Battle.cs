using System;

public class Battle {
    sbyte[] MeAttDefBuff1 = new sbyte[5];
    sbyte[] MeAttDefBuff2 = new sbyte[5];
    sbyte[] MeAttDefBuff3 = new sbyte[5];
    sbyte[] EnemyAttDefBuff1 = new sbyte[5];
    sbyte[] EnemyAttDefBuff2 = new sbyte[5];
    sbyte[] EnemyAttDefBuff3 = new sbyte[5];
    sbyte[] MeAttDefBuffTurn1 = new sbyte[5];
    sbyte[] MeAttDefBuffTurn2 = new sbyte[5];
    sbyte[] MeAttDefBuffTurn3 = new sbyte[5];
    sbyte[] EnemyAttDefBuffTurn1 = new sbyte[5];
    sbyte[] EnemyAttDefBuffTurn2 = new sbyte[5];
    sbyte[] EnemyAttDefBuffTurn3 = new sbyte[5];
    
    public void M() {
        double Skill;
        int Att, Def;
        for(Att = 10; Att <= 200; Att += 10){
            for(Def = 10; Def <= 200; Def += 10){
                Skill = 1.0;
                short B = DamageB(DamageA(Att, Def, Skill), MeAttDefBuff1, EnemyAttDefBuff1);
                Console.Write("Skill: {0} Att: {1} Def: {2} Damage: {3}\n", Skill, Att, Def, B);
            }
        }
    }
    
    private int DamageA(int Att, int Def, double Skill){
        double Attack;
        double Defence;
        double NormalDamage;
        double Correction;
        Attack = Att;
        Defence = Def;
        if(Attack >= Defence) Correction = 0.3 * (2 - Math.Pow(Defence / Attack, 3));
        else Correction = 1 - 0.7 * Math.Sqrt(Math.Sqrt(Defence / Attack));
        NormalDamage = Skill * 0.15 *(3 * Correction * Attack - 0.2 * Defence);
        Random R = new Random();
        NormalDamage *= 0.01 * R.Next(98, 102) * Skill;
        if(NormalDamage < 0) NormalDamage = 0;
        return (int)NormalDamage;
    }

    private short DamageB(int Damage, sbyte[] Att, sbyte[] Def){
        int A = 100, B = 100;
        for(int n = 0; n < 5; n++){
            A += Att[n];
            B += Def[n];
        }
        Damage = A * B * Damage;
        Damage = (int)(0.0001 * Damage);
        return (short)Damage;
    }
}