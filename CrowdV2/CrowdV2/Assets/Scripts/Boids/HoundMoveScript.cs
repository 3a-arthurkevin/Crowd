using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class HoundMoveScript : MonoBehaviour 
{
    [SerializeField]
    string _armyName;

    [SerializeField]
    int _nbTotalUnits;

    [SerializeField]
    int _nbUnitInOneRow;

    [SerializeField]
    int _spaceBetweenUnit;

    [SerializeField]
    GameObject _unitPrefab;

    [SerializeField]
    HoundMoveScript _unitHoundScriptEnemyArmy;

    [SerializeField]
    Transform _unitTent;

    [SerializeField]
    Transform _spawnPoint;

    [SerializeField]
    Transform _enemyTent;

    [SerializeField]
    float _timeSpawn;

    [SerializeField]
    float _timeBetweenGetTarget;

    List<UnitMoveScript> _unitMoveScriptsArmy;

    Vector3 _target;

    int _nbUnitCreated;

    public int NbTotalUnits
    {
        get { return _nbTotalUnits; }
        set { _nbTotalUnits = value; }
    }

    public int NbUnitInOneRow
    {
        get { return _nbUnitInOneRow; }
        set { _nbUnitInOneRow = value; }
    }

    public List<UnitMoveScript> UnitMoveScriptsArmy
    {
        get { return _unitMoveScriptsArmy; }
        set { _unitMoveScriptsArmy = value; }
    }


	// Use this for initialization
	void Start () 
    {
        
	}
	
    void OnEnable()
    {
        _nbUnitCreated = 0;
        _unitMoveScriptsArmy = new List<UnitMoveScript>();

        StartCoroutine(InstanciateUnits());

        StartCoroutine(GetTargetArmyEnemy());
    }

	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("HoundMove - " + target);
	    foreach(UnitMoveScript unitMoveScript in _unitMoveScriptsArmy)
        {
            if(unitMoveScript.isActiveAndEnabled)
                unitMoveScript.ApplyMove(_target);
        }
	}

    IEnumerator GetTargetArmyEnemy()
    {
        List<UnitMoveScript> _enemiesUnitMoveScript = _unitHoundScriptEnemyArmy.UnitMoveScriptsArmy;

        if (_enemiesUnitMoveScript != null)
        {
            int count = _enemiesUnitMoveScript.Count;
            int nbUnit = 0;

            if (count > 0)
            {
                Vector3 centerOfGravity = Vector3.zero;
                foreach (UnitMoveScript enemyMoveScript in _enemiesUnitMoveScript)
                {
                    if (enemyMoveScript.isActiveAndEnabled)
                    {
                        centerOfGravity = centerOfGravity + enemyMoveScript.gameObject.transform.position;
                        ++nbUnit;
                    }
                }

                _target = (centerOfGravity / nbUnit);
            }

            if(nbUnit <= 0)
                _target = _enemyTent.position;
        }

        yield return new WaitForSeconds(_timeBetweenGetTarget);
        StartCoroutine(GetTargetArmyEnemy());
    }

    IEnumerator InstanciateUnits()
    {
        Vector3 initialPosition = _spawnPoint.position;
        int offset = _spaceBetweenUnit * (_nbUnitInOneRow / 2);
        initialPosition.z -= offset;

        Quaternion initialQuaternion = new Quaternion();
        initialQuaternion.eulerAngles = Vector3.zero;

        GameObject gameObject;

        for (int i = 0; i < _nbUnitInOneRow; ++i)
        { 
            gameObject = (GameObject.Instantiate(_unitPrefab, initialPosition, initialQuaternion) as GameObject);
            gameObject.name = _armyName + "_" + _nbUnitCreated;

            _unitMoveScriptsArmy.Add(gameObject.GetComponent(typeof(UnitMoveScript)) as UnitMoveScript);
            initialPosition.z += _spaceBetweenUnit;

            ++_nbUnitCreated;
            if (_nbUnitCreated >= _nbTotalUnits)
                break;
        }

        if (_nbUnitCreated < _nbTotalUnits - 1)
        {
            yield return new WaitForSeconds(_timeSpawn);
            StartCoroutine(InstanciateUnits());
        }
    }

}
