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
   public GameObject ItemImg;
   [HideInInspector] public TMP_Text InfoText;

   private bool _isHiden;

   private void Awake()
   {
      InfoText = PanelInfotext.GetComponentInChildren<TMP_Text>();
   }

   public void RefreshThumbnail(Thumbnail scene, Story story)
   {
      ChoixShowHide();
      if (scene.GivenItem > 0 && !GameManager.Inventory.Contains(story.Items.FirstOrDefault(item => item.Id == scene.GivenItem)))
      {
         Item iteme = story.Items.FirstOrDefault(item => item.Id == scene.GivenItem);
         GameManager.Inventory.Add(iteme);
         GameObject S = Instantiate(ItemImg, PanelObject.transform);
         S.GetComponent<Image>().sprite = GameManager.LoadSprite(iteme.Image);
      }
      InfoText.text = scene.Description;
      BackGround.sprite = GameManager.LoadSprite(scene.Background);
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
            RefreshThumbnail(story.Thumbnails
               .FirstOrDefault(thumb=>thumb.Id == choix.LinkedThumbnail), story);
         }
            );
      }

   }
   
   public void ChoixShowHide()
   {
      if (_isHiden)
      {
         LeanTween.moveY(PanelChoix, -31, 0.25f);
        //PanelChoix.SetActive(true);
         _isHiden = false;
      }
      else
      {
         LeanTween.moveLocalY(PanelChoix, -73, 0.25f);
         //PanelChoix.SetActive(false);
         _isHiden = true;
      }
   }
}

