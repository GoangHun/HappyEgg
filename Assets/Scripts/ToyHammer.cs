using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyHammer : ShootingItem
{
    private Animator animator;
    private void Awake()
    {
        onAction = () =>
        {
			StartCoroutine(Swing());
        };

		animator = GetComponent<Animator>();
	}

    private IEnumerator Swing()
    {
		gameObject.SetActive(true);

		while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
			yield return null;
		}

		ObstacleManager.Instance.AllSmash();
		gameObject.SetActive(false);
	}

}

