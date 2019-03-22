using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroller : MonoBehaviour {

    [SerializeField] float bgScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;

	
	void Start () {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0, bgScrollSpeed);
	}
		
	void Update () {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
	}
}
