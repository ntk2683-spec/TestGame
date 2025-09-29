using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1f;
    protected PlayerControl player;
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerControl>();
    }
    protected virtual void Update()
    {
        MoveToPlayer();
    }
    protected void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
    protected void FlipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }
}
