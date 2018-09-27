using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelUI : MonoBehaviour {
    public float maxSpeed = 3f;
    public float maxJump = 3f;

    public Transform Player;
    public Rigidbody2D PlayerPhysics;
    private bool isFacingRight = true;

    public Animator anim;
    public GameObject[] pauseAndOther;

    public int DirInput;
    public bool isGrounded;
    public Transform GroundChecker;
    public LayerMask WhatIsGround;

    public void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundChecker.position, 0.1f, WhatIsGround);
        if (isGrounded && DirInput == 0) { anim.SetBool("isIdle", true); anim.SetBool("isRun", false); anim.SetBool("isJump", false); }
        if (DirInput == 1 && isGrounded || DirInput == -1 && isGrounded) { anim.SetBool("isRun", true); anim.SetBool("isIdle", false); anim.SetBool("isJump", false); } 
        if (!isGrounded) { anim.SetBool("isJump", true); anim.SetBool("isIdle", false); anim.SetBool("isRun", false); } 
        PlayerPhysics.velocity = new Vector2(maxSpeed * DirInput, PlayerPhysics.velocity.y);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseAndOther[0].SetActive(false);
        pauseAndOther[1].SetActive(true);

    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseAndOther[1].SetActive(false);
        pauseAndOther[0].SetActive(true);
    }
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Move(int Axis)
    {
        DirInput = Axis;
        if (isGrounded) { anim.SetBool("isRun", true); anim.SetBool("isIdle", false);}
         
    }

    public void Jumping()
    {
        if (isGrounded)
        {
            PlayerPhysics.AddForce(transform.up * maxJump, ForceMode2D.Impulse);// new Vector2(PlayerPhysics.velocity.x, maxJump);
        }   
    }

    public void Flip(bool right)
    {
        if (isFacingRight != right)
        {
            isFacingRight = right;
            Vector3 theScale = Player.localScale;
            theScale.x = theScale.x * -1;
            Player.localScale = theScale;
        }
    }
}
