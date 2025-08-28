using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBehavior<GameManager>
{
    [field: Header("GameSettings")]
    [field: SerializeField] public bool GamePaused { get; set; }
    [field: SerializeField] public int GameStartTime { get; set; }
    [field: SerializeField] public int GamePlayingTime { get; set; }
    public bool GameStarted { get; set; }
    public bool PlayerWin { get; set; }
    [field: Space]
    [field: Header("GameUI")]
    [field: SerializeField] public GameObject GameStartUI { get; set; }
    [field: SerializeField] public TMP_Text GameStartTextUI { get; set;}
    [field: SerializeField] public GameObject GameEndUI { get; set;}
    [field: SerializeField] public TMP_Text GameEndTextUI { get; set;}
    [field: Space]
    [field: Header("Player&Bot")]
    [field: SerializeField] public Player Player { get; set; }
    [field: SerializeField] public Bot Bot { get; set; }
    [Space]
    [Header("HpUI")]
    [SerializeField] private Slider playerHp;
    [SerializeField] private Slider botHp;

    public void UpdatePlayerHp()
    {
        playerHp.value = Player.Stat.CurrentHp / Player.Stat.MaxHp;
    }

    public void UpdateBotHp()
    {
        botHp.value = Bot.Stat.CurrentHp / Bot.Stat.MaxHp;
    }
}