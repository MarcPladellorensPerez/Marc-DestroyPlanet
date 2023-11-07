using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float timeToExplosion = 4.0f;
    private float timer = 0.0f;
    private GameManager gm = null;
    public GameObject prefabExplosion;
    public AudioClip defuseSound; // Assigna aquest so des de l'editor d'Unity
    public AudioClip explosionSound; // Assigna aquest so des de l'editor d'Unity

    void Start()
    {
        GameObject o = GameObject.FindGameObjectWithTag("GameManager");

        if (o == null)
        {
            Debug.LogError("There's no gameObject with GameManager tag.");
        }
        else
        {
            gm = o.GetComponent<GameManager>();
            if (gm == null)
            {
                Debug.LogError("The GameObject with GameManager tag doesn't have the GameManager script attached to it");
            }
        }

        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToExplosion)
        {
            timer = 0.0f;
            gm.TakeDamage();
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            PlaySound(explosionSound);
            Destroy(gameObject);
        }

        Color newColor = Color.Lerp(Color.white, Color.red, timer / timeToExplosion);
        GetComponent<MeshRenderer>().material.color = newColor;
    }

    private void OnMouseDown()
    {
        gm.AddScore();
        PlaySound(defuseSound);
        Destroy(gameObject);
    }

    private void PlaySound(AudioClip clip)
    {
        // Crea un nuevo objeto temporal para reproducir el sonido
        GameObject soundGameObject = new GameObject("TempAudio");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        
        // Reproduce el sonido
        audioSource.Play();
        
        // Destruye el objeto temporal después de que el sonido haya terminado
        Destroy(soundGameObject, clip.length);
    }

    public void DeactivateBomb()
    {
        this.enabled = false; // Desactiva este script, previniendo la explosión y daño
    }
}
