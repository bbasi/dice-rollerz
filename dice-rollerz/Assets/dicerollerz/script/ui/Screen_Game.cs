using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace bb.screen
{
  public sealed class Screen_Game : Screen
  {
    CanvasGroup cg_txt_score;
    CanvasGroup cg_txt_result;
    CanvasGroup cg_btn_roll;
    CanvasGroup cg_btn_exit;
    CanvasGroup cg_tgl_debug;
         Button btn_roll;
         Button btn_exit;
        Toggle  tgl_debug;
       TMP_Text txt_score;
       TMP_Text txt_result;

    protected override void On_Awake()
    {
      cg_txt_score  = transform.Find("txt_score") .GetComponent<CanvasGroup>();
      cg_txt_result = transform.Find("txt_result").GetComponent<CanvasGroup>();
      cg_btn_roll   = transform.Find("btn_roll")  .GetComponent<CanvasGroup>();
      cg_btn_exit   = transform.Find("btn_exit")  .GetComponent<CanvasGroup>();
      cg_tgl_debug  = transform.Find("tgl_debug") .GetComponent<CanvasGroup>();
      btn_roll   = cg_btn_roll.GetComponent<Button>();
      btn_exit   = cg_btn_exit.GetComponent<Button>();
      tgl_debug  = cg_tgl_debug.GetComponent<Toggle>();
      txt_score  = cg_txt_score.GetComponent<TMP_Text>();
      txt_result = cg_txt_result.GetComponent<TMP_Text>();
      Fade(cg_txt_score , 0, 0);
      Fade(cg_txt_result, 0, 0);
      Fade(cg_btn_roll  , 0, 0);
      Fade(cg_btn_exit  , 0, 0);
      Fade(cg_tgl_debug , 0, 0);

      btn_roll.onClick.AddListener(() => { 
        btn_roll.interactable = false;
        Fade(cg_btn_roll, 0);
        glbl._.Game.On_Button_Roll();
      });
      btn_exit.onClick.AddListener(() => { 
        glbl._.SFX.Play_Button();
        btn_exit .interactable = false;
        btn_roll .interactable = false;
        tgl_debug.interactable = false;
        Fade(cg_btn_exit, 0f);
        glbl._.Game.On_Button_Exit();
      });
      tgl_debug.onValueChanged.AddListener((toggle) => { 
        if(toggle) glbl._.SFX.Play_Button();
        glbl._.Game.On_Toggle_Debug(toggle);
      });
    }

    public void On_Game_Over()
    {
      btn_exit .interactable = false;
      tgl_debug.interactable = false;
      Fade(cg_btn_exit , 0);
      Fade(cg_tgl_debug, 0);
    }

    public void Show_Button_Roll()
    {
      Fade(cg_btn_roll, 1.0f);
      btn_roll.interactable = true;
    }

    public IEnumerator _Present_Result_Roll(int result)
    {
      txt_result.text = result.ToString();
      Fade(cg_txt_result, 1, 0.5f);
      yield return new WaitForSeconds(1.5f);
      Fade(cg_txt_result, 0, 0.5f);
      var score_cur = int.Parse(txt_score.text);
      var score_new = score_cur + result;
      for(var i=score_cur; i<=score_new; i++)
      {
        txt_score.text = i.ToString();
        yield return new WaitForSeconds(0.075f);
      }
      yield return new WaitForSeconds(1.0f);
    }

    public override IEnumerator _Transition_In()
    {
      tgl_debug.isOn = false;
      txt_score.text = "0";
      Set_Screen_Visibility(true);
      Fade(cg_txt_score, 1, 0.5f);
      yield return new WaitForSeconds(0.8f);
      Fade(cg_btn_exit , 1);
      Fade(cg_tgl_debug, 1);
      yield return new WaitForSeconds(0.3f);
      btn_exit .interactable = true;
      tgl_debug.interactable = true;
      yield return null;
    }
    public override IEnumerator _Transition_Out()
    {
      Fade(cg_btn_exit  , 0);
      Fade(cg_tgl_debug , 0);
      Fade(cg_btn_roll  , 0, 0.5f);
      Fade(cg_txt_result, 0, 0.5f);
      Fade(cg_txt_score , 0, 0.5f);
      yield return new WaitForSeconds(1.0f);
      Set_Screen_Visibility(false);
      yield return null;
    }
  }
}