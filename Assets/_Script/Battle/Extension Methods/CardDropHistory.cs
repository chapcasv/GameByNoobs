using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class CardDropHistory
    {
        private static List<Card> allHistory;

        private static Dictionary<UnitTeam, List<Card>> historyCardDrop;

        private static Dictionary<UnitTeam, List<CardItem>> historyCardItemDrop;

        private static Dictionary<UnitTeam, List<CardUnit>> historyCardUnitDrop;

        private static Dictionary<UnitTeam, List<CardSpell>> historyCardSpellDrop;

        private static bool isInit = false;

        public static void Reset() => isInit = false;

        public static void Init()
        {
            if (isInit) return;

            InitHistory();

            isInit = true;
        }

        private static void InitHistory()
        {
            allHistory = new List<Card>();

            historyCardDrop = new Dictionary<UnitTeam, List<Card>>();
            historyCardDrop.Add(UnitTeam.Player, new List<Card>());
            historyCardDrop.Add(UnitTeam.Enemy, new List<Card>());

            historyCardItemDrop = new Dictionary<UnitTeam, List<CardItem>>();
            historyCardItemDrop.Add(UnitTeam.Player, new List<CardItem>());
            historyCardItemDrop.Add(UnitTeam.Enemy, new List<CardItem>());

            historyCardUnitDrop = new Dictionary<UnitTeam, List<CardUnit>>();
            historyCardUnitDrop.Add(UnitTeam.Player, new List<CardUnit>());
            historyCardUnitDrop.Add(UnitTeam.Enemy, new List<CardUnit>());

            historyCardSpellDrop = new Dictionary<UnitTeam, List<CardSpell>>();
            historyCardSpellDrop.Add(UnitTeam.Player, new List<CardSpell>());
            historyCardSpellDrop.Add(UnitTeam.Enemy, new List<CardSpell>());
        }

        public static void AddCardUnitSpawn(CardUnit unit, UnitTeam team)
        {
            allHistory.Add(unit);
            historyCardDrop[team].Add(unit);
            historyCardUnitDrop[team].Add(unit);
        }

        public static void AddCardItemDrop(CardItem cardItem, UnitTeam team)
        {
            allHistory.Add(cardItem);
            historyCardDrop[team].Add(cardItem);
            historyCardItemDrop[team].Add(cardItem);
        }

        public static List<CardItem> GetCardItems(UnitTeam team) => historyCardItemDrop[team];
    }
}


