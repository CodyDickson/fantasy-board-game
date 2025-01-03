using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class Combat : MonoBehaviour
{
    public Button continueButton;
    public Button fightButton;
    public Button suddenDeathButton;
    public GameObject combatUnitOneWins;
    public GameObject combatUnitTwoWins;
    [SerializeField] public TMP_Text combatUnitOne_Text;
    [SerializeField] public TMP_Text combatUnitTwo_Text;
    public GameObject diceOne;
    public GameObject diceTwo;
    public GameObject diceThree;
    public GameObject diceFour;
    public GameObject diceFive;
    public GameObject diceSix;
    public GameObject avatar_one_diceOne_one;
    public GameObject avatar_one_diceTwo_one;
    public GameObject avatar_one_diceThree_one;
    public GameObject avatar_one_diceFour_one;
    public GameObject avatar_one_diceFive_one;
    public GameObject avatar_one_diceSix_one;
    public GameObject avatar_one_diceOne_two;
    public GameObject avatar_one_diceTwo_two;
    public GameObject avatar_one_diceThree_two;
    public GameObject avatar_one_diceFour_two;
    public GameObject avatar_one_diceFive_two;
    public GameObject avatar_one_diceSix_two;
    public GameObject avatar_one_diceOne_three;
    public GameObject avatar_one_diceTwo_three;
    public GameObject avatar_one_diceThree_three;
    public GameObject avatar_one_diceFour_three;
    public GameObject avatar_one_diceFive_three;
    public GameObject avatar_one_diceSix_three;
    public GameObject avatar_two_diceOne_one;
    public GameObject avatar_two_diceTwo_one;
    public GameObject avatar_two_diceThree_one;
    public GameObject avatar_two_diceFour_one;
    public GameObject avatar_two_diceFive_one;
    public GameObject avatar_two_diceSix_one;
    public GameObject avatar_two_diceOne_two;
    public GameObject avatar_two_diceTwo_two;
    public GameObject avatar_two_diceThree_two;
    public GameObject avatar_two_diceFour_two;
    public GameObject avatar_two_diceFive_two;
    public GameObject avatar_two_diceSix_two;
    public GameObject avatar_two_diceOne_three;
    public GameObject avatar_two_diceTwo_three; 
    public GameObject avatar_two_diceThree_three;
    public GameObject avatar_two_diceFour_three;
    public GameObject avatar_two_diceFive_three;
    public GameObject avatar_two_diceSix_three;
    public Image avatar_one;
    public Image avatar_two;
    public Sprite player_red_sprite;
    public Sprite player_blue_sprite;
    public Sprite player_green_sprite;
    public Sprite player_purple_sprite;
    public Sprite player_white_sprite;
    public Sprite monster_imp_sprite;
    public Sprite monster_basilisk_sprite;
    public Sprite monster_rampagingElephant_sprite;
    [SerializeField] public Tile player_red;
    [SerializeField] public Tile player_blue;
    [SerializeField] public Tile player_green;
    [SerializeField] public Tile player_purple;
    [SerializeField] public Tile player_white;
    [SerializeField] public Tile monster_imp;
    [SerializeField] public Tile monster_basilisk;
    [SerializeField] public Tile monster_rampagingElephant;
    [SerializeField] public Tilemap tilemapStructures;
    public static bool preFightScreenActive = true;
    public static bool fightScreenActive = false;
    public static float tempCounter = 0f;
    public static float counter = 0.5f;
    public static bool avatar_one_set = false;
    public static bool avatar_two_set = false;
    public static int combatUnitOne;
    public static int combatUnitTwo;
    public static int combatUnitOne_AmountOfCombatDice = 1;
    public static int combatUnitTwo_AmountOfCombatDice = 1;
    public static int combatUnitOne_DiceTotal;
    public static int combatUnitTwo_DiceTotal;
    public static int combatUnitOne_resultOne;
    public static int combatUnitOne_resultTwo;
    public static int combatUnitOne_resultThree;
    public static int combatUnitTwo_resultOne;
    public static int combatUnitTwo_resultTwo;
    public static int combatUnitTwo_resultThree;
    public static bool diceAreShowing = false;
    public static bool diceResultAreShowing = false;
    public static bool namesAreUpdated = false;


    void Start()
    {
        continueButton.onClick.AddListener(OnClickContinueButton);
        fightButton.onClick.AddListener(OnClickFightButton);
        fightButton.gameObject.SetActive(true);
        Debug.Log("Fight Button Active");
        continueButton.gameObject.SetActive(false);
        suddenDeathButton.gameObject.SetActive(false);
        avatar_one = avatar_one.GetComponent<Image>();
        avatar_two = avatar_two.GetComponent<Image>();
        preFightScreenActive = true;
        combatUnitOneWins.gameObject.SetActive(false);
        combatUnitTwoWins.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C) && continueButton.gameObject.activeSelf)
        {
            OnClickContinueButton();
        }
        if (Input.GetKey(KeyCode.F) && fightButton.gameObject.activeSelf)
        {
            preFightScreenActive = false;
            fightScreenActive = true;
            diceAreShowing = false;
            CancelInvoke();
            fightButton.gameObject.SetActive(false);
            CombatEncounter(tilemapStructures, player_red, player_blue, player_green, player_purple, player_white, monster_imp, monster_basilisk);
            continueButton.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.F) && suddenDeathButton.gameObject.activeSelf)
        {
            OnClickSuddenDeathButton();
        }
        if (GameMain.combatScreenEnabled && !namesAreUpdated)
        {
            switch (combatUnitOne)
            {
                case 1: combatUnitOne_Text.text = GameMain.playerOneColor + " player"; break;
                case 2: combatUnitOne_Text.text = GameMain.playerTwoColor + " player"; break;
                case 3: combatUnitOne_Text.text = GameMain.playerThreeColor + " player"; break;
                case 4: combatUnitOne_Text.text = GameMain.playerFourColor + " player"; break;
                case 5: combatUnitOne_Text.text = "imp"; break;
                case 6: combatUnitOne_Text.text = "basilisk"; break;
                case 7: combatUnitOne_Text.text = "rampaging elephant"; break;
            }
            switch (combatUnitTwo)
            {
                case 1: combatUnitTwo_Text.text = GameMain.playerOneColor + " player"; break;
                case 2: combatUnitTwo_Text.text = GameMain.playerTwoColor + " player"; break;
                case 3: combatUnitTwo_Text.text = GameMain.playerThreeColor + " player"; break;
                case 4: combatUnitTwo_Text.text = GameMain.playerFourColor + " player"; break;
                case 5: combatUnitTwo_Text.text = "imp"; break;
                case 6: combatUnitTwo_Text.text = "basilisk"; break;
                case 7: combatUnitTwo_Text.text = "rampaging elephant"; break;
            }
            namesAreUpdated = true;
        }
        if (GameMain.combatScreenEnabled && !avatar_one_set)
        {
            Debug.Log("Combat Screen Enabled, Combat Unit One = " + combatUnitOne);
            if (combatUnitOne == 1)
            {
                switch (GameMain.playerOneColor)
                {
                    case "red": avatar_one.sprite = player_red_sprite; break;
                    case "blue": avatar_one.sprite = player_blue_sprite; break;
                    case "green": avatar_one.sprite = player_green_sprite; break;
                    case "purple": avatar_one.sprite = player_purple_sprite; break;
                    case "white": avatar_one.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitOne == 2)
            {
                switch (GameMain.playerTwoColor)
                {
                    case "red": avatar_one.sprite = player_red_sprite; break;
                    case "blue": avatar_one.sprite = player_blue_sprite; break;
                    case "green": avatar_one.sprite = player_green_sprite; break;
                    case "purple": avatar_one.sprite = player_purple_sprite; break;
                    case "white": avatar_one.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitOne == 3)
            {
                switch (GameMain.playerThreeColor)
                {
                    case "red": avatar_one.sprite = player_red_sprite; break;
                    case "blue": avatar_one.sprite = player_blue_sprite; break;
                    case "green": avatar_one.sprite = player_green_sprite; break;
                    case "purple": avatar_one.sprite = player_purple_sprite; break;
                    case "white": avatar_one.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitOne == 4)
            {
                switch (GameMain.playerFourColor)
                {
                    case "red": avatar_one.sprite = player_red_sprite; break;
                    case "blue": avatar_one.sprite = player_blue_sprite; break;
                    case "green": avatar_one.sprite = player_green_sprite; break;
                    case "purple": avatar_one.sprite = player_purple_sprite; break;
                    case "white": avatar_one.sprite = player_white_sprite; break;
                }
            }
            avatar_one_set = true;
        }
        if (GameMain.combatScreenEnabled && !avatar_two_set)
        {
            Debug.Log("Combat Screen Enabled, Combat Unit Two = " + combatUnitTwo);
            if (combatUnitTwo == 1)
            {
                switch (GameMain.playerOneColor)
                {
                    case "red": avatar_two.sprite = player_red_sprite; break;
                    case "blue": avatar_two.sprite = player_blue_sprite; break;
                    case "green": avatar_two.sprite = player_green_sprite; break;
                    case "purple": avatar_two.sprite = player_purple_sprite; break;
                    case "white": avatar_two.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitTwo == 2)
            {
                switch (GameMain.playerTwoColor)
                {
                    case "red": avatar_two.sprite = player_red_sprite; break;
                    case "blue": avatar_two.sprite = player_blue_sprite; break;
                    case "green": avatar_two.sprite = player_green_sprite; break;
                    case "purple": avatar_two.sprite = player_purple_sprite; break;
                    case "white": avatar_two.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitTwo == 3)
            {
                switch (GameMain.playerThreeColor)
                {
                    case "red": avatar_two.sprite = player_red_sprite; break;
                    case "blue": avatar_two.sprite = player_blue_sprite; break;
                    case "green": avatar_two.sprite = player_green_sprite; break;
                    case "purple": avatar_two.sprite = player_purple_sprite; break;
                    case "white": avatar_two.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitTwo == 4)
            {
                switch (GameMain.playerFourColor)
                {
                    case "red": avatar_two.sprite = player_red_sprite; break;
                    case "blue": avatar_two.sprite = player_blue_sprite; break;
                    case "green": avatar_two.sprite = player_green_sprite; break;
                    case "purple": avatar_two.sprite = player_purple_sprite; break;
                    case "white": avatar_two.sprite = player_white_sprite; break;
                }
            }
            if (combatUnitTwo == 5)
            {
                avatar_two.sprite = monster_imp_sprite;
            }
            if (combatUnitTwo == 6)
            {
                avatar_two.sprite = monster_basilisk_sprite;
            }
            if (combatUnitTwo == 7)
            {
                avatar_two.sprite = monster_rampagingElephant_sprite;
            }
            avatar_two_set = true;
        }
        if (preFightScreenActive && !diceAreShowing)
        {
            diceAreShowing = true;
            InvokeRepeating("RandomDice", 0f, 0.5f);
        }
        if (fightScreenActive && !diceResultAreShowing)
        {
            if (combatUnitOne_DiceTotal > 0)
            {
                int result = combatUnitOne_resultOne;
                avatar_one_diceOne_one.SetActive(false);
                avatar_one_diceTwo_one.SetActive(false);
                avatar_one_diceThree_one.SetActive(false);
                avatar_one_diceFour_one.SetActive(false);
                avatar_one_diceFive_one.SetActive(false);
                avatar_one_diceSix_one.SetActive(false);
                switch (result)
                {
                    case 1: avatar_one_diceOne_one.SetActive(true); break;
                    case 2: avatar_one_diceTwo_one.SetActive(true); break;
                    case 3: avatar_one_diceThree_one.SetActive(true); break;
                    case 4: avatar_one_diceFour_one.SetActive(true); break;
                    case 5: avatar_one_diceFive_one.SetActive(true); break;
                    case 6: avatar_one_diceSix_one.SetActive(true); break;
                }
            }
            if (combatUnitOne_DiceTotal > 1)
            {
                int result = combatUnitOne_resultTwo;
                avatar_one_diceOne_two.SetActive(false);
                avatar_one_diceTwo_two.SetActive(false);
                avatar_one_diceThree_two.SetActive(false);
                avatar_one_diceFour_two.SetActive(false);
                avatar_one_diceFive_two.SetActive(false);
                avatar_one_diceSix_two.SetActive(false);
                switch (result)
                {
                    case 1: avatar_one_diceOne_two.SetActive(true); break;
                    case 2: avatar_one_diceTwo_two.SetActive(true); break;
                    case 3: avatar_one_diceThree_two.SetActive(true); break;
                    case 4: avatar_one_diceFour_two.SetActive(true); break;
                    case 5: avatar_one_diceFive_two.SetActive(true); break;
                    case 6: avatar_one_diceSix_two.SetActive(true); break;
                }
            }
            if (combatUnitOne_DiceTotal > 2)
            {
                int result = combatUnitOne_resultThree;
                avatar_one_diceOne_three.SetActive(false);
                avatar_one_diceTwo_three.SetActive(false);
                avatar_one_diceThree_three.SetActive(false);
                avatar_one_diceFour_three.SetActive(false);
                avatar_one_diceFive_three.SetActive(false);
                avatar_one_diceSix_three.SetActive(false);
                switch (result)
                {
                    case 1: avatar_one_diceOne_three.SetActive(true); break;
                    case 2: avatar_one_diceTwo_three.SetActive(true); break;
                    case 3: avatar_one_diceThree_three.SetActive(true); break;
                    case 4: avatar_one_diceFour_three.SetActive(true); break;
                    case 5: avatar_one_diceFive_three.SetActive(true); break;
                    case 6: avatar_one_diceSix_three.SetActive(true); break;
                }
            }
            if (combatUnitTwo_DiceTotal > 0)
            {
                int result = combatUnitTwo_resultOne;
                avatar_two_diceOne_one.SetActive(false);
                avatar_two_diceTwo_one.SetActive(false);
                avatar_two_diceThree_one.SetActive(false);
                avatar_two_diceFour_one.SetActive(false);
                avatar_two_diceFive_one.SetActive(false);
                avatar_two_diceSix_one.SetActive(false);
                switch (result)
                {
                    case 1: avatar_two_diceOne_one.SetActive(true); break;
                    case 2: avatar_two_diceTwo_one.SetActive(true); break;
                    case 3: avatar_two_diceThree_one.SetActive(true); break;
                    case 4: avatar_two_diceFour_one.SetActive(true); break;
                    case 5: avatar_two_diceFive_one.SetActive(true); break;
                    case 6: avatar_two_diceSix_one.SetActive(true); break;
                }
            }
            if (combatUnitTwo_DiceTotal > 1)
            {
                int result = combatUnitTwo_resultTwo;
                avatar_two_diceOne_two.SetActive(false);
                avatar_two_diceTwo_two.SetActive(false);
                avatar_two_diceThree_two.SetActive(false);
                avatar_two_diceFour_two.SetActive(false);
                avatar_two_diceFive_two.SetActive(false);
                avatar_two_diceSix_two.SetActive(false);
                switch (result)
                {
                    case 1: avatar_two_diceOne_two.SetActive(true); break;
                    case 2: avatar_two_diceTwo_two.SetActive(true); break;
                    case 3: avatar_two_diceThree_two.SetActive(true); break;
                    case 4: avatar_two_diceFour_two.SetActive(true); break;
                    case 5: avatar_two_diceFive_two.SetActive(true); break;
                    case 6: avatar_two_diceSix_two.SetActive(true); break;
                }
            }
            if (combatUnitTwo_DiceTotal > 2)
            {
                int result = combatUnitTwo_resultThree;
                avatar_two_diceOne_three.SetActive(false);
                avatar_two_diceTwo_three.SetActive(false);
                avatar_two_diceThree_three.SetActive(false);
                avatar_two_diceFour_three.SetActive(false);
                avatar_two_diceFive_three.SetActive(false);
                avatar_two_diceSix_three.SetActive(false);
                switch (result)
                {
                    case 1: avatar_two_diceOne_three.SetActive(true); break;
                    case 2: avatar_two_diceTwo_three.SetActive(true); break;
                    case 3: avatar_two_diceThree_three.SetActive(true); break;
                    case 4: avatar_two_diceFour_three.SetActive(true); break;
                    case 5: avatar_two_diceFive_three.SetActive(true); break;
                    case 6: avatar_two_diceSix_three.SetActive(true); break;
                }
            }
            if (combatUnitOne_DiceTotal > combatUnitTwo_DiceTotal)
            {
                combatUnitOneWins.SetActive(true);
            }
            else if (combatUnitOne_DiceTotal < combatUnitTwo_DiceTotal)
            {
                combatUnitTwoWins.SetActive(true);
            }
            diceResultAreShowing = true;
        }
    }

    void RandomDice()
    {
        int random = 0;
        avatar_one_diceOne_one.SetActive(false);
        avatar_one_diceTwo_one.SetActive(false);
        avatar_one_diceThree_one.SetActive(false);
        avatar_one_diceFour_one.SetActive(false);
        avatar_one_diceFive_one.SetActive(false);
        avatar_one_diceSix_one.SetActive(false);
        avatar_one_diceOne_two.SetActive(false);
        avatar_one_diceTwo_two.SetActive(false);
        avatar_one_diceThree_two.SetActive(false);
        avatar_one_diceFour_two.SetActive(false);
        avatar_one_diceFive_two.SetActive(false);
        avatar_one_diceSix_two.SetActive(false);
        avatar_one_diceOne_three.SetActive(false);
        avatar_one_diceTwo_three.SetActive(false);
        avatar_one_diceThree_three.SetActive(false);
        avatar_one_diceFour_three.SetActive(false);
        avatar_one_diceFive_three.SetActive(false);
        avatar_one_diceSix_three.SetActive(false);
        avatar_two_diceOne_one.SetActive(false);
        avatar_two_diceTwo_one.SetActive(false);
        avatar_two_diceThree_one.SetActive(false);
        avatar_two_diceFour_one.SetActive(false);
        avatar_two_diceFive_one.SetActive(false);
        avatar_two_diceSix_one.SetActive(false);
        avatar_two_diceOne_two.SetActive(false);
        avatar_two_diceTwo_two.SetActive(false);
        avatar_two_diceThree_two.SetActive(false);
        avatar_two_diceFour_two.SetActive(false);
        avatar_two_diceFive_two.SetActive(false);
        avatar_two_diceSix_two.SetActive(false);
        avatar_two_diceOne_three.SetActive(false);
        avatar_two_diceTwo_three.SetActive(false);
        avatar_two_diceThree_three.SetActive(false);
        avatar_two_diceFour_three.SetActive(false);
        avatar_two_diceFive_three.SetActive(false);
        avatar_two_diceSix_three.SetActive(false);
        for (int i = 0; i < combatUnitOne_AmountOfCombatDice; i++)
        {
            random = Random.Range(1,7);
            if (i == 0)
            {
                switch(random)
                {
                    case 1: avatar_one_diceOne_one.SetActive(true); break;
                    case 2: avatar_one_diceTwo_one.SetActive(true); break;
                    case 3: avatar_one_diceThree_one.SetActive(true); break;
                    case 4: avatar_one_diceFour_one.SetActive(true); break;
                    case 5: avatar_one_diceFive_one.SetActive(true); break;
                    case 6: avatar_one_diceSix_one.SetActive(true); break;
                }
            }
            if (i == 1)
            {
                switch(random)
                {
                    case 1: avatar_one_diceOne_two.SetActive(true); break;
                    case 2: avatar_one_diceTwo_two.SetActive(true); break;
                    case 3: avatar_one_diceThree_two.SetActive(true); break;
                    case 4: avatar_one_diceFour_two.SetActive(true); break;
                    case 5: avatar_one_diceFive_two.SetActive(true); break;
                    case 6: avatar_one_diceSix_two.SetActive(true); break;
                }
            }
            if (i == 2)
            {
                switch(random)
                {
                    case 1: avatar_one_diceOne_three.SetActive(true); break;
                    case 2: avatar_one_diceTwo_three.SetActive(true); break;
                    case 3: avatar_one_diceThree_three.SetActive(true); break;
                    case 4: avatar_one_diceFour_three.SetActive(true); break;
                    case 5: avatar_one_diceFive_three.SetActive(true); break;
                    case 6: avatar_one_diceSix_three.SetActive(true); break;
                }
            }
        }
        for (int x = 0; x < combatUnitTwo_AmountOfCombatDice; x++)
        {
            random = Random.Range(1,7);
            if (x == 0)
            {
                switch(random)
                {
                    case 1: avatar_two_diceOne_one.SetActive(true); break;
                    case 2: avatar_two_diceTwo_one.SetActive(true); break;
                    case 3: avatar_two_diceThree_one.SetActive(true); break;
                    case 4: avatar_two_diceFour_one.SetActive(true); break;
                    case 5: avatar_two_diceFive_one.SetActive(true); break;
                    case 6: avatar_two_diceSix_one.SetActive(true); break;
                }
            }
            if (x == 1)
            {
                switch(random)
                {
                    case 1: avatar_two_diceOne_two.SetActive(true); break;
                    case 2: avatar_two_diceTwo_two.SetActive(true); break;
                    case 3: avatar_two_diceThree_two.SetActive(true); break;
                    case 4: avatar_two_diceFour_two.SetActive(true); break;
                    case 5: avatar_two_diceFive_two.SetActive(true); break;
                    case 6: avatar_two_diceSix_two.SetActive(true); break;
                }
            }
            if (x == 2)
            {
                switch(random)
                {
                    case 1: avatar_two_diceOne_three.SetActive(true); break;
                    case 2: avatar_two_diceTwo_three.SetActive(true); break;
                    case 3: avatar_two_diceThree_three.SetActive(true); break;
                    case 4: avatar_two_diceFour_three.SetActive(true); break;
                    case 5: avatar_two_diceFive_three.SetActive(true); break;
                    case 6: avatar_two_diceSix_three.SetActive(true); break;
                }
            }
        }
    }

    void OnClickContinueButton()
    {
        GameMain.combatScreenEnabled = false;
        GameMain.combatEncounterHappening = false;
        GameMain.GUIEnabled = true;
        GUIManager.endTurnButtonEnabled = true;
        avatar_one_set = false;
        avatar_two_set = false;
        namesAreUpdated = false;
        diceResultAreShowing = false;
    }

    void OnClickFightButton()
    {
        preFightScreenActive = false;
        fightScreenActive = true;
        fightButton.gameObject.SetActive(false);
        CombatEncounter(tilemapStructures, player_red, player_blue, player_green, player_purple, player_white, monster_imp, monster_basilisk);
        continueButton.gameObject.SetActive(true);
    }

    void OnClickSuddenDeathButton()
    {
        suddenDeathButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    public static void CombatEncounterStart()
    {
        GameMain.combatEncounterHappening = true;
        GameMain.combatScreenEnabled = true;
        GameMain.GUIEnabled = false;
        // Sets combat unit one and combat unit two
        combatUnitOne = GameMain.currentPlayer;
        Debug.Log("Combat Unit One: " + combatUnitOne);
        /*if (GameMain.currentPlayer == 1)
        {
            combatUnitOne_AmountOfCombatDice = GameMain.player_combatDice_one;            
        }
        else if (GameMain.currentPlayer == 2)
        {
            combatUnitOne_AmountOfCombatDice = GameMain.player_combatDice_two;            
        }
        else if (GameMain.currentPlayer == 3)
        {
            combatUnitOne_AmountOfCombatDice = GameMain.player_combatDice_three;            
        }
        else if (GameMain.currentPlayer == 4)
        {
            combatUnitOne_AmountOfCombatDice = GameMain.player_combatDice_four;
        }
        if (World.currentUnitPositionOnBoard == GameMain.unitPositionPlayer1 && GameMain.currentPlayer != 1)
        {
            combatUnitTwo = 1;
            combatUnitTwo_AmountOfCombatDice = GameMain.player_combatDice_one;
        }
        else if (World.currentUnitPositionOnBoard == GameMain.unitPositionPlayer2 && GameMain.currentPlayer != 2)
        {
            combatUnitTwo = 2;
            combatUnitTwo_AmountOfCombatDice = GameMain.player_combatDice_two;
        }
        else if (World.currentUnitPositionOnBoard == GameMain.unitPositionPlayer3 && GameMain.currentPlayer != 3)
        {
            combatUnitTwo = 3;
            combatUnitTwo_AmountOfCombatDice = GameMain.player_combatDice_three;
        }
        else if (World.currentUnitPositionOnBoard == GameMain.unitPositionPlayer4 && GameMain.currentPlayer != 4)
        {
            combatUnitTwo = 4;
            combatUnitTwo_AmountOfCombatDice = GameMain.player_combatDice_four;
        }
        else if (GameMain.currentEnemy == "imp")
        {
            combatUnitTwo = 5;
            combatUnitTwo_AmountOfCombatDice = 1;
        }
        else if (GameMain.currentEnemy == "basilisk")
        {
            combatUnitTwo = 6;
            combatUnitTwo_AmountOfCombatDice = 2;
        }
        else if (GameMain.currentEnemy == "rampagingElephant")
        {
            combatUnitTwo = 7;
            combatUnitTwo_AmountOfCombatDice = 3;
        }*/
        preFightScreenActive = true;
    }

    public static void CombatEncounter(Tilemap tilemap, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white, Tile monsterImp, Tile monsterBasilisk)
    {
        /*GameMain.combatEncounterHappening = true;
        // Determines dice roll for combat unit one
        combatUnitOne_DiceTotal = 0;
        for (int x = 1; x <= combatUnitOne_AmountOfCombatDice; x++)
        {
            if (x == 1)
            {
                combatUnitOne_resultOne += Random.Range(1,7);
                combatUnitOne_DiceTotal += combatUnitOne_resultOne;
            }
            if (x == 2)
            {
                combatUnitOne_resultTwo += Random.Range(1,7);
                combatUnitOne_DiceTotal += combatUnitOne_resultTwo;
            }
            if (x == 3)
            {
                combatUnitOne_resultThree += Random.Range(1,7);
                combatUnitOne_DiceTotal += combatUnitOne_resultThree;
            }
        }
        // Determines dice roll for combat unit two
        combatUnitTwo_DiceTotal = 0;
        for (int x = 1; x <= GameMain.player_combatDice_one; x++)
        {
            if (x == 1)
            {
                combatUnitTwo_resultOne += Random.Range(1,7);
                combatUnitTwo_DiceTotal += combatUnitTwo_resultOne;
            }
            if (x == 2)
            {
                combatUnitTwo_resultTwo += Random.Range(1,7);
                combatUnitTwo_DiceTotal += combatUnitTwo_resultTwo;
            }
            if (x == 3)
            {
                combatUnitTwo_resultThree += Random.Range(1,7);
                combatUnitTwo_DiceTotal += combatUnitTwo_resultThree;
            }
        }
        // Combat Results
        if (combatUnitOne_DiceTotal > combatUnitTwo_DiceTotal)
        {
            Debug.Log("Player " + combatUnitOne + " has won!");
            // Imp Combat Rewards
            if (combatUnitTwo == 5)
            {
                if (combatUnitOne == 1)
                {
                    GameMain.playerOneGold += 250;
                }
                else if (combatUnitOne == 2)
                {
                    GameMain.playerTwoGold += 250;
                }
                else if (combatUnitOne == 3)
                {
                    GameMain.playerThreeGold += 250;
                }
                else if (combatUnitOne == 4)
                {
                    GameMain.playerFourGold += 250;
                }
            }
            // Basilisk Combat Rewards
            if (combatUnitTwo == 6)
            {
                if (combatUnitOne == 1)
                {
                    GameMain.playerOneGold += 500;
                }
                else if (combatUnitOne == 2)
                {
                    GameMain.playerTwoGold += 500;
                }
                else if (combatUnitOne == 3)
                {
                    GameMain.playerThreeGold += 500;
                }
                else if (combatUnitOne == 4)
                {
                    GameMain.playerFourGold += 500;
                }
            }
            GameMain.boardMonsters[World.currentUnitPositionOnBoard] = "empty";
            // Return combatUnitTwo to camp if it is a player
            if (combatUnitTwo == 1)
            {
                GameMain.player_in_camp_one = true;
                switch (GameMain.playerOneColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                }
                GameMain.unitPositionPlayer1 = 0;
                GameMain.livesPlayerOne -= 1;
                if (GameMain.livesPlayerOne <= 0)
                {
                    GameMain.player_alive_one = false;
                }
            }
            if (combatUnitTwo == 2)
            {
                GameMain.player_in_camp_two = true;
                switch (GameMain.playerTwoColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                }
                GameMain.unitPositionPlayer2 = 0;
                GameMain.livesPlayerTwo -= 1;
                if (GameMain.livesPlayerTwo <= 0)
                {
                    GameMain.player_alive_two = false;
                }
            }
            if (combatUnitTwo == 3)
            {
                GameMain.player_in_camp_three = true;
                switch (GameMain.playerThreeColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                }
                GameMain.unitPositionPlayer3 = 0;
                GameMain.livesPlayerThree -= 1;
                if (GameMain.livesPlayerThree <= 0)
                {
                    GameMain.player_alive_three = false;
                }
            }
            if (combatUnitTwo == 4)
            {
                GameMain.player_in_camp_four = true;
                switch (GameMain.playerFourColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                }
                GameMain.unitPositionPlayer4 = 0;
                GameMain.livesPlayerFour -= 1;
                if (GameMain.livesPlayerFour <= 0)
                {
                    GameMain.player_alive_four = false;
                }
            }
        }
        else if (combatUnitOne_DiceTotal < combatUnitTwo_DiceTotal)
        {
            Debug.Log("Player " + combatUnitTwo + " has won!");
            if (combatUnitOne == 1)
            {
                GameMain.player_in_camp_one = true;
                switch (GameMain.playerOneColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                }
                GameMain.unitPositionPlayer1 = 0;
                GameMain.livesPlayerOne -= 1;
                if (GameMain.livesPlayerOne <= 0)
                {
                    GameMain.player_alive_one = false;
                }
            }
            else if (combatUnitOne == 2)
            {
                GameMain.player_in_camp_two = true;
                switch (GameMain.playerTwoColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                }
                GameMain.unitPositionPlayer2 = 0;
                GameMain.livesPlayerTwo -= 1;
                if (GameMain.livesPlayerTwo <= 0)
                {
                    GameMain.player_alive_two = false;
                }
            }
            else if (combatUnitOne == 3)
            {
                GameMain.player_in_camp_three = true;
                switch (GameMain.playerThreeColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                }
                GameMain.unitPositionPlayer3 = 0;
                GameMain.livesPlayerThree -= 1;
                if (GameMain.livesPlayerThree <= 0)
                {
                    GameMain.player_alive_three = false;
                }
            }
            else if (combatUnitOne == 4)
            {
                GameMain.player_in_camp_four = true;
                switch (GameMain.playerFourColor)
                {
                    case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                }
                GameMain.unitPositionPlayer4 = 0;
                GameMain.livesPlayerFour -= 1;
                if (GameMain.livesPlayerFour <= 0)
                {
                    GameMain.player_alive_four = false;
                }
            }
            if (combatUnitTwo == 5)
            {
                GameMain.boardPosition = GameMain.boardPositions[World.currentUnitPositionOnBoard];
                tilemap.SetTile(new Vector3Int((int)GameMain.boardPosition[0], (int)GameMain.boardPosition[1]), monsterImp);
            }
            else if (combatUnitTwo == 6)
            {
                GameMain.boardPosition = GameMain.boardPositions[World.currentUnitPositionOnBoard];
                tilemap.SetTile(new Vector3Int((int)GameMain.boardPosition[0], (int)GameMain.boardPosition[1]), monsterBasilisk);
            }
        }
        else if (combatUnitOne_DiceTotal == combatUnitTwo_DiceTotal)
        {
            Debug.Log("Tied combat! SUDDEN DEATH!");
            int rand = Random.Range(1,3);
            if (rand == 1)
            {
                Debug.Log("Player " + GameMain.currentPlayer + " has won!");
                GameMain.boardMonsters[World.currentUnitPositionOnBoard] = "empty";
                // Return combatUnitTwo to camp if it is a player
                if (combatUnitTwo == 1)
                {
                    GameMain.player_in_camp_one = true;
                    switch (GameMain.playerOneColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                    }
                    GameMain.unitPositionPlayer1 = 0;
                    GameMain.livesPlayerOne -= 1;
                    if (GameMain.livesPlayerOne <= 0)
                    {
                        GameMain.player_alive_one = false;
                    }
                }
                else if (combatUnitTwo == 2)
                {
                    GameMain.player_in_camp_two = true;
                    switch (GameMain.playerTwoColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                    }
                    GameMain.unitPositionPlayer2 = 0;
                    GameMain.livesPlayerTwo -= 1;
                    if (GameMain.livesPlayerTwo <= 0)
                    {
                        GameMain.player_alive_two = false;
                    }
                }
                else if (combatUnitTwo == 3)
                {
                    GameMain.player_in_camp_three = true;
                    switch (GameMain.playerThreeColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                    }
                    GameMain.unitPositionPlayer3 = 0;
                    GameMain.livesPlayerThree -= 1;
                    if (GameMain.livesPlayerThree <= 0)
                    {
                        GameMain.player_alive_three = false;
                    }
                }
                else if (combatUnitTwo == 4)
                {
                    GameMain.player_in_camp_four = true;
                    switch (GameMain.playerFourColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                    }
                    GameMain.unitPositionPlayer4 = 0;
                    GameMain.livesPlayerFour -= 1;
                    if (GameMain.livesPlayerFour <= 0)
                    {
                        GameMain.player_alive_four = false;
                    }
                }
            }
            else if (rand == 2)
            {
                Debug.Log("Player " + combatUnitTwo + " has won!");
                if (combatUnitOne == 1)
                {
                    GameMain.player_in_camp_one = true;
                    switch (GameMain.playerOneColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                    }
                    GameMain.unitPositionPlayer1 = 0;
                    GameMain.livesPlayerOne -= 1;
                    if (GameMain.livesPlayerOne <= 0)
                    {
                        GameMain.player_alive_one = false;
                    }
                }
                else if (combatUnitOne == 2)
                {
                    GameMain.player_in_camp_two = true;
                    switch (GameMain.playerTwoColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                    }
                    GameMain.unitPositionPlayer2 = 0;
                    GameMain.livesPlayerTwo -= 1;
                    if (GameMain.livesPlayerTwo <= 0)
                    {
                        GameMain.player_alive_two = false;
                    }
                }
                else if (combatUnitOne == 3)
                {
                    GameMain.player_in_camp_three = true;
                    switch (GameMain.playerThreeColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                    }
                    GameMain.unitPositionPlayer3 = 0;
                    GameMain.livesPlayerThree -= 1;
                    if (GameMain.livesPlayerThree <= 0)
                    {
                        GameMain.player_alive_three = false;
                    }
                }
                else if (combatUnitOne == 4)
                {
                    GameMain.player_in_camp_four = true;
                    switch (GameMain.playerFourColor)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                    }
                    GameMain.unitPositionPlayer4 = 0;
                    GameMain.livesPlayerFour -= 1;
                    if (GameMain.livesPlayerFour <= 0)
                    {
                        GameMain.player_alive_four = false;
                    }
                }
                // Set monster avatar on the board
                if (combatUnitTwo == 5)
                {
                    GameMain.boardPosition = GameMain.boardPositions[World.currentUnitPositionOnBoard];
                    tilemap.SetTile(new Vector3Int((int)GameMain.boardPosition[0], (int)GameMain.boardPosition[1]), monsterImp);
                }
                else if (combatUnitTwo == 6)
                {
                    GameMain.boardPosition = GameMain.boardPositions[World.currentUnitPositionOnBoard];
                    tilemap.SetTile(new Vector3Int((int)GameMain.boardPosition[0], (int)GameMain.boardPosition[1]), monsterBasilisk);
                }
            }
        }*/
    }
}