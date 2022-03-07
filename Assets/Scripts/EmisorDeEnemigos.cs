using UnityEngine;

[DisallowMultipleComponent]
public class EmisorDeEnemigos : MonoBehaviour
{
    #region Atributos
    [SerializeField] Enemigo enemigo;

    int numeroDeEnemigosMaximo;
    #endregion

    #region Métodos de Unity
    private void Start()
    {
        StartCoroutine(Co_EmitirEnemigos());

        numeroDeEnemigosMaximo = Random.Range(3, 8);
    }
    #endregion

    #region Corrutinas
    System.Collections.IEnumerator Co_EmitirEnemigos()
    {
        yield return new WaitForSeconds(Random.Range(4, 10));

        var i = 0;
        while (true)
        {
            i++;

            var nuevoEnemigo = Instantiate(enemigo, transform.position, enemigo.transform.rotation);

            nuevoEnemigo.OnMorir += () => ControladorJuego.Puntuacion++;

            yield return new WaitForSeconds(0.5f);

            if (i >= numeroDeEnemigosMaximo)
            {
                i = 0;
                yield return new WaitForSeconds(Random.Range(3, 5));

                numeroDeEnemigosMaximo = Random.Range(3, 8);
            }
        }
    }
    #endregion
}
