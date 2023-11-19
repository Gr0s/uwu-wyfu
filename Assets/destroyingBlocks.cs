using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyingBlocks : MonoBehaviour
{
    public float MiningDistance = 2.0f; // Ustaw odleg�o��, na jak� mo�esz usuwa� obiekty.
    private bool isMining = false; // Zmienna �ledz�ca, czy myszka jest przytrzymana.

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMining = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMining = false;
        }

        if (isMining)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.CompareTag("Podloze"))
            {
                // Oblicz odleg�o�� mi�dzy obiektem, na kt�rym jest skrypt, a trafionym obiektem.
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                if (distance <= MiningDistance)
                {
                    // Je�li trafiony obiekt jest w odpowiedniej odleg�o�ci, usu� go.
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}