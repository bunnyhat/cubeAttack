using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class PlayerAttributes {
	public string playerName;
	public Color playerColor;
	public float playerHealth;
	public float playerSpeed;
	public float fireRate;
	private bool isDead = false;
}

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private PlayerAttributes m_playerAttr;

	MeshRenderer m_meshRenderer;
	Rigidbody m_rigidbody;

	void Awake() {
		m_meshRenderer = GetComponent<MeshRenderer>();
		m_rigidbody = GetComponent<Rigidbody>();

		m_playerAttr.playerColor = m_meshRenderer.material.color;
		m_playerAttr.playerHealth = 100;
	}

	void Update() {
		if(m_playerAttr.playerName == "Player1") {
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			m_rigidbody.velocity = movement * m_playerAttr.playerSpeed;
		}
	}

}
