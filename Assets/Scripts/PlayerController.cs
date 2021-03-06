using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

//namespace PlayerController {

public class PlayerController : NetworkBehaviour {

	private NetworkAnimator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
	[HideInInspector] public bool tocaChao = false;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public int pontuacaoPlayer = 0;

	public GameObject PortaAberta;
	public GameObject PortaFechada;
	public GameObject SinalVerde;
	public GameObject SinalVermelho;

	private float shootingRate = 0.2f;
	private float shootCooldown = 0f;
	public Transform SpawnBullet;
	public GameObject Bullet;
	public GameObject Grenade;
	private bool correndo = false;


	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;

	public Image vida;
	private MensagemControle MC;

	public AudioClip JumpSound;
	public AudioClip AlienSiren;
	public AudioClip Shooting;
	public AudioClip Explosion;

    public Image arma01;
    public Image arma02;

	

	// Use this for initialization
	void Start () {

        if (!isLocalPlayer)
        {
            return;
        }
		anim = GetComponent <NetworkAnimator> ();
		rb2d = GetComponent <Rigidbody2D> ();
		arma01.enabled = true;
		arma02.enabled = false;

		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}

	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }
		if (Input.GetKeyDown (KeyCode.X)) {
			arma01.enabled = !arma01.enabled;
			arma02.enabled = !arma02.enabled;
		}
		
		//Implementar Pulo Aqui! 
		tocaChao = Physics2D.Linecast (transform.position, posPe.position, 1 << LayerMask.NameToLayer ("Chao"));
		if ((Input.GetKeyDown (KeyCode.Space)) && (tocaChao)) {
			jump = true;
		}
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}

		//Programar o tiro Aqui!
		if (Input.GetKeyDown (KeyCode.Z) && tocaChao == true) {
			if (correndo == true) {
				anim.SetTrigger ("atiracorrendo");
			}
			else if (correndo == false){
				anim.SetTrigger("atiraparado");
			}
			shootCooldown -= shootingRate;
			if (arma01.enabled == true) {
				Fire ();
			} else if (arma02.enabled == true) {
				Fire2 ();
			}
		}
	}

	void FixedUpdate()
	{
        if (!isLocalPlayer)
        {
            return;
        }
		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
		if (translationX != 0 && tocaChao) {
			correndo = true;
			anim.SetTrigger ("corre");
		} else {
			correndo = false;
			anim.SetTrigger("parado");
		}
		if (jump) {
			anim.SetTrigger ("pula");
			rb2d.AddForce (new Vector2 (0f, ForcaPulo));
			AudioSource.PlayClipAtPoint (JumpSound, Vector3.zero);
			jump = false;
		}

		//Programar o pulo Aqui! 

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}

		if (pontuacaoPlayer == 3){

			pontuacaoPlayer++;
			AudioSource.PlayClipAtPoint (AlienSiren, Vector3.zero);
			PortaAberta.SetActive (true);
			PortaFechada.SetActive (false);
			SinalVerde.SetActive (true);
			SinalVermelho.SetActive (false);
		}

	}
	void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D colplayer)
	{
		if (colplayer.CompareTag ("Porta")) {
			Destroy (this.gameObject);
			//colplayer.gameObject.GetComponent<MensagemControle> ().Venceu();
		}
	}

	void Fire (){
		
		if (shootCooldown <= 0f && Bullet != null) {
			var cloneBullet = Instantiate (Bullet, SpawnBullet.position, Quaternion.identity) as GameObject;
			cloneBullet.transform.localScale = this.transform.localScale;
			AudioSource.PlayClipAtPoint (Shooting, Vector3.zero);
		}
	}

	void Fire2 (){

		if (shootCooldown <= 0f && Bullet != null) {
			var cloneGrenade = Instantiate (Grenade, SpawnBullet.position, Quaternion.identity) as GameObject;
			cloneGrenade.transform.localScale = this.transform.localScale;
			AudioSource.PlayClipAtPoint (Explosion, Vector3.zero);
		}
	}
	}