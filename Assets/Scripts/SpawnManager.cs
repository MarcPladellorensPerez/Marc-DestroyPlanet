using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bombParent;
    public GameObject prefabBomb;
    public GameObject gamePanel;
    public float respawnTime = 3.0f;
    private float timer = 0.0f;
    private bool isSpawning = true;
    
    // Variables para la aceleración del spawn de bombas
    private float timeSinceStart = 0.0f;
    public float timeToIncreaseSpeed = 5.0f; // Tiempo para aumentar la velocidad de generación
    public float speedIncreaseFactor = 0.9f; // Factor de disminución del respawnTime

    void Update()
    {
        if (isSpawning && gamePanel.activeSelf)
        {
            timer += Time.deltaTime;
            timeSinceStart += Time.deltaTime;

            // Comprobar si es hora de acelerar la generación de bombas
            if (timeSinceStart >= timeToIncreaseSpeed)
            {
                timeSinceStart = 0; // Restablecer el contador de tiempo
                respawnTime *= speedIncreaseFactor; // Disminuir el tiempo de reaparición
                respawnTime = Mathf.Max(respawnTime, 0.5f); // Asegurar que el tiempo no sea demasiado corto
            }

            if (timer >= respawnTime)
            {
                timer = 0.0f;
                CreateNewBomb();
            }
        }
    }

    private void CreateNewBomb()
    {
        Vector3 randPosition = Random.onUnitSphere * 0.5f; // Asegúrate de que esta posición sea adecuada para tu juego
        Instantiate(prefabBomb, randPosition, Quaternion.identity, bombParent.transform);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timeSinceStart = 0.0f; // Restablecer el contador de tiempo para la aceleración
        respawnTime = 3.0f; // Restablecer el tiempo de reaparición inicial
    }

    public void StopSpawningBombs()
    {
        isSpawning = false; // Detiene el Update de generar nuevas bombas
    }

    public void DestroyAllBombs()
    {
        foreach (Transform bomb in bombParent.transform)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void EnableSpawning()
    {
        this.enabled = true; // Reactiva la generación de bombas
        timer = 0.0f; // Restablece el temporizador para la generación inmediata
        StartSpawning(); // Asegúrate de reiniciar la lógica de spawn
    }
}
