using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shuriken : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float maxRange = 10f;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        MoveBullet();
        CheckRange();
    }
    void MoveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    void CheckRange()
    {
        float distance = Vector3.Distance(transform.position, startPosition);
        if (distance >= maxRange)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            Destroy(gameObject);
        }
    }
}