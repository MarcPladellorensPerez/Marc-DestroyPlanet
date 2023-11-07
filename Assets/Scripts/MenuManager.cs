using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;
    public GameObject gamePanel;

    public TMP_Text startMenuBestScoreText;
    public TMP_Text victoryBestScoreText;
    public TMP_Text gamePanelBestScoreText;

    private void Awake()
    {
        UpdateBestScoreDisplay();
    }

    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gamePanel.SetActive(false);
        UpdateBestScoreDisplay();
    }

    public void StartGame()
    {
        // Encuentra el SpawnManager y destruye todas las bombas existentes
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager != null)
        {
            spawnManager.DestroyAllBombs();
            spawnManager.EnableSpawning(); // Reactiva la generación de bombas
        }
        else
        {
            Debug.LogError("SpawnManager not found in the scene.");
        }

        // Asegúrate de que el GameManager esté listo para un nuevo juego
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ResetGame(); // Asegúrate de que esta función reinicie adecuadamente el juego
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        // Activa el panel del juego y desactiva todos los menús
        gamePanel.SetActive(true);
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        victoryMenu.SetActive(false);
        UpdateBestScoreDisplay();
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        startMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gamePanel.SetActive(false);
        // No es necesario actualizar el Best Score aquí si el gamePanel no se muestra.
    }

    public void ShowVictoryMenu()
    {
        victoryMenu.SetActive(true);
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gamePanel.SetActive(false);
        // Aquí se actualiza el Best Score en el menú de victoria.
        if (victoryBestScoreText != null)
        {
            victoryBestScoreText.text = "Best Score: " + ScoreManager.Instance.GetBestScore();
        }
    }

    private void UpdateBestScoreDisplay()
    {
        int bestScore = ScoreManager.Instance.GetBestScore();
        if (startMenuBestScoreText != null)
        {
            startMenuBestScoreText.text = "Best Score: " + bestScore;
        }
        if (gamePanelBestScoreText != null)
        {
            gamePanelBestScoreText.text = "Best Score: " + bestScore;
        }
        // No necesitas actualizar el victoryBestScoreText aquí ya que se actualiza en ShowVictoryMenu().
    }
}
