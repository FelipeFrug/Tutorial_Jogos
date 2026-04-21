using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Referência ao texto na tela

    public float timeElapsed = 0f;

    void Update()
    {
        // Adiciona o tempo que passou desde o último frame
        if (!GameController.gameOver && !GameController.playerDead)
        {
            timeElapsed += Time.deltaTime;
        }

        // Calcula minutos e segundos
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Atualiza o texto na tela no formato 00:00
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}