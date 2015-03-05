using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	[SerializeField]
	private ArmyScript _army1;

	[SerializeField]
	private ArmyScript _army2;

	[SerializeField]
	private float _limitCoordXmin = -200;

	[SerializeField]
	private float _limitCoordXmax = 200;

	[SerializeField]
	private float _limitCoordYmin = -200;

	[SerializeField]
	private float _limitCoordYmax = 200;

	[SerializeField]
	private float _unitSize = 2f;

	[SerializeField]
	private bool windowOpen = false;
	
	private int selectionGridInt = 0;
	private string[] selectionStrings = {"Flock", "Disperse", "line", "Arrow"};
	private Rect windowRect = new Rect(20, 20, 250, 400);
	private bool targeting = true;

	[SerializeField]
	GameObject prefabUnitKnight1;
	[SerializeField]
	GameObject prefabUnitKnight2;
	[SerializeField]
	GameObject prefabUnitSoldier1;
	[SerializeField]
	GameObject prefabUnitSoldier2;
	[SerializeField]
	Transform spawn1;
	[SerializeField]
	Transform spawn2;

	// Use this for initialization
	void Start () {
		_army2.firstPlacementArmy();
		_army1.firstPlacementArmy();
	}
	
	// Update is called once per frame
	void Update () {
		_army1.set_status(1);
		_army2.set_status(1);
		if(_army1.get_status() == 0){
			charge(_army1, _army2);
		}else{
			protect(_army1, _army2);
		}

		if(_army2.get_status() == 0){
			charge(_army2, _army1);
		}else{
			protect(_army2, _army1);
		}
	}

	public void charge(ArmyScript army, ArmyScript enemies){
		Vector3 v1,v2,v3,v4,v5, v6;
		Vector3 coordArmy = Vector3.zero;
		int index = 0;
		foreach(UnitScript unit in army.get_army_Soldiers()){
			index++;
			coordArmy += unit.transform.position;
			v3 = keepDistance(unit, army.get_army_Knight(),1.0f);//Boucle x 4
			v3 +=keepDistance(unit, enemies.get_army_Knight(),1f);
			v3 += keepDistance(unit, army.get_army_Soldiers(),1.0f);
			v3 +=keepDistance(unit, enemies.get_army_Soldiers(),1f);

			v5 = bound_position(unit, this._limitCoordXmax, this._limitCoordXmin, this._limitCoordYmax, this._limitCoordYmin);

			if(unit.getownWill() == false){
				
				//v2 = matchSpeed(unit, army.get_army_Knight());
				v2 = matchSpeed(unit, army.get_army_Soldiers());
				
				if(enemies.coordGlobalArmy != null)
				{
					v4 = tendToward(unit, enemies.coordGlobalArmy);
				}else
				{
					v4 = Vector3.zero;
				}
				
				//v6 = bound_position(unit, army.get_army_Knight()[0].transform.position.x+10, army.get_army_Knight()[0].transform.position.x-10,army.get_army_Knight()[0].transform.position.z+10, army.get_army_Knight()[0].transform.position.z-10);
				v6 = formationCharge(unit, army, index);
				
				unit.set_direction(v2+v3+v5+v6+v4);
				limit_velocity(unit);
				
				unit.moveUnit();
				unit.rotationUnit();
				
			}else{//L'unité agit independament de l'armée
				//v4 = tendToward(unit, unit.getTarget().position);
			}

		}

		foreach(UnitScript unit in army.get_army_Knight()){
			index ++;
			coordArmy += unit.transform.position;
			v3 = keepDistance(unit, army.get_army_Knight(),1.0f);//Boucle x 4
			v3 +=keepDistance(unit, enemies.get_army_Knight(),1f);
			v3 += keepDistance(unit, army.get_army_Soldiers(),1.0f);
			v3 +=keepDistance(unit, enemies.get_army_Soldiers(),1f);

			v5 = bound_position(unit, this._limitCoordXmax, this._limitCoordXmin, this._limitCoordYmax, this._limitCoordYmin);

			if(unit.getownWill() == false){

			//v2 = matchSpeed(unit, army.get_army_Knight());
			v2 = matchSpeed(unit, army.get_army_Soldiers());
			
			if(enemies.coordGlobalArmy != null)
			{
				v4 = tendToward(unit, enemies.coordGlobalArmy);
			}else
			{
				v4 = Vector3.zero;
			}

			//v6 = bound_position(unit, army.get_army_Knight()[0].transform.position.x+10, army.get_army_Knight()[0].transform.position.x-10,army.get_army_Knight()[0].transform.position.z+10, army.get_army_Knight()[0].transform.position.z-10);
			v6 = formationProtect(unit, army, index);

			unit.set_direction(v2+v3+v5+v6+v4);
			limit_velocity(unit);

			unit.moveUnit();
			unit.rotationUnit();

			}else{//L'unité agit independament de l'armée
				//v4 = tendToward(unit, unit.getTarget().position);
			}
		}
		army.coordGlobalArmy = coordArmy/index;
		army.firstPlacementArmy();
	}

	public void protect(ArmyScript army, ArmyScript enemies){
		Vector3 v1,v2,v3,v4,v5, v6;
		int index = 0;
		Vector3 coordArmy = Vector3.zero;

		foreach(UnitScript unit in army.get_army_Knight()){
			index ++;
			coordArmy += unit.transform.position;
			v3 = keepDistance(unit, army.get_army_Knight(),1.0f);
			v3 += keepDistance(unit, enemies.get_army_Knight(),1);
			v3 += keepDistance(unit, army.get_army_Soldiers(),1.0f);
			v3 += keepDistance(unit, enemies.get_army_Soldiers(),1);
			
			v5 = bound_position(unit, this._limitCoordXmax, this._limitCoordXmin, this._limitCoordYmax, this._limitCoordYmin);
			
			if(unit.getownWill() == false){
				
				v2 = matchSpeed(unit, army.get_army_Knight());
				v2 += matchSpeed(unit, army.get_army_Soldiers());
				v6 = formationProtect(unit, army, index);

				if(army.coordGlobalArmy != null)
				{
					v4 = tendToward(unit, army.coordGlobalArmy);
				}else
				{
					v4 = Vector3.zero;
				}
				
				unit.set_direction(v2+v3+v5+v6+v4);
				limit_velocity(unit);
				
				unit.moveUnit();
				unit.rotationUnit();
				
			}else{//se dirige vers ca cible
				//v4 = tendToward(unit, unit.getTarget().position);
			}
		}

		foreach(UnitScript unit in army.get_army_Soldiers()){
			index++;
			coordArmy += unit.transform.position;
			v3 = keepDistance(unit, army.get_army_Knight(),1.5f);//Boucle x 4
			v3 += keepDistance(unit, enemies.get_army_Knight(),1.5f);
			v3 += keepDistance(unit, army.get_army_Soldiers(),1.5f);
			v3 +=keepDistance(unit, enemies.get_army_Soldiers(),1.5f);
			
			v5 = bound_position(unit, this._limitCoordXmax, this._limitCoordXmin, this._limitCoordYmax, this._limitCoordYmin);
			
			if(unit.getownWill() == false){
				
				v2 = matchSpeed(unit, army.get_army_Knight());
				//v2 = matchSpeed(unit, army.get_army_Soldiers());
				
				v6 = formationProtect(unit, army, index);
				if(army.coordGlobalArmy != null)
				{
					v4 = tendToward(unit, army.coordGlobalArmy);
				}else
				{
					v4 = Vector3.zero;
				}

				unit.set_direction(v2+v3+v5+v6+v4);
				limit_velocity(unit);
				
				unit.moveUnit();
				unit.rotationUnit();
				
			}else{//L'unité agit independament de l'armée
				//v4 = tendToward(unit, unit.getTarget().position);
			}
			
		}
		army.coordGlobalArmy = coordArmy/index;
		army.firstPlacementArmy();
	}

	//RULES

	//rule 1 : gravitate 
	//An unit is attracted by his fellow comrade
	//Bug : make the unit move too much

	public static Vector3 gravitate(UnitScript actualUnit, List<UnitScript> army){
		Vector3 direction = Vector3.zero;
		foreach(UnitScript unit in army){
			if(actualUnit != unit){
				direction += actualUnit.transform.position;
			}
		}
		if(direction != Vector3.zero && army.Count != 0){
			direction /= (float)army.Count;
			return (direction - actualUnit.transform.position)/100;
		}
		//Debug.Log(direction);
		return Vector3.zero;
	}

	//rule 2 : match Speed 
	//An unit try to match the speed of his fellow comrade, and folow the same direction

	public Vector3 matchSpeed(UnitScript actualUnit, List<UnitScript> army){
		Vector3 direction = Vector3.zero;
		int count = 0;
		foreach(UnitScript unit in army){
			if(unit != actualUnit){
				direction += actualUnit.get_direction();
				count++;
			}
		}
		if(direction != Vector3.zero && army.Count != 0)
			direction/= (count);
		return (direction - actualUnit.get_direction())/8;
	}

	//rule 3 : keep distance 
	//An unit try to keep a minimum distance from his fellow comrade

	public static Vector3 keepDistance(UnitScript actualUnit, List<UnitScript> army, float distanceMinimum){
		Vector3 direction = Vector3.zero;
		foreach(UnitScript unit in army){
			if(Vector3.Distance(actualUnit.transform.position, unit.transform.position) < distanceMinimum){
				direction -= (unit.transform.position - actualUnit.transform.position);
			}
		}
		return direction;
	}

	//rule 4 : tend Toward
	//An unit try to go where the target is

	public static Vector3 tendToward(UnitScript actualUnit, Vector3 target){
		return (target - actualUnit.transform.position)/100;
	}

	//rule 5 : limit velocity
	//An unit can't go that fast!

	public void limit_velocity(UnitScript actualUnit){
		actualUnit.set_direction(new Vector3(actualUnit.get_direction().x,0, actualUnit.get_direction().z));
		if(actualUnit.get_direction().magnitude > actualUnit.get_velocity()){
			//Debug.Log(actualUnit.get_direction().magnitude);
			actualUnit.set_direction((actualUnit.get_direction()/actualUnit.get_direction().magnitude)*actualUnit.get_velocity());
		}
	}

	//rule 6 : bound position
	//An unit is restrained to an area of coord given

	public static Vector3 bound_position(UnitScript actualUnit, float squareLimit_Xmax, float squareLimit_Xmin, float squareLimit_Zmax, float squareLimit_Zmin){
		Vector3 vect = Vector3.zero;
		//Debug.Log(actualUnit.transform.position.x + " " +squareLimit_Xmin);
		if(actualUnit.transform.position.x < squareLimit_Xmin){
			vect.x = 10;
		}else{
			if(actualUnit.transform.position.x > squareLimit_Xmax){
				vect.x = -10;
			}
		}
		//y is for verticale so we don't need it
		if(actualUnit.transform.position.z < squareLimit_Zmin){
			vect.z = 10;
		}else{
			if(actualUnit.transform.position.z > squareLimit_Zmax){
				vect.z = -10;
			}
		}
		//Debug.Log("vect" + vect);
		return vect;
	}

	public Vector3 formationProtect(UnitScript actualUnit, ArmyScript army, int index){
		//Debug.Log("FORMATION army.rulePosition : "+army.rulePosition);
		Vector3 direction = Vector3.zero;
		if(army.get_army_Knight().Count > index){
			direction = army.get_army_Knight()[0].transform.position + Vector3.left * 2 * ((index-1) % army.rulePosition) + Vector3.forward * -1 * 2 * ((index-1) / army.rulePosition);
			return (direction - actualUnit.transform.position)/10;
		}else{
			if(army.get_army_Soldiers().Count > index){
				direction = army.get_army_Knight()[0].transform.position + Vector3.left * 2 * ((index-army.get_army_Knight().Count) % army.rulePosition) + Vector3.forward * -1 * 2 * ((index-army.get_army_Knight().Count) / army.rulePosition)+ Vector3.forward * -2 ;
				return (direction - actualUnit.transform.position)/10;
			}
		}
		return direction;
	}

	public Vector3 formationCharge(UnitScript actualUnit, ArmyScript army, int index){
		//Debug.Log("FORMATION army.rulePosition : "+army.rulePosition);
		Vector3 direction = Vector3.zero;
		if(army.get_army_Soldiers().Count > index){
			direction = army.get_army_Soldiers()[0].transform.position + Vector3.left * 2 * ((index-1) % army.rulePosition) + Vector3.forward * -1 * 2 * ((index-1) / army.rulePosition);
			return (direction - actualUnit.transform.position)/10;
		}else{
			if(army.get_army_Knight().Count > index){
				direction = army.get_army_Soldiers()[0].transform.position + Vector3.left * 2 * ((index-army.get_army_Soldiers().Count) % army.rulePosition) + Vector3.forward * -1 * 2 * ((index-army.get_army_Soldiers().Count) / army.rulePosition)+ Vector3.forward * -2 ;
				return (direction - actualUnit.transform.position)/10;
			}
		}
		return direction;
	}

	//interface

	void OnGUI(){
		if(windowOpen){
			windowRect = GUI.Window(0, windowRect, boidManagerWindow, "Army manager");
		}else{
			if(GUI.Button (new Rect (10,10,100,20), "Show")){
				windowOpen = true;
			}
		}
	}

	public void boidManagerWindow(int windowID){
		GUI.Label(new Rect(25, 25, 100, 20), "Army :");
		GUI.DragWindow(new Rect(0, 0, 10000, 20));
		if(GUI.Button (new Rect (25,50,100,20), "Hide")){
			windowOpen = false;
		}
		if(GUI.Button (new Rect (25,75,100,20), "+ Knight1")){
			GameObject clone_go = Instantiate(prefabUnitKnight1, spawn1.position, spawn1.rotation) as GameObject;
			UnitScript clone_sc = clone_go.GetComponent<UnitScript>();//Didn't found another way for the creation
			_army1.get_army_Knight().Add(clone_sc);
		}

		if(GUI.Button (new Rect (25,100,100,20), "+ Soldier1")){
			GameObject clone_go = Instantiate(prefabUnitSoldier1, spawn1.position, spawn1.rotation) as GameObject;
			UnitScript clone_sc = clone_go.GetComponent<UnitScript>();//Didn't found another way for the creation
			_army1.get_army_Soldiers().Add(clone_sc);
		}

		if(GUI.Button (new Rect (25,125,100,20), "+ Knight2")){
			GameObject clone_go = Instantiate(prefabUnitKnight2, spawn2.position, spawn2.rotation) as GameObject;
			UnitScript clone_sc = clone_go.GetComponent<UnitScript>();//Didn't found another way for the creation
			_army2.get_army_Knight().Add(clone_sc);
		}

		if(GUI.Button (new Rect (25,150,100,20), "+ Soldier2")){
			GameObject clone_go = Instantiate(prefabUnitSoldier2, spawn2.position, spawn2.rotation) as GameObject;
			UnitScript clone_sc = clone_go.GetComponent<UnitScript>();//Didn't found another way for the creation
			_army2.get_army_Soldiers().Add(clone_sc);
		}

		
		/*if(GUI.Button (new Rect (50,150,20,20), "-")){
			int index = spaceFleet.Count-1;
			BoidScript temp = spaceFleet[index];
			spaceFleet.RemoveAt(index);
			temp.destroyItself();
		}
		GUI.Label(new Rect(25, 175, 200, 20), "Fleet number : "+spaceFleet.Count);*/
	}

}
