using UnityEngine;

namespace bb.core
{
  public class SFX : MonoBehaviour
  {
    #pragma warning disable CS0649
    [SerializeField] AudioClip   sfx_button;
    #pragma warning disable CS0649
    [SerializeField] AudioClip   sfx_success;
    #pragma warning disable CS0649
    [SerializeField] AudioClip   sfx_failure;
    #pragma warning disable CS0649
    [SerializeField] AudioClip[] sfxs_dice_pickup;
    #pragma warning disable CS0649
    [SerializeField] AudioClip[] sfxs_dice_roll;
    #pragma warning disable CS0649
    [SerializeField] AudioClip[] sfxs_dice_hit;
    AudioSource[] asrcs;
              int idx_asrc;

    void Awake()
    {
      asrcs = new AudioSource[5];
      for(var i=0; i<asrcs.Length; i++)
        asrcs[i] = transform.Find($"sfx_gen_0{i}").GetComponent<AudioSource>();
      idx_asrc = 0;
    }

    public void Play_Button () => Play_SFX(sfx_button , 0.7f);
    public void Play_Success() => Play_SFX(sfx_success, 0.7f);
    public void Play_Failure() => Play_SFX(sfx_failure, 0.7f);
    public void Play_Dice_Pickup() => Play_SFX(Get_Random_Clip(sfxs_dice_pickup), 0.8f);
    public void Play_Dice_Roll()   => Play_SFX(Get_Random_Clip(sfxs_dice_roll)  , 0.7f);
    public void Play_Dice_Hit()    => Play_SFX(Get_Random_Clip(sfxs_dice_hit)   , 0.8f);
    AudioClip Get_Random_Clip(AudioClip[] clips) { return clips[Random.Range(0, clips.Length)]; }

    void Play_SFX(AudioClip ac, float vlm)
    {
      if(idx_asrc == asrcs.Length) idx_asrc = 0;
      var asrc = asrcs[idx_asrc];
          asrc.clip = ac;
          asrc.volume = vlm;
          asrc.Play();
      idx_asrc++;
    }
  }
}