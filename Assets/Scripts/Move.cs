using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using System;

public class Move : MonoBehaviour {

    private Rigidbody2D rg;
    private bool isGround = true;

    [SerializeField]
    private int jumpPower = 300;

	// Use this for initialization
	void Start () {

        rg = gameObject.GetComponent<Rigidbody2D>();

        this.UpdateAsObservable()
            .Where(x => Input.GetKey(KeyCode.D))
            .Subscribe(x => {
                var pos = gameObject.transform.position;
                pos.x += 0.05f;
                gameObject.transform.position = pos;
        });

		this.UpdateAsObservable()
			.Where(x => Input.GetKey(KeyCode.A))
			.Subscribe(x => {
				var pos = gameObject.transform.position;
				pos.x -= 0.05f;
				gameObject.transform.position = pos;
			});

        this.UpdateAsObservable()
            .Where(x => Input.GetKeyDown(KeyCode.Space))
            .Where(x => isGround)
            .Subscribe(x =>
            {
            rg.AddForce(Vector2.up * this.jumpPower);
        });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
