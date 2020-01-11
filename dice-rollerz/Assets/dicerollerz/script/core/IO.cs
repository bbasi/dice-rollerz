using UnityEngine;

namespace bb.core
{
  public class IO : MonoBehaviour
  {
    const string KEY_SAVE = "score_high";
    public int  Get_Score_High(){ return PlayerPrefs.GetInt(KEY_SAVE, 0); }
    public void Set_Score_High(int score) => PlayerPrefs.SetInt(KEY_SAVE, score);
  }
}