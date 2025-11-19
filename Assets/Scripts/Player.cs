using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private Vector3 destination;
    private float distancia;
    private bool bloquear;
    private Ray ray;
    private RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bloquear = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(distancia.ToString());

        if (Input.GetMouseButtonDown(0))
        {
            // Lanzamos un rayo desde el puntero del mouse en la cordenada Z
            ray = camera.gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.z));

            // Si golpea con algo se guarda en HIT
            if (Physics.Raycast(ray, out hit))
            {
                // Creamos la destinación con el objeto colisionado (terrain). La y es para que no suba ni baje el personaje.
                destination = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                // Desbloqueamos al player
                bloquear = false;
            }

            // Si no está bloqueado (se usa para que no haga la animación de andar cuando esté parado)
            if (!bloquear)
            {
                // Obtenemos la distancia hasta el destino
                distancia = Vector3.Distance(transform.position, destination);

                // Si la distancia es mayor que la apuntada en el NavMeshAgent   
                if (distancia > gameObject.GetComponent<NavMeshAgent>().stoppingDistance)
                {
                    // Vamos al destino
                    gameObject.GetComponent<Animator>().SetBool("Andar", true);
                    gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
                }
                else
                {
                    // Al llegar al destino lo volvemos a bloquear, lo posicionamos exactamente en el destino y paramos la animación
                    transform.position = destination;
                    gameObject.GetComponent<Animator>().SetBool("Andar", false);
                    bloquear = true;
                }
            }

        }
    }
}
