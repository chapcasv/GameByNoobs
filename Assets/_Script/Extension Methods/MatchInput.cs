using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class MatchInput
    {
        public static bool CardCostMatchInputCost(int cost, int inputCost, CostMode costMode)
        {

            switch (costMode)
            {
                case CostMode.EQUAL:
                    return IsMatchCostModeEqual(cost, inputCost);
                case CostMode.LOWER:
                    return IsMatchCostModeLower(cost, inputCost);
                case CostMode.UPPER:
                    return IsMatchCostModeUpper(cost, inputCost);
                default:
                    throw new System.Exception("Cant get cost Mode");
            }
        }

        private static bool IsMatchCostModeUpper(int cost, int inputCost)
        {
            if (cost >= inputCost) return true;
            else return false;
        }

        private static bool IsMatchCostModeEqual(int cost, int inputCost)
        {
            if (cost == inputCost) return true;
            else return false;
        }

        private static bool IsMatchCostModeLower(int cost, int inputCost)
        {
            if (cost <= inputCost) return true;
            else return false;
        }
    }
}

