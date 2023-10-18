using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
	private Pocket ShootingItemPocket;
    public Conveyor conveyor;
    public ParticleSystem shootingEffect;
    public ParticleSystem confusionEffect;

	public float hitEffectTime = 1f;
    public float shootingCoolTime = 30f;
    public float shootingSpeed = 20f;
    public float shootingSpeedUpTime = 5f;

    public bool IsMagnetic { get; set; } = false;
    public bool IsHitEffect { get; private set; } = false;
    public bool IsShootingBuff { get; set; } = false;

    private float lastShootingTime = 0f;
	private new SkinnedMeshRenderer renderer;   //Component.renderer�� �̸��� ���Ƽ� new���
    private PlayerInput playerInput;
    private List<ShootingItem> currentShootingItem = new List<ShootingItem>();

    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		playerInput = GetComponent<PlayerInput>();
	}

	private void Start()
	{
        lastShootingTime = -shootingCoolTime;
        SetBroom();
	}

	private void Update()
	{
        if (!GameManager.Instance.IsGameover)
        {
            if ( playerInput.DoubleClick || Input.GetKeyDown(KeyCode.Space) && lastShootingTime + shootingCoolTime < Time.time)
            {
				lastShootingTime = Time.time;
				for (int i = 0; i < currentShootingItem.Count; i++)
				{
					currentShootingItem[i].gameObject.SetActive(true);
					currentShootingItem[i].Action();
					shootingEffect.Play();
					SpeedBuff(shootingSpeed, shootingSpeedUpTime);
				}
			}   
        }


        //test code
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetToyHammer();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SetRollingPin();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SetBroom();
        }
    }

	public void OnDamage(float damage)
    {
        GameManager.Instance.SetTimer(-damage);
        if (!IsHitEffect)
        {
			StartCoroutine(OnHitEffect());
		}
    }

    //��ü�� ���̴��� �����̽� �ɼ� ��尡 ��� ���Ұ�
    public IEnumerator OnHitEffect()
    {
        IsHitEffect = true;
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
        IsHitEffect = false;
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

    public void SpeedBuff(float speed, float time)
    {
		conveyor.SpeedBuff(speed, time);
    }
	public void SpeedDeBuff(float speed, float time)
	{
		conveyor.SpeedDeBuff(speed, time);
	}

}
