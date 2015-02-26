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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		charge(_army2, _army1);
	}

	public void charge(ArmyScript army, ArmyScript enemies){
		float y_formationKnight, x_formationKnight, y_formationSoldiers, x_formationSoldiers;
		Vector3 v1,v2,v3,v4,v5, v6;
		if(army.get_army_Soldiers().Count < army.get_army_Knight().Count){
			if(army.get_army_Knight().Count < 8){
				y_formationKnight = _unitSize;
				x_formationKnight = 8 * _unitSize;
			}else{

			}
		}
		foreach(UnitScript unit in army.get_army_Knight()){

			v3 = keepDistance(unit, army.get_army_Knight(),1.5f);
			//keepDistance(unit, enemies.get_army_Knight(),1);
			v3 += keepDistance(unit, army.get_army_Soldiers(),1.5f);
			//keepDistance(unit, enemies.get_army_Soldiers(),1);

			v5 = bound_position(unit, this._limitCoordXmax, this._limitCoordXmin, this._limitCoordYmax, this._limitCoordYmin);

			if(unit.getownWill() == false){
			v1 = gravitate(unit, army.get_army_Knight());
			v1 += gravitate(unit, army.get_army_Soldiers());

			v2 = matchSpeed(unit, army.get_army_Knight());
			v2 += matchSpeed(unit, army.get_army_Soldiers());

			v4 = tendToward(unit, Vector3.zero);

			v6 = bound_position(unit, army.get_army_Knight()[0].transform.position.x+10, army.get_army_Knight()[0].transform.position.x-10,
			                    army.get_army_Knight()[0].transform.position.z+10, army.get_army_Knight()[0].transform.position.z-10);

			unit.set_direction(v2+v3+v5+v6);
			limit_velocity(unit);

			unit.moveUnit();
			}else{//se dirige vers ca cible
				v4 = tendToward(unit, unit.getTarget().position);
			}
		}

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
			actualUnit.set_direction((actualUnit.get_direction()/actualUnit.get_direction().magnitude)*0.05f);
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

	void test(){
		//foreach(UnitScript unit in _army2){
			 
		//}
	}

}
