using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CartelInteractivo : MonoBehaviour
{
    [Header("Texto del cartel")]
    [TextArea(3, 6)]
    public string textoDelCartel = "Aquí va el texto del cartel...";

    [Header("UI")]
    public GameObject panelTexto;
    public Text textoUI;

    // Aquí el indicador será tu sprite animado
    public GameObject indicadorInteractuar;

    [Header("Efecto de escritura")]
    public float velocidadTexto = 0.05f;

    [Header("Sonido")]
    public AudioSource audioHabla;   // AudioSource (SIN AudioClip)
    public AudioClip sonidoLetra;    // Clip del sonido de letra

    private bool puedeInteractuar = false;
    private Coroutine escritura;

    void Start()
    {
        panelTexto.SetActive(false);
        indicadorInteractuar.SetActive(false);
        textoUI.text = "";
    }

    void Update()
    {
        if (puedeInteractuar && Input.GetKeyDown(KeyCode.E))
        {
            if (!panelTexto.activeSelf)
            {
                AbrirCartel();
            }
            else
            {
                CerrarCartel();
            }
        }
    }

    void AbrirCartel()
    {
        panelTexto.SetActive(true);
        indicadorInteractuar.SetActive(false);
        textoUI.text = "";

        if (escritura != null)
            StopCoroutine(escritura);

        escritura = StartCoroutine(EscribirTexto());
    }

    void CerrarCartel()
    {
        panelTexto.SetActive(false);
        indicadorInteractuar.SetActive(true);

        if (escritura != null)
            StopCoroutine(escritura);

        audioHabla.Stop();
    }

    IEnumerator EscribirTexto()
    {
        foreach (char letra in textoDelCartel)
        {
            textoUI.text += letra;

            // 🔊 Solo suena en letras (no espacios)
            if (letra != ' ' && sonidoLetra != null)
            {
                audioHabla.PlayOneShot(sonidoLetra);
            }

            yield return new WaitForSeconds(velocidadTexto);
        }

        // 🔇 Al terminar todo el texto
        audioHabla.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeInteractuar = true;
            indicadorInteractuar.SetActive(true); // <--- aparece el sprite animado
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeInteractuar = false;
            indicadorInteractuar.SetActive(false); // <--- desaparece el sprite animado
            panelTexto.SetActive(false);

            if (escritura != null)
                StopCoroutine(escritura);

            audioHabla.Stop();
        }
    }
}



