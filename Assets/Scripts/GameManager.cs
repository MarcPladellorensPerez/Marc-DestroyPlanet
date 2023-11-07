using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;
    
    public TMP_Text lbl_Score;
    public TMP_Text lbl_Lives;
    public MenuManager menuManager;
    
    public AudioClip gameOverSound; // Assigna aquest so des de l'editor d'Unity
    public AudioClip victorySound; // Assigna aquest so des de l'editor d'Unity
    private AudioSource audioSource;

    public bool IsGameActive { get; private set; }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RestartGame();
    }

    public void AddScore()
    {
        score++;
        lbl_Score.text = "Score: " + score;
    }

    public void TakeDamage()
    {
        lives--;
        lbl_Lives.text = "Lives: " + lives;
        if (lives <= 0)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        StopGame(); // Esto debe suceder antes de mostrar cualquier menú para detener el juego por completo
        int bestScore = ScoreManager.Instance.GetBestScore();
        if (score > bestScore)
        {
            ScoreManager.Instance.TrySetNewBestScore(score);
            audioSource.PlayOneShot(victorySound);
            menuManager.ShowVictoryMenu();
        }
        else
        {
            audioSource.PlayOneShot(gameOverSound);
            menuManager.ShowGameOverMenu();
        }
    }

    public void RestartGame()
    {
        lives = 3;
        score = 0;
        lbl_Lives.text = "Lives: " + lives;
        lbl_Score.text = "Score: " + score;
        ShowStartMenu();
    }

    public void StartGame()
    {
        RestartGame(); // Asegúrate de que esto reinicie las vidas y el score
        IsGameActive = true;
        menuManager.StartGame();
    }

    public void ShowStartMenu()
    {
        menuManager.ShowStartMenu();
    }
    
    public void StopGame()
    {
        IsGameActive = false;
        // Llama al método para detener la generación de bombas
        SpawnManager[] spawners = FindObjectsOfType<SpawnManager>();
        foreach (var spawner in spawners)
        {
            spawner.StopSpawningBombs();
        }
        
        // Desactiva todas las bombas existentes
        BombScript[] bombs = FindObjectsOfType<BombScript>();
        foreach (var bomb in bombs)
        {
            bomb.DeactivateBomb();
        }
    }

    public void ResetGame()
    {
        lives = 3;
        score = 0;
        lbl_Lives.text = "Lives: " + lives;
        lbl_Score.text = "Score: " + score;
    }
}
