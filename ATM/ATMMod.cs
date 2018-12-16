using Microsoft.Xna.Framework;
using PyTK.Types;
using PyTK.Extensions;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using PyTK.Tiled;
using System.IO;
using StardewValley.Menus;

namespace ATM
{
    public class ATMMod : Mod
    {
        internal static Config config;
        internal ITranslationHelper i18n => Helper.Translation;
        //internal BankAccount bankAccount;
        internal List<Response> responses;

        public override void Entry(IModHelper helper)
        {
            config = helper.ReadConfig<Config>();

            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
            SaveEvents.BeforeSave += SaveEvents_BeforeSave;
            TimeEvents.AfterDayStarted += TimeEvents_AfterDayStarted;

            responses = new List<Response>();
            responses.Add(new Response("ATM_Deposit", i18n.Get("ATM_Deposit")));
            responses.Add(new Response("ATM_Daily_Deposit", i18n.Get("ATM_Daily_Deposit")));
            responses.Add(new Response("ATM_Withdraw", i18n.Get("ATM_Withdraw")));
            responses.Add(new Response("ATM_Close", i18n.Get("ATM_Close")));

            var openATMAction = new TileAction("OpenATM", openATM);
            openATMAction.register();

            var atm = TMXContent.Load(Path.Combine("Assets", "atm.tmx"), Helper);
            atm.injectInto(@"Maps/" + config.Map, new Vector2(config.Position[0], config.Position[1]), null);
        }

        private bool openATM(string action, GameLocation location, Vector2 tileposition, string layer)
        {
            if (!Game1.IsMasterGame)
            {
                Game1.addHUDMessage(new HUDMessage(i18n.Get("Fail_Main")));
                return true;
            }

            //string text = i18n.Get("Account_Ballance") + ": " + bankAccount.Balance + "g. " + (config.Credit ? i18n.Get("Line_Credit") + ": " + bankAccount.CreditLine + "g" : "");
            //Game1.currentLocation.createQuestionDialogue(text, responses.ToArray(), nextMenu);
            return true;
        }

        private void nextMenu(Farmer who, string key)
        {
            if (key == "ATM_Close")
                return;

            var text = responses.Find(r => r.responseKey == key).responseText;

            //Game1.activeClickableMenu = new NumberSelectionMenu(text, (number, price, farmer) => processOrder(number, price, farmer, key), -1, 0, (key != "ATM_Withdraw") ? (key == "ATM_Daily_Deposit") ? Math.Max(Game1.player.money, bankAccount.DailyMoneyOrder) : Game1.player.Money : bankAccount.AvailableMoney, (key == "ATM_Daily_Deposit") ? bankAccount.DailyMoneyOrder : Math.Min((key != "ATM_Withdraw") ? Game1.player.money : bankAccount.AvailableMoney, 100));
        }

        private void TimeEvents_AfterDayStarted(object sender, EventArgs e)
        {
            if (Game1.IsMasterGame)
            {
                //if (bankAccount.DailyMoneyOrder > 0 && bankAccount.DailyMoneyOrder < Game1.player.Money)
                //{
                //    Game1.player.Money -= bankAccount.DailyMoneyOrder;
                //    bankAccount.ActualBalance += bankAccount.DailyMoneyOrder;
                //    Game1.addHUDMessage(new HUDMessage(i18n.Get("Daily_Deposite") + ": " + bankAccount.DailyMoneyOrder + "g", 2));
                //}

                //setInterest();

                //if (Game1.dayOfMonth == 1)
                //{
                //    if (Game1.currentSeason.ToLower() == "spring")
                //        setCreditLine();

                //    payInterest();
                //}
            }
        }

        private void SaveEvents_BeforeSave(object sender, System.EventArgs e)
        {
            //if (Game1.IsMasterGame)
                //Helper.Data.WriteSaveData("Platonymous.ATM.BankAccount", bankAccount);
        }

        private void SaveEvents_AfterLoad(object sender, System.EventArgs e)
        {
            if (!Game1.IsMasterGame)
                return;
        }
    }
}
