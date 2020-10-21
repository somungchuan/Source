using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
	
    // 单例模式：本类内部创建对象实例
    private static ObjectPool Instance = new ObjectPool();

	// 构造器私有化，外部不能new
	private ObjectPool()
	{
		
	}

	// 提供一个公有的静态方法，返回实例对象
	public static ObjectPool GetInstance()
	{
		return Instance;
	}

	public GameObject GetObject(string name, Vector3 position, Quaternion rotation)
    {
		Debug.Log("In here");
		GameObject laserPrefab = Resources.Load("Prefabs/" + name) as GameObject;
		return Object.Instantiate(laserPrefab, position, rotation);
    }

	public void ReleaseObject(GameObject obj)
    {
		Object.Destroy(obj);
    }
}
