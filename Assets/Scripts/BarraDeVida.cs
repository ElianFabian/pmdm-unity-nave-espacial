using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField] JugadorNave jugador;
    Image imgBarraDeVida;

    float vidaInicial;

    private void Awake()
    {
        imgBarraDeVida = GetComponent<Image>();

        vidaInicial = jugador.Vida;
    }

    // Update is called once per frame
    void Update()
    {
        imgBarraDeVida.fillAmount = jugador.Vida / vidaInicial;
    }
}
