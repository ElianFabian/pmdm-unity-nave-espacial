using UnityEngine;

[DisallowMultipleComponent]
public class EmisorDeEnemigos : MonoBehaviour
{
    [SerializeField] Enemigo enemigo;

    private void Start()
    {
        StartCoroutine(Co_EmitirEnemigos());
    }

    System.Collections.IEnumerator Co_EmitirEnemigos()
    {
        while (true)
        {
            Instantiate(enemigo, transform.position, enemigo.transform.rotation);

            var segundosAleatorios = Random.Range(2.0f, 5.0f);

            yield return new WaitForSeconds(segundosAleatorios);
        }
    }
}
