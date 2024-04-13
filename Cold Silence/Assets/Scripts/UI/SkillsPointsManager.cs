using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillsPointsManager : MonoBehaviour
{
    public TextMeshProUGUI skillPointsText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI hpText;
    public GameObject damageUpgradeButton;
    public GameObject hpUpgradeButton;
    public void OpenWindow()
    {
        this.gameObject.SetActive(true);
        Player.Instance.is_in_menu = true;
        if (PlayerLevelManager.skill_points > 0)
        {
            damageUpgradeButton.SetActive(true);
            hpUpgradeButton.SetActive(true);
        }
        else
        {
            UIManager.Instance.DeactivateSkillMenuButton();
            damageUpgradeButton.SetActive(false);
            hpUpgradeButton.SetActive(false);
        }
        skillPointsText.text = "Очки улучшений: " + PlayerLevelManager.skill_points;
        hpText.text = "ХП: " + Player.Instance.max_health;
        damageText.text = "Урон: " + Player.Instance.damage;
    }
    public void HPUpgradeButtonPress()
    {
        PlayerLevelManager.skill_points--;
        if (PlayerLevelManager.skill_points <= 0)
        {
            UIManager.Instance.DeactivateSkillMenuButton();
            damageUpgradeButton.SetActive(false);
            hpUpgradeButton.SetActive(false);
        }
        skillPointsText.text = "Очки улучшений: " + PlayerLevelManager.skill_points;
        Player.Instance.max_health += 5;
        Player.Instance.health = Player.Instance.max_health;
        hpText.text = "ХП: " + Player.Instance.max_health;
    }
    public void DamageUpgradeButtonPress()
    {
        PlayerLevelManager.skill_points--;
        if (PlayerLevelManager.skill_points <= 0)
        {
            UIManager.Instance.DeactivateSkillMenuButton();
            damageUpgradeButton.SetActive(false);
            hpUpgradeButton.SetActive(false);
        }
        skillPointsText.text = "Очки улучшений: " + PlayerLevelManager.skill_points;
        Player.Instance.damage++;
        damageText.text = "Урон: " + Player.Instance.damage;
    }
    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
        Player.Instance.is_in_menu = false;
    }
}
