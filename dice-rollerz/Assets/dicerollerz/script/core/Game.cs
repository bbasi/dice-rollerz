using System.Collections;
using UnityEngine;

namespace bb.core
{
  public class Game : MonoBehaviour
  {
    Coroutine cr_game;
          int score;
          Die die_1;
          Die die_2;
          int idx_debug;
         bool is_debug;
         bool did_input;

    void Awake()
    {
       cr_game  = null;
          score = 0;
      idx_debug = 0;
       is_debug = false;
      did_input = false;
    }

    public void Initialize(Die d1, Die d2)
    {
      die_1 = d1;
      die_2 = d2;
    }

    public void  Play() => StartCoroutine(_Play());
    IEnumerator _Play()
    {
      is_debug = false;
      yield return new WaitForSeconds(0.5f);
      yield return StartCoroutine(glbl._.UI.Screen_Game._Transition_In());
      cr_game = StartCoroutine(_Game());
    }

    public void On_Button_Roll() => did_input = true;
    public void On_Button_Exit() => Exit();

    public void On_Toggle_Debug(bool on)
    {
       is_debug = on;
      idx_debug = 0;
    }

    public void Exit()
    {
      StopCoroutine(cr_game);
      cr_game = null;
      die_1.On_Exit();
      die_2.On_Exit();
       is_debug = false;
      idx_debug = 0;
          score = 0;
      glbl._.GameState.To_Home();
    }

    IEnumerator _Game()
    {
      glbl._.SFX.Play_Dice_Pickup();
      die_1.Pickup();
      die_2.Pickup();
      yield return new WaitForSeconds(0.8f);

      glbl._.UI.Screen_Game.Show_Button_Roll();
      while(!did_input) yield return null;
      did_input = false;

      bool is_rigged = false; // force dice result : 1 / 1
      if(is_debug && ++idx_debug == 5)
      {
        is_rigged = true;
        idx_debug = 0;
      }

      glbl._.SFX.Play_Dice_Roll();
      yield return new WaitForSeconds(1.5f);

      int result_die_1 = -1;
      int result_die_2 = -1;
      die_1.Launch((side) => { result_die_1 = side; }, is_rigged);
      yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
      die_2.Launch((side) => { result_die_2 = side; }, is_rigged);

      while(result_die_1 == -1 || result_die_2 == -1 ) yield return null;
       int total   = result_die_1 + result_die_2;
      bool success = result_die_1 != result_die_2;
      yield return new WaitForSeconds(1.0f);

      if(success)
      {
        glbl._.SFX.Play_Success();
        score += total;
        yield return glbl._.UI.Screen_Game._Present_Result_Roll(total);
        cr_game = StartCoroutine(_Game());
      }
      else
      {
        glbl._.SFX.Play_Failure();
        glbl._.UI.Screen_Game.On_Game_Over();
        bool is_high_score = score > glbl._.IO.Get_Score_High();
          if(is_high_score) glbl._.IO.Set_Score_High(score);
        glbl._.GameState.To_Over(score, is_high_score);
        score = 0;
      }
    }
  }
}