using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isFacingRight = true;
    public float moveSpeed;
    public AudioSource MusicPlayer;
    public AudioSource LevelMusic;
    public AudioSource LevelMusic2;
    public AudioSource Boom;
    private Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Your movement logic here
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetFloat("Speed", Mathf.Abs(touchPos.x));
            if (touchPos.x < 0)
            {
                flipL();
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                flipR();
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }
    }

    private void flipL()
    {
        if (isFacingRight)
        {
            isFacingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void flipR()
    {
        if (!isFacingRight)
        {
            isFacingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            UI.instance.OpenEndScreen();
            LevelMusic.Stop();
            LevelMusic2.Stop();

            StartCoroutine(PlayBoomThenMusic());
        }
    }

    private IEnumerator PlayBoomThenMusic()
    {
        Boom.Play();

        // Wait until Boom finishes playing
        yield return new WaitWhile(() => Boom.isPlaying);

        MusicPlayer.Play();
    }
}
