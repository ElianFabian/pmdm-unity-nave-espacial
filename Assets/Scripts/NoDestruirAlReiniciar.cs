using UnityEngine;

// Si se va a usar este script en un GameObject éste debe no tener padre
// en caso contrario la función DondtDestroyOnLoad() no funcionará
// ya que si el padre es destruido el objeto que tenga este script también será destruido con su padre


/// <summary>
/// El GameObject que tenga este script no será destruido al reiniciar la escena
/// pero sí al cambiar a una escena diferente.
/// </summary>
public class NoDestruirAlReiniciar : MonoBehaviour
{
    static NoDestruirAlReiniciar Instancia;

    string escenaInicial;

    private void Awake()
    {
        escenaInicial = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        DontDestroyOnLoad(gameObject);

        if (Instancia == null)
        {
            Instancia = this;
        }
        else Destroy(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        var escenaActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (escenaActual != escenaInicial) Destroy(gameObject);
    }
}
