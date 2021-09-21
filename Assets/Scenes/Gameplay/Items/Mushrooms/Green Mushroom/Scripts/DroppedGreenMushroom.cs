using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedGreenMushroom : GreenMushroom
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float minDistance = 0.1f;
    [SerializeField] private float delayTime = 0.01f;

    protected override void OnEnable()
    {
        GameManager.Instance.onResetItems += ResetItem;
    }

    protected override void ResetItem()
    {
        GameManager.Instance.onResetItems -= ResetItem;
        Destroy(gameObject);
    }

    public override void Collect()
    {
        StartCoroutine(MoveTowardsPlayer());

        IEnumerator MoveTowardsPlayer()
        {
            yield return new WaitForSeconds(delayTime);

            while(Vector2.Distance(transform.position, player.position) > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                yield return null;
            }
            base.Collect();

            ResetItem();
        }
    }
}
