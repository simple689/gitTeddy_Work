﻿using UnityEngine;  public class UI : MonoBehaviour {     public GameObject scenePrefab;      private UIRoot nguiRoot;     private GameObject scenePrefabObj;     private float timer = 1.0f;      void Start() {         nguiRoot = GameObject.FindObjectOfType<UIRoot>();         if (scenePrefabObj == null) {             scenePrefabObj = Instantiate(scenePrefab);             Vector3 localScale = scenePrefabObj.transform.localScale;             scenePrefabObj.transform.parent = nguiRoot.transform;             scenePrefabObj.transform.localScale = localScale;         }     }      void Update() {         timer -= Time.deltaTime;         if (timer <= 0) {
            Debug.Log(string.Format("Timer1 is up !!! time = ${0}", Time.time));
            timer = 1.0f;
        }     } }