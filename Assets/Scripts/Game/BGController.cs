using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [Header("화면속도")]
    [SerializeField] float speed = 0.1f;

    SpriteRenderer sprRender;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        mat = sprRender.material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset += Vector2.up * speed * Time.deltaTime;
    }
}
