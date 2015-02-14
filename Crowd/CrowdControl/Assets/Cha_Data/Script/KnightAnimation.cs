using UnityEngine;
using System.Collections;

public class KnightAnimation : MonoBehaviour {

	[SerializeField]
	private Animator anim;

	int jumpHash = Animator.StringToHash("Jump");
	int runStateHash = Animator.StringToHash("Base Layer.Run");
	
	
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool("is_attacking", true);
		}
		/*
		float move = Input.GetAxis ("Vertical");
		anim.SetFloat("Speed", move);

		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if(Input.GetKeyDown(KeyCode.Space) && stateInfo.nameHash == runStateHash)
		{
			anim.SetTrigger (jumpHash);
		}*/
	}

}
