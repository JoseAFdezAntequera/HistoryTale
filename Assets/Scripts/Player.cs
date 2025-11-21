using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject canvas;

    private Vector3 destination;
    private float distancia;
    public bool bloquear;
    private Ray ray;
    private RaycastHit hit;
    private bool bloquearHabla;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bloquear = false;
        bloquearHabla = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Si no está hablando con un pj
        if (!bloquearHabla)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Lanzamos un rayo desde el puntero del mouse en la cordenada Z
                ray = camera.gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.z));

                // Si golpea con algo se guarda en HIT
                if (Physics.Raycast(ray, out hit) && !bloquear)
                {
                    switch (hit.collider.gameObject.tag)
                    {
                        case "Terreno":
                            // Creamos la destinación con el objeto colisionado (terrain). La y es para que no suba ni baje el personaje.
                            destination = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                            // Desbloqueamos al player
                            bloquear = false;

                            break;

                        case "PocionRoja" or "PocionAzul" or "PocionVerde":
                            destination = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                            bloquear = false;

                            break;
                    }
                }
            }

            // Si no está bloqueado (se usa para que no haga la animación de andar cuando esté parado)
            if (!bloquear)
            {
                // Obtenemos la distancia hasta el destino
                distancia = Vector3.Distance(transform.position, destination);

                // Si la distancia es mayor que la apuntada en el NavMeshAgent
                if (distancia > 0.5f)
                {
                    // Vamos al destino
                    gameObject.GetComponent<Animator>().SetBool("Andar", true);
                    gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
                }
                else
                {
                    // Al llegar al destino lo volvemos a bloquear, lo posicionamos exactamente en el destino y paramos la animación
                    gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
                    gameObject.GetComponent<Animator>().SetBool("Andar", false);

                    // Comprobamos el objeto en hit y lo añadimos al inventario
                    switch (hit.collider.gameObject.tag)
                    {
                        case "PocionRoja":
                            SetInventario(hit.collider.gameObject, 0);

                            break;

                        case "PocionAzul":
                            SetInventario(hit.collider.gameObject, 1);

                            break;

                        case "PocionVerde":
                            SetInventario(hit.collider.gameObject, 2);

                            break;
                    }

                }
            }

            // Hacemos seguir a la camara al player
            camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 5.0f);
        }
    }

    // Estas dos funciones son utilizadas por el Fungus para activar la conversación
    public void BloquearHabla()
    {
        bloquearHabla = true;
    }

    public void DesbloquearHabla()
    {
        bloquearHabla = false;
    }

    private void SetInventario(GameObject other, int pos)
    {
        // Lo bloqueamos para que no se mueva al dar al boton de cerrar inventario
        bloquear = true;

        // Ocultamos la pocion
        other.transform.parent.transform.parent.gameObject.SetActive(false);

        // Activamos el icono de la pocion correspondiente
        canvas.transform.GetChild(0).transform.GetChild(0).transform.GetChild(pos).gameObject.SetActive(true);

        // Abrimos el inventario
        if (!canvas.gameObject.activeSelf) canvas.gameObject.SetActive(true);
    }
}
