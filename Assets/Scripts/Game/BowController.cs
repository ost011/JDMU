using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [Header("화살프리팹")]
    [SerializeField]GameObject arrowPrefab = null;
    [Header("총구위치")]
    [SerializeField] Transform firePos;
    [Header("화살풀링객체")]
    [SerializeField] Transform arrowSpawn;
    [Header("화살풀링객체")]
    [SerializeField] float lifeTime;
    float angle;            // 마우스 거리로부터 각도계산
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
        // 마우스와 활과의 거리
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // 마우스 위치 각도계산
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // 회전값
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    /* 활 생성 */
    void CreateArrow()
    {
        var arrow = arrowPool.Dequeue();
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        arrow.transform.rotation = rotation;
        arrow.transform.position = firePos.position;
        arrow.gameObject.SetActive(true);
    }

    /* 활 소멸 */
    public void RemoveArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrowPool.Enqueue(arrow);
        
    }

    /* 마우스 클릭시 */
    void TryFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 차징했을때 isCharge
        }
        if (Input.GetMouseButtonUp(0))
        {
            CreateArrow();
        }
    }
}


