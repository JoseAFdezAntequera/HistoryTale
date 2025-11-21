using UnityEngine;
using UnityEngine.AI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject winCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Funcion de cierre de inventario
    public void CloseInventary()
    {
        canvas.gameObject.SetActive(false);
        player.gameObject.GetComponent<Player>().bloquear = false;
    }

    public void Resume()
    {
        winCanvas.gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
