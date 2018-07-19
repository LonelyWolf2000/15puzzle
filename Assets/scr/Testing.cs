using System.Collections;
using System.Collections.Generic;
using puzzle15;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private ILogic _logic;
	// Use this for initialization
	void Start ()
	{
	    _logic = new Logic();
	    _logic.InitField(4);

	    Debug.Log(_logic.MoveChip(new Chip(3, 2)));
	    Debug.Log(_logic.MoveChip(new Chip(0, 1)));

        if (_logic.CheckWin())
            Debug.Log("Win!!!");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
