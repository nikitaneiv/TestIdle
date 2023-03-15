using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public event Action InstantiateGold;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Log>())
        {
            InstantiateGold?.Invoke();
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 1.5f);
        }
    }
    
}