using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

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
    }

}
