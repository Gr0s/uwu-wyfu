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
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.isTrigger && hit.collider.CompareTag("Zniszczalny") && hit.collider.gameObject != this.gameObject)
                {
                    float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                    if (distance <= MiningDistance)
                    {
                        RaycastHit2D[] allHits = Physics2D.RaycastAll(transform.position, (hit.collider.transform.position - transform.position).normalized, distance);

                        bool canDestroy = true;

                        foreach (var h in allHits)
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
                            BlockScript blockScript = hit.collider.gameObject.GetComponent<BlockScript>();
                            if (blockScript != null)
                            {
                                if (blockScript != currentTargetBlockScript)
                                {
                                    miningTimer = 0f;
                                    currentTargetBlockScript = blockScript;
                                }

                                miningTimer += Time.deltaTime;
                                float durabilityDecrease = miningTimer / blockScript.MiningTime;
                                blockScript.DecreaseDurability(durabilityDecrease);

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