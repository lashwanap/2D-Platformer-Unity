using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum pickupType { coin, gem, health }

    public pickupType pt;

    [SerializeField] GameObject PickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        switch (pt)
        {
            case pickupType.coin:
                GameManager.instance.IncrementCoinCount();
                player.score += 1;
                GameAudioManager.instance.PlayCoin();
                break;

            case pickupType.gem:
                GameManager.instance.IncrementGemCount();
                player.score += 1;
                player.UpdateScoreUI();
                GameAudioManager.instance.PlayCoin();
                break;

            case pickupType.health:
                // Si tu veux plus tard : player.RestoreHealth();
                break;
        }

        if (PickupEffect != null)
            Instantiate(PickupEffect, transform.position, Quaternion.identity);

        Destroy(this.gameObject, 0.2f);
    }
}
