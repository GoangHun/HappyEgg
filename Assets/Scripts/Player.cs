using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Renderer renderer;


    public Pocket ShootingItemPocket;
    public float hitEffectTime = 1f;
    public bool IsMagnetic { get; set; } = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

	private void Start()
	{
		GameManager.Instance.Player = this;
	}


	public void OnDamage(float damage)
    {
        GameManager.Instance.SetTimer(-damage);
    }

    public IEnumerator OnHitEffect()
    {
        float time = Time.time + hitEffectTime;
        var color = renderer.material.color;
        while (time > Time.time)
        {
            color.a = 0.3f;
            renderer.material.color = color;
            
            yield return new WaitForSeconds(0.1f);

            color.a = 1f;
            renderer.material.color = color;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
