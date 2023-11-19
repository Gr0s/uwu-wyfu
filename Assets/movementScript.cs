using UnityEngine;

public class movementScript : MonoBehaviour
{
    public float predkosc = 5f; // Pr�dko�� poruszania si� gracza
    public float silaSkoku = 10f; // Si�a skoku
    private Rigidbody2D rb;
    [SerializeField] private bool czySkacze = false;
    public LayerMask groundLayer; // Warstwa pod�o�a

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Poruszanie w lewo i w prawo
        float ruchX = Input.GetAxis("Horizontal") * predkosc;
        rb.velocity = new Vector2(ruchX, rb.velocity.y);

        // Wykrywanie pod�o�a za pomoc� raycasta
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // Rysowanie raycasta w trybie debugowania
        Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);

        // Skakanie
        if (hit.collider != null)
        {
            czySkacze = false; // Resetuj zmienn� czySkacze po kontakcie z pod�o�em
        }
        else
        {
            czySkacze = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !czySkacze)
        {
            rb.AddForce(Vector2.up * silaSkoku, ForceMode2D.Impulse);
        }
    }
}