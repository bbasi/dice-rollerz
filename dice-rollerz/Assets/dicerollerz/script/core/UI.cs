using UnityEngine;

namespace bb.core
{
  public class UI : MonoBehaviour
  {
    public screen.Screen_Home Screen_Home { get; private set;}
    public screen.Screen_Game Screen_Game { get; private set;}
    public screen.Screen_Over Screen_Over { get; private set;}

    private void Awake()
    {
      Screen_Home = GameObject.Find("screen_home").GetComponent<screen.Screen_Home>();
      Screen_Game = GameObject.Find("screen_game").GetComponent<screen.Screen_Game>();
      Screen_Over = GameObject.Find("screen_over").GetComponent<screen.Screen_Over>();
    }
  }
}