#include <stdio.h>
#include <stdlib.h>

int map[10][10] = {
    {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
    {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    {2, 0, 0, 0, -1, 1, 0, 0, 0, 2},
    {2, 0, 0, 0, 1, -1, 0, 0, 0, 2},
    {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    {2, 2, 2, 2, 2, 2, 2, 2, 2, 2} };

int pointmap[10][10] = {
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 120, -20, 20, 5, 5, 20, -20, 120, 0},
    {0, -20, -40, -5, -5, -5, -5, -40, -20, 0},
    {0, 20, -5, 15, 3, 3, 15, -5, 20, 0},
    {0, 5, -5, 3, 3, 3, 3, -5, 5, 0},
    {0, 5, -5, 3, 3, 3, 3, -5, 5, 0},
    {0, 20, -5, 15, 3, 3, 15, -5, 20, 0},
    {0, -20, -40, -5, -5, -5, -5, -40, -20, 0},
    {0, 120, -20, 20, 5, 5, 20, -20, 120, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};


struct data {
    int setturn;
    int setstonex[20];
    int setstoney[20];
    struct data* back;
};

int turnpart = 1;
int turn = 0;
int X, Y;
struct data* p,  *newp = NULL;

void printmap(void);
int checkcandidate(int x, int y, int me);
int checkcontinue(int me);
void putstone(int x, int y, int me, int s, int turns);
int selectwinner(void);
int printstone(void);
void computerlogic(int me);
int decidestone(int deep, int point, int me);
int evalate(int me, int turns);
void remake(int turns);

int main()
{
    int x, y, s;
    p = NULL;
    while (1) {
        s = checkcontinue(turnpart);
        if (s != 0) {
            switch (turnpart) {
            case 1: printf("先手の番です.\n"); break;
            case -1: printf("後手の番です.\n"); break;
            }
            if (s == turnpart) {
                printmap();
                while (1) {
                    printf("打つマスを選んでください. : ");
                    switch (turnpart) {
                        case 1: scanf("%d, %d", &x, &y); break;
                        case -1: computerlogic(-1); x = X; y = Y; printf("%d, %d\n", x, y); X = 0; Y = 0; break;
                    }
                    s = checkcandidate(x, y, turnpart);
                    if (s != 1) {
                        putstone(x, y, turnpart, s, turn);
                        turn++;
                        turnpart *= -1;
                        break;
                    }
                    else { printf("(%d, %d)そのマスには置けません.\n", x, y);  x = 0; y = 0;}
                }
            }
            else {
                printf("置けるマスがないためパスします.\n");
                turn++;
                turnpart *= -1;
            }
        }
        else {
            printmap();
            printf("両者置けるマスがないためゲームを終了します.\n");
            s = printstone();
            printf("%d石差で", s);
            s = selectwinner();
            switch (s) {
                case 1: printf("先手の勝ちです.\n"); break;
                case -1: printf("後手の勝ちです.\n"); break;
                default: printf("引き分けです.\n"); break;
            }
            break;
        }
        x = 0;
        y = 0;
    }
    return 0;
}


void printmap(void)
{
    int a, b;
    for (a = 1; a <= 8; a++) {
        for (b = 1; b <= 8; b++) {
            switch (map[a][b]) {
            case 1: printf("○"); break;
            case -1: printf("●"); break;
            case 0: printf("□"); break;
            }
        }
        printf("\n");
    }
}

int checkcandidate(int x, int y, int me)
{
    int n, s = 1, enemy;
    enemy = me * -1;
    if (map[y][x] != 0) return s;
    if (map[y - 1][x] == enemy) {
        for (n = 2; map[y - n][x] == enemy; n++);
        if (map[y - n][x] == me) s *= 2;
    }
    if (map[y + 1][x] == enemy) {
        for (n = 2; map[y + n][x] == enemy; n++);
        if (map[y + n][x] == me) s *= 3;

    }
    if (map[y][x + 1] == enemy) {
        for (n = 2; map[y][x + n] == enemy; n++);
        if (map[y][x + n] == me) s *= 5;
    }
    if (map[y][x - 1] == enemy) {
        for (n = 2; map[y][x - n] == enemy; n++);
        if (map[y][x - n] == me) s *= 7;
    }
    if (map[y - 1][x + 1] == enemy) {
        for (n = 2; map[y - n][x + n] == enemy; n++);
        if (map[y - n][x + n] == me) s *= 11;
    }
    if (map[y + 1][x + 1] == enemy) {
        for (n = 2; map[y + n][x + n] == enemy; n++);
        if (map[y + n][x + n] == me) s *= 13;
    }
    if (map[y - 1][x - 1] == enemy) {
        for (n = 2; map[y - n][x - n] == enemy; n++);
        if (map[y - n][x - n] == me) s *= 17;
    }
    if (map[y + 1][x - 1] == enemy) {
        for (n = 2; map[y + n][x - n] == enemy; n++);
        if (map[y + n][x - n] == me) s *= 19;
    }
    return s;
}

int checkcontinue(int me)
{
    int a, b;
    for (a = 1; a <= 8; a++) {
        for (b = 1; b <= 8; b++) {
            if (checkcandidate(a, b, me) != 1) return me;
        }
    }
    if (me == turnpart) return checkcontinue(me * -1);
    else return 0;
}

void putstone(int x, int y, int me, int s, int turns)
{
    int n = 1, m = 1;
    if (p != NULL) {
        if(p->setturn < turns) newp = (struct data*)malloc(sizeof(struct data));
        else {
            newp = p;
            p = p->back;
        }
    }else newp = (struct data*)malloc(sizeof(struct data));
    newp->setturn = turns;
    map[y][x] = me;
    newp->setstonex[0] = x;
    newp->setstoney[0] = y;
    if (s % 2 == 0) {
        while (map[y - n][x] == me * -1) {
            map[y - n][x] = me;
            newp->setstonex[m] = x;
            newp->setstoney[m] = y - n;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 3 == 0) {
        while (map[y + n][x] == me * -1) {
            map[y + n][x] = me;
            newp->setstonex[m] = x;
            newp->setstoney[m] = y + n;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 5 == 0) {
        while (map[y][x + n] == me * -1) {
            map[y][x + n] = me;
            newp->setstonex[m] = x + n;
            newp->setstoney[m] = y;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 7 == 0) {
        while (map[y][x - n] == me * -1) {
            map[y][x - n] = me;
            newp->setstonex[m] = x - n;
            newp->setstoney[m] = y;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 11 == 0) {
        while (map[y - n][x + n] == me * -1) {
            map[y - n][x + n] = me;
            newp->setstonex[m] = x + n;
            newp->setstoney[m] = y - n;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 13 == 0) {
        while (map[y + n][x + n] == me * -1) {
            map[y + n][x + n] = me;
            newp->setstonex[m] = x + n;
            newp->setstoney[m] = y + n;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 17 == 0) {
        while (map[y - n][x - n] == me * -1) {
            map[y - n][x - n] = me;
            newp->setstonex[m] = x - n;
            newp->setstoney[m] = y - n;
            n++;
            m++;
        }
        n = 1;
    }
    if (s % 19 == 0) {
        while (map[y + n][x - n] == me * -1) {
            map[y + n][x - n] = me;
            newp->setstonex[m] = x - n;
            newp->setstoney[m] = y + n;
            n++;
            m++;
        }
    }
    newp->setstonex[m] = 0;
    newp->setstoney[m] = 0;
    newp->back = p;
    p = newp;
}

int selectwinner(void)
{
    int a, b, sum = 0;
    for (a = 1; a <= 8; a++) {
        for (b = 1; b <= 8; b++) {
            sum += map[a][b];
        }
    }
    if (sum > 0) return 1;
    else if (sum < 0) return -1;
    else return 0;
}

int printstone(void)
{
    int a, b, first = 0, second = 0, stone;
    for (a = 1; a <= 8; a++) {
        for (b = 1; b <= 8; b++) {
            if (map[a][b] == 1) first++;
            else if (map[a][b] == -1) second++;
            map[a][b] = 0;
        }
    }
    stone = first - second;
    if (stone < 0) stone *= -1;
    for (a = 1; a <= 8 && first > 0; a++) {
        for (b = 1; b <= 8 && first > 0; b++) {
                map[a][b] = 1;
                first--;
        }
    }
    for (a = 8; a >= 1 && second > 0; a--) {
        for (b = 8; b >= 1 && second > 0; b--) {
            map[a][b] = -1;
            second--;
        }
    }
    printmap();
    return stone;
}

void computerlogic(int me)
{
    int a, b, c, deep, assess = 50001;
    if (p == NULL) {
        X = 3;
        Y = 4;
        return;
    }
    if (turn >= 52) deep = 12;
    else deep = 5;
    for (a = 1; a <= 8; a++) {
        for (b = 1; b <= 8; b++) {
            c = checkcandidate(b, a, me);
            if (c != 1) {
                putstone(b, a, me, c, turn);
                c = decidestone(deep, 1, me * -1);
                if (assess > c) {
                    assess = c;
                    X = b;
                    Y = a;
                }
                remake(turn);
            }
        }
    }
}

int decidestone(int deep, int point, int me)
{
    int a, b, c, assess = 50001;
    if (checkcontinue(me) == 0) return selectwinner() * 50000;
    if (deep - point <= 0) {
        return evalate(me, turn + point);
    }else {
        for (a = 1; a <= 8; a++) {
            for (b = 1; b <= 8; b++) {
                c = checkcandidate(b, a, me);
                if (c != 1) {
                    putstone(b, a, me, c, turn + point);
                    c = decidestone(deep, point + 1, me * -1);
                    if ((assess == 50001 || assess * me < c * me) && c != 50001) assess = c;
                    remake(turn + point);
                }
            }
        }
        return assess;
    }
}

int evalate(int me, int turns)
{
    int a, b, c, d, assesspoint = 0, assesscandidate = 0, assessopen = 0;
    for (a = 1; a <= 8; a++) {
        for (b = 1; b <= 8; b++) {
            assesspoint += map[b][a] * pointmap[b][a];
            if (checkcandidate(a, b, me) != 1) assesscandidate += me;
            if (checkcandidate(a, b, me * -1) != 1) assesscandidate -= me;
            if (map[b][a] != 2 && map[b][a] != 0) {
                for (c = -1; c <= 1; c++) {
                    for (d = -1; d <= 1; d++) {
                        if (map[b+d][a+c] == 0) {
                            assessopen -= map[b][a];
                        }
                    }
                }
            }
        }
    }
    return 20 *  assesspoint + 2 * turns * assesscandidate + (10 + turns) * assessopen;
}

void remake(int turns)
{
    int n;
    struct data* q;
    while (p->setturn >= turns) {
        map[p->setstoney[0]][p->setstonex[0]] = 0;
        for (n = 1; p->setstonex[n] != 0; n++) {
            map[p->setstoney[n]][p->setstonex[n]] *= -1;
        }
        q = p;
        p = p->back;
        free(q);
    }
}
