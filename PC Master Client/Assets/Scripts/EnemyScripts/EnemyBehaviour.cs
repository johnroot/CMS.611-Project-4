﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    public int damage;

    Vector3 corePosition;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        try
        {
            corePosition = GameObject.FindGameObjectWithTag("Core").transform.position;
        }
        catch (NullReferenceException)
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = corePosition - transform.position;
        rb.AddForce(direction.normalized * speed, ForceMode2D.Force);
		float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.velocity = rb.velocity.magnitude > speed ? rb.velocity.normalized * speed : rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
			
            //collision.gameObject.GetComponent<CoreBehaviour>().Damage(damage);
			var behaviour = collision.gameObject.GetComponent<CoreBehaviour>();
			if (null == behaviour) {
				collision.gameObject.GetComponent<RotatingCoreBehaviour> ().Damage(damage);
			} else {
				
				behaviour.Damage (damage);
			} 

            Destroy(gameObject);
        }
    }
}