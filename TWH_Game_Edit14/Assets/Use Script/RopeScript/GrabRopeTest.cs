using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRopeTest : MonoBehaviour
{
    public Rigidbody2D _rb;
    private HingeJoint2D _hj;

    public float _pushForce = 10;

    public bool attacth = false;
    public Transform attachedTo;
    private GameObject disregard;
    public bool _isClimping;

    //public GameObject pulleySelected = null;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hj = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        CheckInputData();
        //CheckPulleyInput();
    }

    public void CheckInputData()
    {
        if(InputManager._climpWIsHeld && attacth)
        {
            Slide(1);
        }

        if (InputManager._climpSIsHeld && attacth)
        {
            Slide(-1);
        }

        if (InputManager._climpDIsHeld)
        {
            if (attacth)
            {
                _rb.AddRelativeForce(new Vector3(-1, 0, 0) * _pushForce);
            }
        }

        if (InputManager._climpAIsHeld)
        {
            if (attacth)
            {
                _rb.AddRelativeForce(new Vector3(1, 0, 0) * _pushForce);
            }
        }

        if (InputManager.JumpWasPressed && _isClimping == true)
        {
            Detach();
        }


    }

    public void Attach(Rigidbody2D RopeBone)
    {
        RopeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        _hj.connectedBody = RopeBone;
        _hj.enabled = true;
        attacth = true;
        _isClimping = true;
        attachedTo = RopeBone.gameObject.transform.parent;

    }

    void Detach()
    {
        _hj.connectedBody.GetComponent<RopeSegment>().isPlayerAttached = false;
        _hj.enabled = false;
        attacth = false;
        _isClimping = false;
        _hj.connectedBody = null;
    }

    public void Slide(int direction)
    {
        RopeSegment myConnection = _hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (direction > 0)
        {
            if (myConnection.connectedAbove != null)
            {
                if (myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }        
        }

        else
        {
            if(myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }

        if(newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            _hj.connectedBody = newSeg.GetComponent <Rigidbody2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!attacth)
        {
            if (collision.gameObject.CompareTag("Rope") /*&& InputManager.GrabWasPressed*/)
            {
                if(attachedTo != collision.gameObject.transform.parent)
                {
                    if (disregard == null || collision.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }






}
