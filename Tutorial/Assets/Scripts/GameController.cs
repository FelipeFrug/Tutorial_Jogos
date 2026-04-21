using UnityEngine;

public static class GameController
{
    private static int collectableCount;

    public static int roundsCount;

    public static int lives;

    public static bool gameOver {get{return roundsCount <= 0;}}

    public static bool roundOver {get{return collectableCount <= 0;}}

    public static bool playerDead {get{return lives <= 0;}}

    public static System.Action OnRoundStart;

    public static void Init(){
        collectableCount = 3;
        roundsCount = 5;
        lives = 3;

        OnRoundStart?.Invoke();
    }   

    public static void Collect(){
        collectableCount--;
        if(collectableCount <= 0){
            roundsCount--;
            collectableCount = 3;
            if (roundsCount > 0) OnRoundStart?.Invoke();

        }
    }

    public static void Damage(){
        lives--;
    }

}
