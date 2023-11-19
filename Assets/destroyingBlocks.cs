using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyingBlocks : MonoBehaviour
{
    public float MiningDistance = 2.0f;
    private bool isMining = false;
    private float miningTimer = 0f;
    private BlockScript currentTargetBlockScript; // Dodano referencjê do aktualnego obiektu docelowego.

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMining = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMining = false;
            miningTimer = 0f;
        }

        if (isMining)
        {
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
                            // Pobierz skrypt BlockScript z obiektu docelowego.
                            BlockScript blockScript = hit.collider.gameObject.GetComponent<BlockScript>();
                            if (blockScript != null)
                            {
                                // Jeœli kursor zosta³ przesuniêty na inny obiekt, zresetuj timer.
                                if (blockScript != currentTargetBlockScript)
                                {
                                    miningTimer = 0f;
                                    currentTargetBlockScript = blockScript; // Ustaw nowy obiekt docelowy.
                                }

                                // Odjêcie durability w zale¿noœci od czasu, przez który trzymany jest kursor.
                                miningTimer += Time.deltaTime;
                                float durabilityDecrease = miningTimer / blockScript.MiningTime;
                                blockScript.DecreaseDurability(durabilityDecrease);

                                // Jeœli durability osi¹gnie 0, zniszcz blok.
                                if (blockScript.CurrentDurability <= 0f)
                                {
                                    blockScript.DestroyBlock();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}