using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Renderer renderer;
	private PlayerInput playerInput;

	public Pocket ShootingItemPocket;
	public float hitEffectTime = 1f;
    public bool IsMagnetic { get; set; } = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
		playerInput = GetComponent<PlayerInput>();

	}

	private void Start()
	{
		GameManager.Instance.Player = this;
	}

	private void Update()
	{
		
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
