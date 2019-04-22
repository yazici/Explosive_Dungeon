using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class Achievements : MonoBehaviour
{
    public double progress = 100.0f;
    public static Achievements Instance;
    public bool authenticated;
    public enum AchievementsType
    {
    //Visit the first cave+
    Adventurer,
    //Die once by the explosion+
    Bang,
    //Die by the explosion 100 times+
    Unlucky,
    //Open 50 diamond chests+ 
    TreasureHunter,
    //Open the third cave+
    NewJob,
    //Die 50 times in the third cave+
    Defender,
    //Die from crabs once+
    Panic,
    //Die from the laser TNT 100 times+
    LaserParty,
    //Die from the laser EYE 100 times+
    AllSeingEye,
    //Die from fish 20 times+
    Fisher,
    //Open 15 glitch chests+
    Secret,
    //Collect 50 diamonds in one game
    WOW,
    //Collect 110 diamonds in one game
    TryHard,
    //Collect 300 diamonds in one game
    OMG,
    //Open any 15 chests under armor effect
    Ghost,
    //Jump 50 times in one game
    FarDistances,
    //Jump 40 times under the armor effect in one game
    HighStakes
    }   
    public static SaveAchievements AchievementSave = new SaveAchievements();
    bool first = true;
    void Start()
    {
        
        LoadAllData();
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        if(Achievements.Instance == null) 
            DontDestroyOnLoad(this);
        Instance = this;
        if(first)
        {
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => {
            authenticated = success;
            GameObject.Find("aut").GetComponent<Text>().text = "Authenticated: "+ success.ToString() + "; " + SystemInfo.deviceUniqueIdentifier.ToUpper();
            first = false;
        });
        }
    }
    public void ShowAchievements(){
        Social.ShowAchievementsUI();
    }
    public void ShowLeader(){
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
    public void GiveAchievement(AchievementsType AchievementType)
    {
        switch (AchievementType)
        {
            case AchievementsType.Adventurer: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQAQ",(bool success)=>{
            print(success);}); break;
            case AchievementsType.Bang: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQAw", (bool success)=>{
            print(success);}); break;
            case AchievementsType.Unlucky: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQBA", (bool success)=>{
            print(success);}); break;
            case AchievementsType.TreasureHunter: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQBQ", (bool success)=>{
            print(success);}); break;
            case AchievementsType.NewJob: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQBg", (bool success)=>{
            print(success);}); break;
            case AchievementsType.Defender: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQBw", (bool success)=>{
            print(success);}); break;
            case AchievementsType.Panic: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQCA", (bool success)=>{
            print(success);}); break;
            case AchievementsType.LaserParty: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQCQ", (bool success)=>{
            print(success);}); break;
            case AchievementsType.AllSeingEye: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQCg", (bool success)=>{
            print(success);}); break;
            case AchievementsType.Fisher: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQCw", (bool success)=>{
            print(success);}); break;
            case AchievementsType.Secret: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQDA", (bool success)=>{
            print(success);}); break;
            case AchievementsType.WOW: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQEw", (bool success)=>{
            print(success);}); break;
            case AchievementsType.TryHard: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQFA", (bool success)=>{
            print(success);}); break;
            case AchievementsType.OMG: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQFQ", (bool success)=>{
            print(success);}); break;
            case AchievementsType.Ghost: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQFg", (bool success)=>{
            print(success);}); break;
            case AchievementsType.FarDistances: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQFw", (bool success)=>{
            print(success);}); break;
            case AchievementsType.HighStakes: PlayGamesPlatform.Instance.UnlockAchievement("CgkIyriH1Y4ZEAIQGA", (bool success)=>{
            print(success);}); break;
            default: print("Error! This achievement don't found!"); break;
        }
        Debug.Log("Выдано достижение: " + AchievementType.ToString());
    }
    public void SaveAllData()
    {
        PlayerPrefs.SetString("SavedAchievements", JsonUtility.ToJson(AchievementSave));
    }
    public void LoadAllData()
    {
        if(PlayerPrefs.HasKey("SavedAchievements"))
        AchievementSave = JsonUtility.FromJson<SaveAchievements>(PlayerPrefs.GetString("SavedAchievements"));
        else
            SaveAllData();
    }
}
[System.Serializable]
public class SaveAchievements 
{
    public int diedByExplosion, 
        diedByCrabs, 
        diamondChestsOpened, 
        diedInThirdCave,
        diedByLaserEye,
        diedByLaserTNT,
        diedByFish,
        glitchChestsOpened,
        openedChestUnderInvisibility,
        jumpedTimesInOneGame,
        jumpedTimesInOneGameUnderArmorEffect,
        diamondsCollectedInOneGame
         = 0;
        
