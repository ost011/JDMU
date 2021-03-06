using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : new()
{
    public delegate T CreateFunc();

    int m_count;
    CreateFunc m_func;
    Queue<T> m_queue;

    public GameObjectPool(int count, CreateFunc func)
    {
        m_count = count;
        m_func = func;
        m_queue = new Queue<T>();
        Allocate();
    }
    void Allocate()
    {
        for (int i = 0; i < m_count; i++)
        {
            m_queue.Enqueue(m_func());
        }
    }
    void LocateOnce()
    {
        m_queue.Enqueue(m_func());
    }
    public T Dequeue()
    {
        if (m_queue.Count <= 0)
        {
            LocateOnce();
        }
        return m_queue.Dequeue();
    }
    public void Enqueue(T obj)
    {
        m_queue.Enqueue(obj);
    }
}
