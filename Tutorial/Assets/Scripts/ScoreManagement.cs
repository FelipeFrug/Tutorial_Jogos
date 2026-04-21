using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI liveText; // Referência ao texto na tela
    public TextMeshProUGUI RoundsText; // Referência ao texto na tela

    void Update()
    {
        liveText.text = string.Format("Lives: {0}", GameController.lives);
        RoundsText.text = string.Format("Rounds: {0}/5", 6- GameController.roundsCount);
    }
}