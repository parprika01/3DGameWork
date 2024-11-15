using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 该组件用于实现全局的游戏对象
public class GlobalControl: MonoBehaviour {
	protected static GlobalControl instance;
	void Awake() {
		if (instance) {
			Destroy(gameObject);
		} 
		else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
}
