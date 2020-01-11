using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace bb.screen
{
  public sealed class Screen_Home : Screen
  {
    CanvasGroup cg_txt_score;
    CanvasGroup cg_txt_title;
    CanvasGroup cg_btn_play;
         Button btn_play;
       TMP_Text txt_score;

    protected override void On_Awake()
    {
      cg_txt_score = transform.Find("txt_score").GetComponent<CanvasGroup>();
      cg_txt_title = transform.Find("txt_title").GetComponent<CanvasGroup>();
      cg_btn_play  = transform.Find("btn_play") .GetComponent<CanvasGroup>();
      btn_play  = cg_btn_play.GetComponent<Button>();
      txt_score = cg_txt_score.GetComponent<TMP_Text>();
      Fade(cg_txt_score, 0, 0);
      Fade(cg_txt_title, 0, 0);
      Fade(cg_btn_play , 0, 0);

      btn_play.onClick.AddListener(()=>{
        glbl._.SFX.Play_Button();
        btn_play.interactable = false;
        glbl._.GameState.To_Game();
      });
    }
      
    public override IEnumerator _Transition_In()
    {
      txt_score.text = $"best - {glbl._.IO.Get_Score_High()}";
      Set_Screen_Visibility(true);
      Fade(cg_txt_title, 1, 0.5f);
      yield return new WaitForSeconds(1.3f);
      Fade(cg_txt_score, 1);
      yield return new WaitForSeconds(0.3f);
      Fade(cg_btn_play , 1);
      yield return new WaitForSeconds(0.3f);
      btn_play.interactable = true;
      yield return null;
    }
    public override IEnumerator _Transition_Out()
    {
      Fade(cg_btn_play , 0);
      Fade(cg_txt_score, 0);
      yield return new WaitForSeconds(1.0f);
      Fade(cg_txt_title, 0, 0.5f);
      yield return new WaitForSeconds(0.6f);
      Set_Screen_Visibility(false);
    }
  }
}