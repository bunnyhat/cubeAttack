using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class PlayerAttributes {
	public string playerName;
	public Color playerColor;
	public float playerHealth;
	public float playerSpeed;
	public Transform bulletSpawner;
	public GameObject bullet;
	public string facingDirection;
	public float bulletSpeed;
	public float fireRate, resetFireRate;
	private bool isDead = false;
	public Animator animator;
}

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private PlayerAttributes m_playerAttr;

	MeshRenderer m_meshRenderer;
	Rigidbody m_rigidbody;

	void Awake() {
		m_meshRenderer = GetComponent<MeshRenderer>();
		m_rigidbody = GetComponent<Rigidbody>();
		m_playerAttr.animator = GetComponent<Animator>();

		m_playerAttr.playerColor = m_meshRenderer.material.color;
		m_playerAttr.playerHealth = 100;
		m_playerAttr.facingDirection = "Right";
	}

	void Update() {
		
		if(m_playerAttr.playerName == "Player1") {
			float moveHorizontal = Input.GetAxisRaw("Horizontal");
			float moveVertical = Input.GetAxisRaw("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			m_rigidbody.velocity = movement.normalized * m_playerAttr.playerSpeed;
			
			// Debug.Log(movement);
			if(movement.x < 0) { m_playerAttr.facingDirection = "Left"; }
			if(movement.x > 0) { m_playerAttr.facingDirection = "Right"; }
			if(movement.z < 0) { m_playerAttr.facingDirection = "Down"; }
			if(movement.z > 0) { m_playerAttr.facingDirection = "Up"; }

			if(movement.z > 0 && movement.x > 0) { // UP RIGHT
				m_playerAttr.facingDirection = "Up Right";
				m_playerAttr.animator.SetFloat("UpDown", 0.5f);
				m_playerAttr.animator.SetFloat("LeftRight", 0.5f);
			}
			if(movement.z > 0 && movement.x < 0) { // UP LEFT
				m_playerAttr.facingDirection = "Up Left";
				m_playerAttr.animator.SetFloat("UpDown", -0.5f);
				m_playerAttr.animator.SetFloat("LeftRight", 0.5f);
			}
			if(movement.z < 0 && movement.x > 0) { // DOWN RIGHT
				m_playerAttr.facingDirection = "Down Right";
				m_playerAttr.animator.SetFloat("UpDown", 0.5f);
				m_playerAttr.animator.SetFloat("LeftRight", -0.5f);
			}
			if(movement.z < 0 && movement.x < 0) { // DOWN LEFT
				m_playerAttr.facingDirection = "Down Left";
				m_playerAttr.animator.SetFloat("UpDown", -0.5f);
				m_playerAttr.animator.SetFloat("LeftRight", -0.5f);
			}

			if(Input.GetKeyDown(KeyCode.Space)) {	
				m_playerAttr.fireRate -= Time.deltaTime;
				Debug.Log(m_playerAttr.fireRate);
				if(m_playerAttr.fireRate <= 0) { Shoot(); }
			}
		}

		if(m_playerAttr.facingDirection == "Left") { 
			m_playerAttr.animator.SetFloat("UpDown", -1);
			m_playerAttr.animator.SetFloat("LeftRight", 0);
		}
		if(m_playerAttr.facingDirection == "Right") { 
			m_playerAttr.animator.SetFloat("UpDown", 1);
			m_playerAttr.animator.SetFloat("LeftRight", 0);
		}
		if(m_playerAttr.facingDirection == "Down") { 
			m_playerAttr.animator.SetFloat("UpDown", 0);
			m_playerAttr.animator.SetFloat("LeftRight", -1);
		}
		if(m_playerAttr.facingDirection == "Up") { 
			m_playerAttr.animator.SetFloat("UpDown", 0);
			m_playerAttr.animator.SetFloat("LeftRight", 1);
		}
	}

	void Shoot() {
		if(m_playerAttr.playerName == "Player1") {
			GameObject bulletPrefab = Instantiate(m_playerAttr.bullet, m_playerAttr.bulletSpawner.position, m_playerAttr.bulletSpawner.rotation);
			
			if(m_playerAttr.facingDirection == "Left") { bulletPrefab.GetComponent<Rigidbody>().AddForce(Vector3.left * m_playerAttr.bulletSpeed); }
			if(m_playerAttr.facingDirection == "Right") { bulletPrefab.GetComponent<Rigidbody>().AddForce(Vector3.right * m_playerAttr.bulletSpeed); }
			if(m_playerAttr.facingDirection == "Up") { bulletPrefab.GetComponent<Rigidbody>().AddForce(Vector3.forward * m_playerAttr.bulletSpeed); }
			if(m_playerAttr.facingDirection == "Down") { bulletPrefab.GetComponent<Rigidbody>().AddForce(-Vector3.forward * m_playerAttr.bulletSpeed); }

			if(m_playerAttr.facingDirection == "Up Right") { bulletPrefab.GetComponent<Rigidbody>().AddForce((Vector3.forward + Vector3.right) * m_playerAttr.bulletSpeed); }
			if(m_playerAttr.facingDirection == "Up Left") { bulletPrefab.GetComponent<Rigidbody>().AddForce((Vector3.forward + Vector3.left) * m_playerAttr.bulletSpeed); }
			if(m_playerAttr.facingDirection == "Down Right") { bulletPrefab.GetComponent<Rigidbody>().AddForce((-Vector3.forward + Vector3.right) * m_playerAttr.bulletSpeed); }
			if(m_playerAttr.facingDirection == "Down Left") { bulletPrefab.GetComponent<Rigidbody>().AddForce((-Vector3.forward + Vector3.left) * m_playerAttr.bulletSpeed); }
			
			Destroy(bulletPrefab, 1.5f);
		}
		m_playerAttr.fireRate = 0.015f;
	}

}
