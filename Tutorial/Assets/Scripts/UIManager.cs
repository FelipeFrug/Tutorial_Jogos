using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject endGamePanel;

    public GameObject lostGamePanel; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.gameOver){
            endGamePanel.SetActive(true);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
             foreach (GameObject collectable in GameObject.FindGameObjectsWithTag("Coletavel"))
            {
                Destroy(collectable);
            }
        }
        if (GameController.playerDead){
            lostGamePanel.SetActive(true);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
             foreach (GameObject collectable in GameObject.FindGameObjectsWithTag("Coletavel"))
            {
                Destroy(collectable);
            }
        }
    }
}
