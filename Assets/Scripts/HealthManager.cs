using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public GameObject damageEffect;

    private int MaxHealth = 3;
    public int currentHealth;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite FullHeartSprite;
    [SerializeField] private Sprite EmptyHeartSprite;

    private GameObject Player;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        currentHealth = MaxHealth;
        DisplayHearts();
    }




    public void HurtPlayer()
    {
        if (currentHealth <= 0)
            return; // Empêche d'aller plus loin si déjà à 0

        currentHealth--;
        DisplayHearts();


        if (damageEffect != null && Player != null)
        {
            Instantiate(damageEffect, Player.transform.position, Quaternion.identity);
        }


        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");

            GameManager.instance.Death();
        }

        else
        {

            UIManager.instance.fadeToBlack = true;
            UIManager.instance.StartCoroutine(RespawnSequence());
        }
    }



    public void DisplayHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = FullHeartSprite;
            }
            else
            {
                hearts[i].sprite = EmptyHeartSprite;
            }
        }
    }


    private IEnumerator RespawnSequence()
    {
        yield return new WaitForSeconds(1f); // Laisse le fadeToBlack se faire

        Player.GetComponent<PlayerController>().Respawn();

        yield return new WaitForSeconds(0.5f); // Pause si tu veux

        UIManager.instance.fadeFromBlack = true;
    }

    

}
