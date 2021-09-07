using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]float arrowSpeed = 10f;
    float moveAmount;

    BowController bow;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        bow = GameObject.FindGameObjectWithTag("Player").GetComponent<BowController>();

    }
    private void Update()
    {
        moveAmount = arrowSpeed * Time.deltaTime;
        var hit = Physics2D.Raycast(transform.position, Vector3.up, moveAmount, (1 << LayerMask.NameToLayer("BG_Collider")));
        if (hit.collider != null)
        {
            bow.RemoveArrow(this);
        }
        transform.Translate(Vector2.up * moveAmount);
    }
}
