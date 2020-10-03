using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
   [Header("UI Elements")] 
   public GameObject PanelInfotext;
   public GameObject PanelChoix;
   public GameObject PanelObject;
   public Image BackGround;
   public GameObject BoutonChoix;
   [HideInInspector] public TMP_Text InfoText;

   private bool _isHiden;

   private void Awake()
   {
      InfoText = PanelInfotext.GetComponentInChildren<TMP_Text>();
   }

   public void RefreshThumbnail(Thumbnail scene)
   {
      if (scene.GivenItem > 0)
      {
         GameManager.Inventory.Add(GameManager.Story.Items.FirstOrDefault(item => item.Id == scene.GivenItem));
      }
      InfoText.text = scene.Description;
      BackGround.sprite = Resources.Load<Sprite>(Application.streamingAssetsPath +"/"+ scene.Background);
      ChoixShowHide();
      GameManager.LoadSprite(scene.Background);
      foreach (Transform transform in PanelChoix.transform)
      {
         Destroy(transform.gameObject);
      }
      foreach (Choice choix in scene.Choices)
      {
         Item firstOrDefault = GameManager.Inventory.FirstOrDefault(item => item.Id == choix.NeededItem);
         if (choix.NeededItem > 0 && firstOrDefault == null) continue;
         GameObject bp = Instantiate(BoutonChoix, PanelChoix.transform);
         bp.GetComponentInChildren<TMP_Text>().text = choix.Description;
         bp.GetComponent<Button>().onClick.AddListener(delegate
         {
            RefreshThumbnail(GameManager.Story.Thumbnails
               .FirstOrDefault(thumb=>thumb.Id==choix.LinkedThumbnail));
         });
      }

   }
   
   public void ChoixShowHide()
   {
      if (_isHiden)
      {
         PanelChoix.SetActive(true);
         _isHiden = false;
      }
      else
      {
         PanelChoix.SetActive(false);
         _isHiden = true;
      }
   }
}