    public bool firstCaveVisited,
        thirdCaveOpened;
    public void ClearOneGameAchievements()
    {
        DiamondChestsOpened = 0;
        JumpedTimesInOneGame = 0;
        JumpedTimesInOneGameUnderArmorEffect = 0;
        Achievements.Instance.SaveAllData();
    }
    public int JumpedTimesInOneGame
    {
        get{return jumpedTimesInOneGame;}
        set
        {
            if(Achievements.Instance.authenticated){
            jumpedTimesInOneGame = value;
            if(value == 50)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.FarDistances);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int JumpedTimesInOneGameUnderArmorEffect
    {
        get{return jumpedTimesInOneGameUnderArmorEffect;}
        set
        {
            if(Achievements.Instance.authenticated){
            jumpedTimesInOneGameUnderArmorEffect = value;
            if(value == 40)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.HighStakes);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiamondsCollectedInOneGame
    {
        get{return diamondsCollectedInOneGame;}
        set
        {
            if(Achievements.Instance.authenticated){
            diamondsCollectedInOneGame = value;
            if(value == 50)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.WOW);
            if(value == 110)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.TryHard);
            if(value == 300)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.OMG);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int OpenedChestUnderInvisibility
    {
        get{return openedChestUnderInvisibility;}
        set
        {
            if(Achievements.Instance.authenticated){
            openedChestUnderInvisibility = value;
            if(value == 15)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Ghost);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int GlitchChestsOpened
    {
        get{return glitchChestsOpened;}
        set
        {
            if(Achievements.Instance.authenticated){
            glitchChestsOpened = value;
            if(value == 15)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Secret);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiedByLaserTNT
    {
        get{return diedByLaserTNT;}
        set
        {
            if(Achievements.Instance.authenticated){
            diedByLaserTNT = value;
            if(value == 100)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.LaserParty);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiedByLaserEye
    {
        get{return diedByLaserEye;}
        set
        {
            if(Achievements.Instance.authenticated){
            diedByLaserEye = value;
            if(value == 100)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.AllSeingEye);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiedByFish
    {
        get{return diedByFish;}
        set
        {
            if(Achievements.Instance.authenticated){
            diedByFish = value;
            if(value == 20)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Fisher);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiamondChestsOpened
    {
        get{return diamondChestsOpened;}
        set
        {
            if(Achievements.Instance.authenticated){
            diamondChestsOpened = value;
            if(value == 50)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.TreasureHunter);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public bool ThirdCaveOpened
    {
        get{return thirdCaveOpened;}
        set
        {
            if(Achievements.Instance.authenticated){
            if(!thirdCaveOpened)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.NewJob);
            thirdCaveOpened = value;
            Achievements.Instance.SaveAllData();
            Debug.Log("ВЫДАЯНО");
            }else{Debug.Log("da eto pizdec");}
        }
    }
    public bool FirstCaveVisited
    {
        get{return firstCaveVisited;}
        set
        {
            if(Achievements.Instance.authenticated){
            if(!firstCaveVisited)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Adventurer);
            firstCaveVisited = value;
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiedByCrabs
    {
        get {return diedByCrabs;}
        set
        {
            if(Achievements.Instance.authenticated){
            diedByCrabs = value;
            if(value == 1)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Panic);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiedInThirdCave
    {
        get{return diedInThirdCave;}
        set
        {
            if(Achievements.Instance.authenticated){
            diedInThirdCave = value;
            if(value == 50)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Defender);
            Achievements.Instance.SaveAllData();
            }
        }
    }
    public int DiedByExplosion
    {
        get {return diedByExplosion;}
        set
        {
            if(Achievements.Instance.authenticated){
            diedByExplosion = value;
            if(value == 1)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Bang);
            else if(value == 100)
                Achievements.Instance.GiveAchievement(Achievements.AchievementsType.Unlucky);
            Achievements.Instance.SaveAllData();
            }
        }
    }
}
