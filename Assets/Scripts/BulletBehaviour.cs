using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	public float damage;
	PlayerMovement m_playerMovement;

	MeshRenderer m_meshRenderer;

	void Awake() {
		m_playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		m_meshRenderer = GetComponent<MeshRenderer>();

	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "Wall") {
			Destroy(this.gameObject);
		}

		if(other.gameObject.name == "Player1") {
			m_playerMovement.m_playerAttr.playerHealth -= damage;
		}
		if(other.gameObject.name == "Player2") {
			m_playerMovement.m_playerAttr.playerHealth -= damage;
		}
		if(other.gameObject.name == "Player3") {
			m_playerMovement.m_playerAttr.playerHealth -= damage;
		}
		if(other.gameObject.name == "Player4") {
			m_playerMovement.m_playerAttr.playerHealth -= damage;
		}
	}

}
