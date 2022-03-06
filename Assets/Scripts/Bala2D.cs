using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bala2D : MonoBehaviour
{
    #region Atributos
    [SerializeField] float Velocidad      = 10;
    [SerializeField] float TiempoDeVida   = 5;
    [SerializeField] float EscalaGravedad = 0;
    [SerializeField] bool  IsTrigger      = true;

    bool destruir = false;
    float tiempoDeAudioReproducido = 0;

    AudioSource Sonido;

    const string TAG_ENEMIGO      = "Enemigo";
    const string TAG_COLISIONABLE = "Colisionable";
    #endregion

    #region Eventos
    private void Awake()
    {
        Sonido = GetComponent<AudioSource>();

        // Configuración
        GetComponent<Rigidbody2D>().gravityScale    = EscalaGravedad;
        GetComponent<CapsuleCollider2D>().isTrigger = IsTrigger;
    }
    private void Update()
    {
        // Después de colisionar se destruye la bala cuando ya se ha reproducido su sonido
        if (destruir) tiempoDeAudioReproducido += Time.deltaTime;
        if (!destruir || tiempoDeAudioReproducido < Sonido.clip.length) return;

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(TAG_ENEMIGO) && !collision.CompareTag(TAG_COLISIONABLE)) return;

        // Cuando colisiona se encoge la bala a 0
        // para que antes de destruirla le de tiempo a reproducir su sonido
        transform.localScale = Vector3.zero;

        destruir = true;
    }
    #endregion

    #region Métodos
    public void Disparar()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Velocidad;
        Destroy(gameObject, TiempoDeVida);
    }
    #endregion
}