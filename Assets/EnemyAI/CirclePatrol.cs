﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
	public class CirclePatrol : ScriptBase
	{
		public float moveSpeed = 2.5f;
		public float rotateSpeed = 90.0f;
		
		void Start () 
		{
			Debug.Log("Start CirclePatrol");
			StartCoroutine(Execute());
		}
		public IEnumerator Execute()
		{
			while (true)
			{
				float moveRate = moveSpeed * Time.deltaTime;

				float spinRate = rotateSpeed * Time.deltaTime;

				transform.Rotate(Vector3.forward * spinRate);

				transform.position += transform.up * moveRate;

				yield return null;
			}
		}
	}
}