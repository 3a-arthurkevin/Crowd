using UnityEngine;
using System.Collections;

public class UnitMoveScript : MonoBehaviour 
{

    [SerializeField]
    Transform _unitTransform;

    [SerializeField]
    float _lookRange;

    [SerializeField]
    int _layerEnemies;

    [SerializeField]
    int _layerAllies;

    [SerializeField]
    float _timeBetweenLook;

    [SerializeField]
    float _moveSpeed;

    [SerializeField]
    float _rotationSpeed;

    [SerializeField]
    float _minDistanceBetweenUnitAndTarget;

    [SerializeField]
    float _minDistanceBetweenAllies;

    [SerializeField]
    float _randomPositionFactor;

    [SerializeField]
    float _timeBetweenRandom;

    [SerializeField]
    GameObject _target;

    Vector3 _random;

	Animator anim;

	// Use this for initialization
	void Start () 
    {
        _random = Vector3.zero;
		anim = GetComponent<Animator>();
        StartCoroutine(SetRandom());

        StartCoroutine(LookingForTarget());
	}
	void Awake() {
		anim = GetComponent<Animator>();
	}

    void OnEnable()
    {

    }
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void ApplyMove(Vector3 houndTarget)
    {
		anim.SetInteger("UnitAction", 4);
        if (_target != null)
        {
            if (_target.activeSelf)
                UnitMove();
            else
            {
                _target = null;
                HoundMove(houndTarget);
            }
        }
        else
            HoundMove(houndTarget);
    }

    void UnitMove()
    {
        Vector3 currentPosition = new Vector3(_unitTransform.position.x, _unitTransform.position.y, _unitTransform.position.z);
        Vector3 targetPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, _target.transform.position.z) + _random;

        if (Vector3.Distance(_unitTransform.position, _target.transform.position) > _minDistanceBetweenUnitAndTarget)
            _unitTransform.position = Vector3.MoveTowards(currentPosition, targetPosition, (_moveSpeed*2) * Time.deltaTime);

        LookRotation(_target.transform.position);
    }

    void HoundMove(Vector3 target)
    {
        Vector3 currentPosition = new Vector3(_unitTransform.position.x, _unitTransform.position.y, _unitTransform.position.z);
        Vector3 targetPosition = new Vector3(target.x, target.y, target.z) + _random;
		anim.SetInteger("UnitAction", 2);
        LookRotation(targetPosition);

        _unitTransform.position = Vector3.MoveTowards(currentPosition, targetPosition, _moveSpeed * Time.deltaTime);
    }

    bool CheckAlliesInRange()
    {
        bool alliesInRange = false;
        Collider[] allies = Physics.OverlapSphere(_unitTransform.position, _minDistanceBetweenAllies, 1 << _layerAllies);

        if (allies.Length > 0)
            alliesInRange = true;

        return alliesInRange;
    }

    /*
    bool checkAlliesForward(out RaycastHit hit)
    {
        return Physics.Raycast(_unitTransform.position, _target.transform.position, out hit, _lookRange, 1 << (_layerAllies | _layerEnemies));
    }
    */

    /*
    Vector3 RepelPosition()
    {
        Vector3 freePosition = Vector3.zero;
        
        Collider[] allies = Physics.OverlapSphere(_unitTransform.position, _minDistanceBetweenAllies, 1 << _layerAllies);

        foreach (Collider collider in allies)
        {
            if (collider.gameObject.transform != transform)
            {
                if (Vector3.Distance(_unitTransform.position, collider.gameObject.transform.position) < _minDistanceBetweenAllies)
                    freePosition = freePosition - ((_unitTransform.position - collider.gameObject.transform.position)*2);
            }
        }
        
        return freePosition;
    }
    */

    void LookRotation(Vector3 targetToLook)
    {
        if ((targetToLook - _unitTransform.position) != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetToLook - _unitTransform.position);
            _unitTransform.rotation = Quaternion.Slerp(_unitTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    /*
    GameObject LookingForTheNearestEnemyInRange()
    {
        Collider[] enemies = Physics.OverlapSphere(_unitTransform.position, _lookRange, 1 << _layerEnemies);

        int length = enemies.Length;

        if (length > 1)
        {
            float minDistance = Vector3.Distance(_unitTransform.position, enemies[0].gameObject.transform.position);
            int minIndex = 0;
            for (int i = 0; i < length; ++i)
            {
                float distance = Vector3.Distance(_unitTransform.position, enemies[i].gameObject.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = i;
                }
            }
            return enemies[minIndex].gameObject;
        }
        else if (length > 0)
            return enemies[0].gameObject;

        return null;
    }
    */

    GameObject LookingForEnemyInRange()
    {
        Collider[] enemies = Physics.OverlapSphere(_unitTransform.position, _lookRange, 1 << _layerEnemies);
        
        int length = enemies.Length;

        if (length > 0)
            return enemies[Random.Range(0, length-1)].gameObject;

        return null;
    }

    IEnumerator LookingForTarget()
    {
        GameObject enemy = null;

        if (_target != null)
        {
            if (!_target.activeSelf)
                enemy = LookingForEnemyInRange();
        }
        else
            enemy = LookingForEnemyInRange();

        if(enemy != null)
            _target = enemy;

        yield return new WaitForSeconds(_timeBetweenLook);
        yield return StartCoroutine(LookingForTarget());
    }

    IEnumerator SetRandom()
    {
        _random = new Vector3(Random.Range(_randomPositionFactor / 2, _randomPositionFactor), 0, Random.Range(_randomPositionFactor / 2, _randomPositionFactor));

        yield return new WaitForSeconds(_timeBetweenRandom);
        yield return StartCoroutine(SetRandom());
    }
}
