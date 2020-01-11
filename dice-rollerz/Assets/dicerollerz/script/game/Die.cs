using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace bb
{
  [RequireComponent(typeof(Rigidbody))]
  public class Die : MonoBehaviour
  {
       Rigidbody rb;
         Vector3 xyz_ini;
         Vector3 rot_ini;
            bool is_cllsn_1;
            bool is_cllsn_2;
     Transform[] t_sides;
       Coroutine cr_launch;

    void Awake()
    {
         is_cllsn_1 = false;
         is_cllsn_2 = false;
          cr_launch = null;
            xyz_ini = transform.position;
            rot_ini = transform.rotation.eulerAngles;
                 rb = GetComponent<Rigidbody>();
                 rb.isKinematic = true;
      t_sides = new Transform[6];
      for(var i=0; i<t_sides.Length; i++) t_sides[i] = transform.GetChild(i);
    }

    public void On_Exit()
    {
      if(cr_launch != null) StopCoroutine(cr_launch);
      cr_launch = null;
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      StartCoroutine(_On_Exit());
    }

    IEnumerator _On_Exit() 
    {
      transform.DOMove  (xyz_ini, 0.75f);
      transform.DORotate(rot_ini, 0.50f);
      yield return new WaitForSeconds(0.50f);
      rb.freezeRotation = true;
      yield return new WaitForSeconds(0.50f);
      rb.freezeRotation = false;
    }

    public void Pickup()
    {
      var xyz_cam = Camera.main.transform.position;
      var xyz_die = xyz_cam;
      xyz_die.x  = Random.Range(xyz_ini.x,xyz_ini.x * 1.3f);
      xyz_die.y -= Random.Range(0.75f, 1.0f);
      xyz_die.z -= Random.Range(0.75f, 1.0f);
      rb.isKinematic = true;
      transform.DOMove(xyz_die, 0.3f);
    }

    void OnCollisionEnter(Collision cllsn)
    {
      glbl._.SFX.Play_Dice_Hit();

      switch(cllsn.gameObject.tag)
      {
        case "Table": is_cllsn_1 = true; break;
        case "Wall" : is_cllsn_2 = true; break;
        case "Die"  : is_cllsn_2 = true; break;
      }
    }

    public void Launch(System.Action<int> cb_fin, bool rigged)
    {
      cr_launch = StartCoroutine(_Launch(cb_fin, rigged));
    }
    IEnumerator _Launch(System.Action<int> cb_fin,bool rigged)
    {
      is_cllsn_1 = false;
      is_cllsn_2 = false;
        rb.velocity = Vector3.zero;
      yield return null;
      var force_1   = Vector3.zero;
          force_1.y = Random.Range(0.3f, 1.0f);
          force_1.z = Random.Range(7.0f, 7.5f);
          force_1.x = Random.Range(0.2f, 0.5f);
          force_1.x = Random.value > 0.5f ? -force_1.x : force_1.x;
      var torqe   = Vector3.zero;
          torqe.x = Random.Range(90, 360);
          torqe.y = Random.Range(90, 360);
          torqe.z = Random.Range(90, 360);
      rb.isKinematic = false;
      rb.AddForce (force_1, ForceMode.Impulse);
      rb.AddTorque(torqe, ForceMode.Impulse);

      while(!is_cllsn_1) yield return null; // table
      var force_2   = Vector3.zero;
          force_2.y = Random.Range(4.5f, 5.0f);
          force_2.z = Random.Range(7.5f, 8.5f);
      rb.AddForce(force_2, ForceMode.Impulse);

      while(!is_cllsn_2) yield return null; // wall | other die
      var force_3   = Vector3.zero;
          force_3.y = -Random.Range(1.0f, 1.2f);
          force_3.z = -Random.Range(4.2f, 4.5f);
      rb.AddForce(force_3, ForceMode.Impulse);

      if(rigged)
      { 
        rb.angularVelocity = Vector3.zero;
        var rot_side_1_up = new Vector3(-180f, 0, 0);
        var dur = Random.Range(0.5f, 0.7f);
        transform.DORotate(rot_side_1_up, dur);
        yield return new WaitForSeconds(dur);
        rb.freezeRotation = true;
      }
      while(rb.velocity != Vector3.zero) yield return null;
      rb.freezeRotation = false;

      var idx_top = 0;
      for(var i=1; i<t_sides.Length; i++)
        if(t_sides[i].position.y > t_sides[idx_top].position.y)
          idx_top = i;
      var side = idx_top + 1;
      cb_fin(side);
      cr_launch = null;
    }
  }
}