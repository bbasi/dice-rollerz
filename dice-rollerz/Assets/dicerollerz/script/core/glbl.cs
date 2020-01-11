using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bb
{
  public class glbl : MonoBehaviour
  {
    public static glbl _;
    public core.GameState GameState { get; private set;}
    public core.UI        UI        { get; private set;}
    public core.Game      Game      { get; private set;}
    public core.IO        IO        { get; private set;}
    public core.Camera_   Camera_   { get; private set;}
    public core.SFX       SFX       { get; private set;}

    void Awake()
    {
      _ = this;
       GameState = GetComponent<core.GameState>();
            Game = GetComponent<core.Game>();
         Camera_ = GetComponent<core.Camera_>();
              UI = GetComponent<core.UI>();
              IO = GetComponent<core.IO>();
             SFX = GetComponentInChildren<core.SFX>(); 
      StartCoroutine(_boot());
    }

    IEnumerator _boot()
    {
      var load = new AsyncOperation();
          load = SceneManager.LoadSceneAsync("game", LoadSceneMode.Additive);
      while(!load.isDone) yield return null;
      yield return null;
      var die_1 = GameObject.Find("die_1").GetComponent<Die>();
      var die_2 = GameObject.Find("die_2").GetComponent<Die>();
      Game.Initialize(die_1,die_2);
      Camera_.Initialize(Camera.main);
      yield return new WaitForSeconds(1.0f);
      GameState.To_Home();
    }
  }
}