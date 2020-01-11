using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace bb.screen
{
  public sealed class Screen_Over : Screen
  {
    CanvasGroup cg_txt_score;
    CanvasGroup cg_txt_highscore;
    CanvasGroup cg_btn_home;
         Button btn_home;
       TMP_Text txt_score;
            int score;
           bool is_high;

    protected override void On_Awake()
    {
      cg_txt_score     = transform.Find("txt_score")    .GetComponent<CanvasGroup>();
      cg_txt_highscore = transform.Find("txt_highscore").GetComponent<CanvasGroup>();
      cg_btn_home      = transform.Find("btn_home")     .GetComponent<CanvasGroup>();
      btn_home  = cg_btn_home.GetComponent<Button>();
      txt_score = cg_txt_score.GetComponent<TMP_Text>();
      Fade(cg_txt_score    , 0, 0);
      Fade(cg_txt_highscore, 0, 0);
      Fade(cg_btn_home     , 0, 0);
      btn_home.onClick.AddListener(() => { 
        glbl._.SFX.Play_Button();
        btn_home.interactable = false;
        Fade(cg_btn_home, 0);
        glbl._.GameState.To_Home();
      });
    }

    public void Setup(int score_, bool is_high_)
    {
      score   = score_;
      is_high = is_high_;
    }

    public override IEnumerator _Transition_In()
    {
      txt_score.text = score.ToString();
      Set_Screen_Visibility(true);
      Fade(cg_txt_score, 1, 0.5f);
      yield return new WaitForSeconds(1.5f);
      if(is_high)
      {
        Fade(cg_txt_highscore, 1, 0.5f);
        yield return new WaitForSeconds(1.5f);
      }
      Fade(cg_btn_home, 1, 0.5f);
      yield return new WaitForSeconds(0.5f);
      btn_home.interactable = true;
      yield return null;
    }
    public override IEnumerator _Transition_Out()
    {
      Fade(cg_btn_home , 0, 0.5f);
      yield return new WaitForSeconds(1.0f);
      Fade(cg_txt_score, 0, 0.5f);
      if(is_high)
        Fade(cg_txt_highscore, 0, 0.5f);
      yield return new WaitForSeconds(1.0f);
      Set_Screen_Visibility(false);
    }
  }
}