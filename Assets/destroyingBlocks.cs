using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyingBlocks : MonoBehaviour
{
    public float MiningDistance = 2.0f;
    public float MiningTime = 2.0f; // Czas potrzebny do zniszczenia obiektu
    private bool isMining = false;
    private float miningTimer = 0.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMining = true;
            miningTimer = 0.0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMining = false;
            miningTimer = 0.0f;
        }

        if (isMining)
        {
            miningTimer += Time.deltaTime;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Zniszczalny") && hit.collider.gameObject != this.gameObject)
                {
                    float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                    if (distance <= MiningDistance)
                    {
                        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (hit.collider.transform.position - transform.position).normalized, Vector2.Distance(transform.position, hit.collider.transform.position));

                        bool canDestroy = true;

                        foreach (var h in hits)
                        {
                            if (h.collider.CompareTag("Zniszczalny") || h.collider.CompareTag("Podloze"))
                            {
                                if (h.collider.gameObject != hit.collider.gameObject)
                                {
                                    canDestroy = false;
                                    break;
                                }
                            }
                        }

                        if (canDestroy)
                        {
                            // Zmniejszanie skali obiektu na podstawie miningTimer
                            float destructionProgress = Mathf.Clamp01(miningTimer / MiningTime);
                            hit.collider.transform.localScale = new Vector3(1.0f - destructionProgress, 1.0f - destructionProgress, 1.0f);

                            // Jeœli czas miningu przekroczy³ MiningTime, zniszcz obiekt
                            if (miningTimer >= MiningTime)
                            {
                                Destroy(hit.collider.gameObject);
                            }
                        }
                    }
                    else
                    {
                        // Resetuj timer, jeœli myszka zosta³a oddalona od obiektu
                        miningTimer = 0.0f;
                    }
                }
            }
        }
    }
}