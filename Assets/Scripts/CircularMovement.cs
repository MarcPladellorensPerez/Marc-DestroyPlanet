using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 50f; // Radio del círculo.
    public float speed = 1f; // Velocidad de movimiento.

    private float angle; // Ángulo actual alrededor del círculo.

    void Update()
    {
        // Incrementa el ángulo basado en la velocidad y el tiempo.
        angle += speed * Time.deltaTime; 
        angle %= 2 * Mathf.PI; // Asegura que el ángulo no sobrepase los 360 grados.

        // Calcula la nueva posición x y z basándose en el ángulo y el radio.
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        
        // Mueve la nave espacial a la nueva posición.
        transform.position = newPosition;

        // Hace que la nave espacial mire hacia adelante. El segundo argumento Vector3.up indica que el 'up' de la nave es hacia arriba en el mundo.
        transform.LookAt(newPosition + new Vector3(Mathf.Cos(angle + Mathf.PI/2), 0, Mathf.Sin(angle + Mathf.PI/2)), Vector3.up);
    }
}
