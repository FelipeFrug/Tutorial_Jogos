using UnityEngine;

public class EnemyGrower : MonoBehaviour
{
    public float growthRate = 0.1f; // O quanto ele cresce por segundo
    public float maxScale = 3.0f;  
    
    public static float currentSize = -1f;

    void Start()
    {
        if (currentSize < 0)
        {
            currentSize = 1f; // Ou transform.localScale.x, assumindo 1 para começar do zero
        }
        else
        {
            transform.localScale = new Vector3(currentSize, currentSize, transform.localScale.z);
        }
    }

    void Update()
    {
        if (GameController.gameOver || GameController.playerDead)
        {
            currentSize = -1f;
            return;
        }

        // Verifica se o objeto atual tem a tag "Enemy"
        if (gameObject.CompareTag("Enemy"))
        {
            // Verifica se ainda não atingiu o tamanho máximo
            if (transform.localScale.x < maxScale)
            {
                // Cria o novo vetor de escala baseado no crescimento por segundo
                float growth = growthRate * Time.deltaTime;
                transform.localScale += new Vector3(growth, growth, 0); 
                
                // Atualiza o tamanho estático para manter nos próximos rounds
                currentSize = transform.localScale.x;
            }
        }
    }
}