using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace bb.screen
{
  [RequireComponent(typeof(CanvasGroup))]
  public abstract class Screen : MonoBehaviour
  {
    CanvasGroup cg;

    void Awake()
    {
      cg = GetComponent<CanvasGroup>();
      Set_Screen_Visibility(false);
      On_Awake();
    }
    protected virtual void On_Awake() {}

    protected void Set_Screen_Visibility(bool visible)
    {
      cg.alpha = visible ? 1 : 0;
      cg.interactable = visible;
      cg.blocksRaycasts = visible;
    }

    protected void Fade(CanvasGroup cg_, float alpha, float dur=0.30f) => cg_.DOFade(alpha, dur);

    public abstract IEnumerator _Transition_In ();
    public abstract IEnumerator _Transition_Out();
  }
}