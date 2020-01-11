using UnityEngine;

namespace bb
{
  public class Fan : MonoBehaviour
  {
    void Update() => transform.Rotate(new Vector3(0,360 * Time.deltaTime,0));
  }
}