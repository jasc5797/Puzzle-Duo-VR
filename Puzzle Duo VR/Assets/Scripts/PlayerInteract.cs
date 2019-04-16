using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public int MaxHealth = 3;

    public float KnockBackForce = 15.0f;
    public Slider slider;
    public float TimeBetweenDamage = 2.0f;
    public float TimeBetweenKnockback = 0.5f;

    public Canvas GameOverCanvas;

    private CharacterController CharacterController;

    private Vector3 impact = Vector3.zero;

    private float DamageTimer = 0.0f;
    private float KnockBackTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody.AddForce(new Vector3(1, 0, 0) * 50.0f);
        if (impact.magnitude > 0.2f)
        {
            CharacterController.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);

        if (DamageTimer > 0)
        {
            DamageTimer -= Time.deltaTime;
        }

        if(KnockBackTimer > 0)
        {
            KnockBackTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemy"></param>
    void TakeDamage(GameObject enemy)
    {
        if (DamageTimer <= 0) {
            slider.value--;
            if (slider.value == 0)
            {
                StartCoroutine(FailLevel());
            }

            Debug.Log("TakeDamage");
         
            DamageTimer = TimeBetweenDamage;
        }

        if (KnockBackTimer <= 0)
        {
            Vector3 direction = transform.position - enemy.transform.position;
            if (direction.y < 0)
            {
                direction.y = -direction.y;
            }
            impact += direction.normalized * KnockBackForce;

            KnockBackTimer = TimeBetweenKnockback;
        }
    }

    IEnumerator FailLevel()
    {
        GameOverCanvas.gameObject.SetActive(true);
       // GameOverCanvas.GetComponent<PanelFadeIn>().FadeIn();
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public bool CanBeDamaged()
    {
        return DamageTimer > 0;
    }

}
