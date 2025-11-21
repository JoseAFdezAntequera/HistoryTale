using UnityEngine;
using Fungus;

public class Guerrero : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float distanciaHablar;

    private float distancia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        distancia = Vector3.Distance(transform.position, player.transform.position);

        if (distancia <= distanciaHablar)
        {
            // Si está a la distancia adecuada encaramos el personaje al player
            transform.LookAt(player.transform.position);
            player.transform.LookAt(transform.position);

            // Activamos el flowchart del personaje
            Fungus.Flowchart.BroadcastFungusMessage("Guerrero");
        }
    }
}
