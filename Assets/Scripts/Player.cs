using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Pocket ShootingItemPocket;
	public float hitEffectTime = 1f;
    public float shootingCoolTime = 30f;

    public bool IsMagnetic { get; set; } = false;

    private float lastShootingTime = 0f;
    private new SkinnedMeshRenderer renderer;
    private PlayerInput playerInput;
    private List<ShootingItem> currentShootingItem = new List<ShootingItem>();

    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		playerInput = GetComponent<PlayerInput>();
	}

	private void Start()
	{
		GameManager.Instance.Player = this;
        lastShootingTime = -shootingCoolTime;
        SetBroom();
	}

	private void Update()
	{
        if (playerInput.DoubleClick && lastShootingTime + shootingCoolTime < Time.time)
        {
            lastShootingTime = Time.time;
			for (int i = 0; i < currentShootingItem.Count; i++)
			{
                currentShootingItem[i].gameObject.SetActive(true);
				currentShootingItem[i].Action();
			}   
        }

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

    public void SetToyHammer()
    {
        currentShootingItem.Clear();
		currentShootingItem.Add(ItemManager.Instance.shootingItems[0].GetComponent<ToyHammer>());
    }

    public void SetRollingPin()
    {
		currentShootingItem.Clear();
		currentShootingItem.Add(ItemManager.Instance.shootingItems[1].GetComponent<RollingPin>());
	}

	public void SetBroom()
    {
		currentShootingItem.Clear();
		currentShootingItem.Add(ItemManager.Instance.shootingItems[2].GetComponent<Broom>());
		currentShootingItem.Add(ItemManager.Instance.shootingItems[3].GetComponent<Broom>());
	}

}
