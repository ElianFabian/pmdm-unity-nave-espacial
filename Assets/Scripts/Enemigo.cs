using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] float Amplitud = 3;

    const string TAG_BALA_JUGADOR = "Bala Jugador";

    Vector2 Velocidad;
    float Desfase;

    const float limitIzquierdo = -14;

    private void Start()
    {
        Desfase = Random.Range(0.0f, 10.0f);

        var x = Random.Range(1.0f, 2.0f);
        var y = Random.Range(1.0f, 3.0f);

        Velocidad = new Vector2(x, y);
    }
    void Update()
    {
        Mover();

        if (transform.position.x < limitIzquierdo)
        {
            Destruir();
        }
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
