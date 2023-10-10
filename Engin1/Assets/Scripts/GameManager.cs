using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;

	private List<GameState> m_possibleStates;


	public static GameManager Instance
    {
		get
		{
			if (s_instance == null)
			{
				Debug.Log("Game Manager is Null");
			}

			return s_instance;	   
		}
	}

	private void Awake()
	{
		s_instance = this;

		m_possibleStates = new List<GameState>();

		//m_possibleStates.Add(new GameplayState());
		//m_possibleStates.Add(new CinematicState());
		//m_possibleStates.Add(new SceneTransitionState());

		foreach (GameState state in m_possibleStates)
		{
			state.OnStart(this);
		}
	}

}
