﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
	public abstract class Page : MonoBehaviour {

		// Use this for initialization
		protected virtual void Start () {
			Hide();
		}
		
		// Update is called once per frame
		protected virtual void Update () {
			
		}

		protected void Show() {
			gameObject.SetActive(true);
		}

		public virtual void Hide() {
			gameObject.SetActive(false);
		}
	}
}