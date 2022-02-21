namespace PH
{
    public static class MatchInput
    {
        public static bool CardCostMatchInputCost(int cost, int inputCost, CostMode costMode)
        {

            return costMode switch
            {
                CostMode.EQUAL => IsMatchCostModeEqual(cost, inputCost),
                CostMode.LOWER => IsMatchCostModeLower(cost, inputCost),
                CostMode.UPPER => IsMatchCostModeUpper(cost, inputCost),
                _ => throw new System.Exception("Cant get cost Mode"),
            };
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

