using UnityEngine;
using UnityEngine.AI;

public class Peasant : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    private int wp;
    private Vector3 destino;
    private float distancia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        destino = new Vector3(waypoints[wp].transform.position.x, transform.position.y, waypoints[wp].transform.position.z);
        distancia = Vector3.Distance(transform.position, destino);

        if (distancia > 0.7f)
        {
            // Vamos al destino
            gameObject.GetComponent<Animator>().SetBool("Drunk", true);
            gameObject.GetComponent<NavMeshAgent>().SetDestination(destino);
        }
        else
        {
            destino = transform.position;
            wp++;
        }

        if (wp >= waypoints.Length) wp = 0;

        
    }
}
