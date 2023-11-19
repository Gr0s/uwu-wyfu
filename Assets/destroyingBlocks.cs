using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyingBlocks : MonoBehaviour
{
    public float MiningDistance = 2.0f; // Ustaw odleg³oœæ, na jak¹ mo¿esz usuwaæ obiekty.
    private bool isMining = false; // Zmienna œledz¹ca, czy myszka jest przytrzymana.

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
                // Oblicz odleg³oœæ miêdzy obiektem, na którym jest skrypt, a trafionym obiektem.
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                if (distance <= MiningDistance)
                {
                    // Jeœli trafiony obiekt jest w odpowiedniej odleg³oœci, usuñ go.
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}