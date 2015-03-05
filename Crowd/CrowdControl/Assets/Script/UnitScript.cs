using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	[SerializeField]
	private Vector3 _direction = Vector3.zero;

	[SerializeField]
	private float _velocity = 1.5f;

	[SerializeField]
	private int _max_life;

	[SerializeField]
	private int _life_point;

	[SerializeField]
	private int _damage;

	private float waitTime = 10f;
	private float radius = 10f;
	private int layerMask = 1 << 8;
	private bool ownWill = false;
	private Transform target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine(LookAround());
	}

	IEnumerator LookAround() {
		//public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
		yield return new WaitForSeconds(waitTime);
		RaycastHit[] hit = Physics.SphereCastAll(this.transform.position, radius, transform.forward, Mathf.Infinity, layerMask);
		foreach(RaycastHit rh in hit){

			Debug.Log(rh);
		}

		print("WaitAndPrint " + Time.time);
	}

	public void moveUnit(){
		//Debug.Log( "vector de direction : "+_direction+" distance parcouru : "+_direction.magnitude);
		this.transform.position += this._direction*Time.deltaTime;
	}

	public void rotationUnit(){
		Quaternion targetRotation = Quaternion.LookRotation (_direction);
		float str = Mathf.Min (5 * Time.deltaTime, 1);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);
	}

	public void set_direction(Vector3 val){
		_direction = val;
	}

	public Vector3 get_direction(){
		return _direction;
	}

	public void set_velocity(float val){
		_velocity = val;
	}

	public float get_velocity(){
		return _velocity;
	}

	public void set_max_life(int val){
		_max_life = val;
	}

	public int get_max_life(){
		return _max_life;
	}

	public void set_life_point(int val){
		_life_point = val;
	}

	public int get_life_point(){
		return _life_point;
	}

	public void set_damage(int val){
		_damage = val;
	}

	public int get_damage(){
		return _damage;
	}

	public void setownWill(bool val){
		ownWill = val;
	}
	
	public bool getownWill(){
		return ownWill;
	}

	public void setTarget(Transform val){
		target = val;
	}
	
	public Transform getTarget(){
		return target;
	}
}
