using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayerBehaviour : MonoBehaviour {
	public static int score = 0;
	public int scale = 1;
	public static int maxScore = 99999999;

	public float jumpSpeed    = 175f;
	public float forwardSpeed = 1f;
	public float maxSpeed = 25f;

	private CircleCollider2D myCollider;

	public float gainSize = 1f;
	public float gainMass = 0.2f;
	public float gainSpeed = 0.2f;

	public float loseSize = 0.2f;
	public float loseMass = 0.04f;
	public float loseSpeed = 1f;

	private float counter = 0f; 
	private float counterMass = 0f; 
	private float counterSpeed = 0f;

	bool didJump = false;
	Animator animator;
	public bool dead = false;
	public bool godMode = false;

	public AudioClip jump;
	public AudioClip absorb;
	public AudioClip squish;
	public AudioClip powerup;
	AudioSource audioSource;

	// Our audio mixer
	public AudioMixer audioMixer;

	void Start () {
		// Getting the master mixer
		AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");
		audioSource = GetComponent<AudioSource>();
		// And make sure we play through the proper mixer
		audioSource.outputAudioMixerGroup = audioMixGroup[0];
		animator = transform.GetComponentInChildren<Animator>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) didJump = true;
		score = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").transform.position.x) * scale;   
		if (score > maxScore) dead = true;
		if (Time.timeScale == 0 && dead) Destroy(gameObject);
	}

	void FixedUpdate () {
		if (dead) return;
		GetComponent<Rigidbody2D>().AddForce( Vector2.right * forwardSpeed );

		if (didJump) {
			GetComponent<Rigidbody2D>().AddForce( Vector2.up * jumpSpeed );
			audioSource.PlayOneShot(jump, 1F);
			if (GetComponent<Rigidbody2D> ().mass >= 1.04f) {
				counter = loseSize;
				counterMass = loseMass;
				GetComponent<Rigidbody2D> ().mass -= counterMass;
				transform.localScale -= new Vector3 (counter, counter, counter);
				counterMass = 0f;
				counter = 0f;
			}
			if (GetComponent<Rigidbody2D> ().velocity.x <= maxSpeed) {
				counterSpeed = gainSpeed;
				forwardSpeed += counterSpeed;
				counterSpeed = 0f;
			}
			didJump = false;
		}

		if (GetComponent<Rigidbody2D>().velocity.y > 0) {
			float angle = Mathf.Lerp (0, 90, (-GetComponent<Rigidbody2D>().velocity.y / 3f) );
			transform.rotation = Quaternion.Euler(0, 0, angle);
		} else {
			float angle = Mathf.Lerp (0, -90, (-GetComponent<Rigidbody2D>().velocity.y / 3f) );
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			if (godMode) return;
			Destroy (collision.gameObject);
			audioSource.PlayOneShot(absorb, 1F);
			counter = gainSize;
			counterMass = gainMass;
			GetComponent<Rigidbody2D>().mass += counterMass;
			transform.localScale += new Vector3 (counter, counter, counter);
			counter = 0f;
			counterMass = 0f;
			if (GetComponent<Rigidbody2D> ().velocity.x >= 2) {
				counterSpeed = loseSpeed;
				forwardSpeed -= counterSpeed;
				counterSpeed = 0f;
			}
		}
		if (collision.gameObject.tag == "Ground") {
			audioSource.PlayOneShot(squish, 1F);
			animator.SetTrigger("Death");
			dead = true;
		}
	}

	// Sets the animiation of our player - used primarily in the PowerUpBase and classes that inherit from that
	public void setAnimation( string animationKeyword )
	{
		animator.SetTrigger( animationKeyword );
	}
}