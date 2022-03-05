using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] Vector2 Velocidad = new Vector2(2, 3);
    [SerializeField] float Amplitud = 3;

    const string TAG_BALA_JUGADOR = "Bala Jugador";

    float Desfase;

    private void Start()
    {
        Desfase = Random.Range(0.0f, 10.0f);
    }
    void Update()
    {
        Mover();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(TAG_BALA_JUGADOR)) return;

        Destruir();
    }

    void Mover()
    {
        transform.position = new Vector2
        (
            // Movimiento lineal
            transform.position.x - Velocidad.x * Time.deltaTime,
            // Movimiento senoidal
            Amplitud * Mathf.Sin(Velocidad.y * Time.time + Desfase)
        );
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
}
