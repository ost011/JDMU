using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [Header("ȭ��������")]
    [SerializeField]GameObject arrowPrefab = null;
    [Header("�ѱ���ġ")]
    [SerializeField] Transform firePos;
    [Header("ȭ��Ǯ����ü")]
    [SerializeField] Transform arrowSpawn;
    [Header("ȭ��Ǯ����ü")]
    [SerializeField] float lifeTime;
    float angle;            // ���콺 �Ÿ��κ��� �������
    GameObjectPool<Arrow> arrowPool;
    
    // Start is called before the first frame update
    void Start()
    {
        
        arrowPool = new GameObjectPool<Arrow>(10, () =>
        {
            Debug.Log(angle);
            var clone = Instantiate(arrowPrefab, firePos.position
                , Quaternion.identity);
            clone.transform.SetParent(arrowSpawn, false);
            var arrow = clone.GetComponent<Arrow>();
            return arrow;
        });

    }

    // Update is called once per frame
    void Update()
    {
        TryFire();
        LockOnMouse();
        //clone.transform.Translate(Vector2.up * curBow.speed * Time.deltaTime);
    }
    void LockOnMouse()
    {
        // ���콺�� Ȱ���� �Ÿ�
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // ���콺 ��ġ �������
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // ȸ����
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    /* Ȱ ���� */
    void CreateArrow()
    {
        var arrow = arrowPool.Dequeue();
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        arrow.transform.rotation = rotation;
        arrow.transform.position = firePos.position;
        arrow.gameObject.SetActive(true);
    }

    /* Ȱ �Ҹ� */
    public void RemoveArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrowPool.Enqueue(arrow);
        
    }

    /* ���콺 Ŭ���� */
    void TryFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ��¡������ isCharge
        }
        if (Input.GetMouseButtonUp(0))
        {
            CreateArrow();
        }
    }
}


