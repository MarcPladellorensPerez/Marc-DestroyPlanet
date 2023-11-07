using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private static int bestScore = 0; // Variable estática para mantener el mejor puntaje durante la sesión

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene este objeto persistente entre escenas
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TrySetNewBestScore(int score)
    {
        if (score > bestScore)
        {
            bestScore = score; // Actualiza el mejor puntaje si el nuevo puntaje es mayor
        }
    }

    public int GetBestScore()
    {
        return bestScore; // Devuelve el mejor puntaje actual
    }
}
