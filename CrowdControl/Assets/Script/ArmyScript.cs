using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmyScript : MonoBehaviour {

	[SerializeField]
	private List<UnitScript>_army_Soldiers;

	[SerializeField]
	private List<UnitScript>_army_Knight;

	[SerializeField]
	private int _status;

	public int rulePosition;

	public Vector3 coordGlobalArmy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void firstPlacementArmy(){
		if(_army_Soldiers.Count < _army_Knight.Count){
			if(_army_Knight.Count < 8){
				rulePosition = 8;
			}else{
				rulePosition = (int) Mathf.Sqrt(_army_Knight.Count);
				Debug.Log("Knight army : "+rulePosition);
			}
		}else{
			if(_army_Soldiers.Count < 8){
				rulePosition = 8;
			}else{
				rulePosition = (int) Mathf.Sqrt(_army_Soldiers.Count);
				Debug.Log("soldier army : "+rulePosition);
			}
		}
	}



	public void set_army_Soldiers(List<UnitScript> val){
		_army_Soldiers = val;
	}
	
	public List<UnitScript> get_army_Soldiers(){
		return _army_Soldiers;
	}

	public void set_army_Knight(List<UnitScript> val){
		_army_Knight = val;
	}
	
	public List<UnitScript> get_army_Knight(){
		return _army_Knight;
	}

	public void set_status(int val){
		_status = val;
	}
	
	public int get_status(){
		return _status;
	}
}
