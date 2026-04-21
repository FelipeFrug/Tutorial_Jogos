using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject collectablePrefab;
    public GameObject enemyPrefab;

    public float minDistanceBetweenObjects = 2f;
    public int maxSpawnAttempts = 30; // Previne loops infinitos

    private void OnEnable()
    {
        // Inscreve no evento que será disparado toda vez que iniciar um round
        GameController.OnRoundStart += SpawnCollectables;
    }

    private void OnDisable()
    {
        // Importante desinscrever para evitar memory leaks
        GameController.OnRoundStart -= SpawnCollectables;
    }

    private void Start()
    {
        // Inicia o jogo para o primeiro round (caso o GameController não o faça em outro script)
        // Se a chamada GameController.Init() já existir em outro Manager, remova essa linha.
        GameController.Init();
    }

    private void SpawnCollectables()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        int collectableAmountToSpawn = 3; 
        int enemyAmountToSpawn = 6 - GameController.roundsCount; 

        System.Collections.Generic.List<Vector3> spawnedPositions = new System.Collections.Generic.List<Vector3>();

        PlayerMovement player = Object.FindFirstObjectByType<PlayerMovement>();
        if (player != null)
        {
            spawnedPositions.Add(player.transform.position);
        }

        for (int i = 0; i < collectableAmountToSpawn; i++)
        {
            Vector3 randomPosition = GetValidRandomPosition(spawnedPositions);
            if (randomPosition != Vector3.zero || spawnedPositions.Count == 0) // Trata caso não ache posição
            {
                Instantiate(collectablePrefab, randomPosition, Quaternion.identity);
                spawnedPositions.Add(randomPosition);
            }
        }

        for (int i = 0; i < enemyAmountToSpawn; i++)
        {
            Vector3 randomPosition = GetValidRandomPosition(spawnedPositions);
            if (randomPosition != Vector3.zero || spawnedPositions.Count == 0)
            {
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                spawnedPositions.Add(randomPosition);
            }
        }
    }

    private Vector3 GetValidRandomPosition(System.Collections.Generic.List<Vector3> existingPositions)
    {
        for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
        {
            Vector3 candidatePosition = GetRandomPosition();
            bool isValid = true;

            foreach (Vector3 pos in existingPositions)
            {
                if (Vector3.Distance(candidatePosition, pos) < minDistanceBetweenObjects)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                return candidatePosition;
            }
        }

        Debug.LogWarning("Não foi possível encontrar uma posição livre após " + maxSpawnAttempts + " tentativas.");
        return Vector3.zero; // Retorna nulo/zero se falhar (precisa de map maior ou distância menor)
    }

    private Vector3 GetRandomPosition()
    {
        // Usa as escalas X e Y do objeto pra definir o tamanho da área na Unity
        float finalWidth = transform.localScale.x;
        float finalHeight = transform.localScale.y;

        float randomX = Random.Range(-finalWidth / 2f, finalWidth / 2f);
        float randomY = Random.Range(-finalHeight / 2f, finalHeight / 2f);

        // Retorna a posição relativa à posição do spawner (ou posição global)
        return transform.position + new Vector3(randomX, randomY, 0f);
    }

    // Desenha a área de spawn no Unity Editor para facilitar o ajuste (Gizmos)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        
        float finalWidth = transform.localScale.x;
        float finalHeight = transform.localScale.y;

        Gizmos.DrawCube(transform.position, new Vector3(finalWidth, finalHeight, 0f));
    }
}
