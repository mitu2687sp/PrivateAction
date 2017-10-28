using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MessagePack;

public class Player : MonoBehaviour {

	Animator animator;
	Rigidbody2D rg;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rg = GetComponent<Rigidbody2D>();
		
		this.UpdateAsObservable()
			.Where(x => Input.GetKeyDown(KeyCode.Z))
			.Subscribe(x =>
			{
				animator.SetTrigger("Action");
				rg.velocity = new Vector2(4f, rg.velocity.y);
			});

		var mc = new MyClass
		{
			FirstName = "hoge",
			LastName = "fuga"
		};

		var bytes = MessagePackSerializer.Serialize(mc);
		var mc2 = MessagePackSerializer.Deserialize<MyClass>(bytes);

		print(bytes);
		var json = MessagePackSerializer.ToJson(bytes);

		var d = MessagePackSerializer.Deserialize<MyClass>(MessagePackSerializer.FromJson(@"{""hoge"":""foo"",""huga"":2000}"));
		print(d);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[MessagePackObject]
public class MyClass
{

	[Key(0)]
	public string FirstName { get; set; }

	[Key(1)]
	public string LastName { get; set; }

	// public members and does not serialize target, mark IgnoreMemberttribute
	[IgnoreMember]
	public string FullName { get { return FirstName + LastName; } }
}