using UnityEngine;
using DG.Tweening;

namespace bb.core
{
  public class Camera_ : MonoBehaviour
  {
       Camera cam3D;
      Vector3 xyz_dflt;
      Vector3 xyz_game;
      Vector3 rot_dflt;
      Vector3 rot_game;

    public void Initialize(Camera cam)
    {
         cam3D = cam;
      xyz_dflt = cam3D.transform.position;
      xyz_game = new Vector3(0, -2.0f, -3.5f);
      rot_dflt = new Vector3(22, 0, 0);
      rot_game = new Vector3(30, 0, 0);
    }

    public void Transition_To_Game()
    {
      cam3D.transform.DOMove  (xyz_game, 1);
      cam3D.transform.DORotate(rot_game, 1);
    }

    public void Transition_To_Home()
    {
      cam3D.transform.DOMove  (xyz_dflt, 1);
      cam3D.transform.DORotate(rot_dflt, 1);
    }
  }
}