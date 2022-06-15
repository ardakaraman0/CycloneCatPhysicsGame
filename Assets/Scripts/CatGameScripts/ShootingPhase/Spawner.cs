using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : PoolerBase<BulletMechanic>
{
	public static Spawner Instance { get; private set; }
	public virtual void Awake()
	{
		if (Instance == null)
		{
			Instance = this as Spawner;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	[SerializeField]
	BulletMechanic bulletPrefab;
	[SerializeField]
	int amountToPool;
	[SerializeField]
	int maxAmount;

	private void Start()
	{
		InitPool(bulletPrefab, amountToPool, maxAmount);
	}


}
