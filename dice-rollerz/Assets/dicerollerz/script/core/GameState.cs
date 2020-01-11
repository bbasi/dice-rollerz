using System.Collections;
using UnityEngine;

namespace bb.core
{
  public class GameState : MonoBehaviour
  {
    public enum State
    {
      None,
      Home,
      Game,
      Over
    }
    State state;

    void Awake() => state = State.None;
    public void To_Home() => StartCoroutine(_To(State.Home));
    public void To_Game() => StartCoroutine(_To(State.Game));
    public void To_Over(int result, bool is_high)
    {
      glbl._.UI.Screen_Over.Setup(result, is_high);
      StartCoroutine(_To(State.Over));
    }

    IEnumerator _To(State stt_new)
    {
      if(stt_new == State.Game)
        glbl._.Camera_.Transition_To_Game();
      else
        glbl._.Camera_.Transition_To_Home();

      switch(state)
      {
        case State.Home: yield return StartCoroutine(glbl._.UI.Screen_Home._Transition_Out()); break;
        case State.Game: yield return StartCoroutine(glbl._.UI.Screen_Game._Transition_Out()); break;
        case State.Over: yield return StartCoroutine(glbl._.UI.Screen_Over._Transition_Out()); break;
      }
      state = stt_new;
      switch(state)
      {
        case State.Home: yield return StartCoroutine(glbl._.UI.Screen_Home._Transition_In()); break;
        case State.Game: glbl._.Game.Play(); break;
        case State.Over: yield return StartCoroutine(glbl._.UI.Screen_Over._Transition_In()); break;
      }
    }
  }
}