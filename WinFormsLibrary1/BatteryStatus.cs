
namespace BvWinFormsLib
{
    public static class BatteryStatus
    {
        private static PowerStatus Status => SystemInformation.PowerStatus;

        public static bool IsCharging => Status.PowerLineStatus == PowerLineStatus.Online;

        public static int MinutesRemaining => Status.BatteryLifeRemaining / 60;

        public static int BatteryLevel => (int)(Status.BatteryLifePercent * 100);

        public new static string ToString()
        {
            return (Status.BatteryLifePercent * 100) + "%, " +
                (IsCharging ? "charging..." : $"Remaining: {MinutesRemaining / 60}h {MinutesRemaining % 60}min");
        }

        public static int[] BatteryLevelCalls { get; set; } = [90, 85, 80, 75, 70, 65, 60];

        private static int idxBatteryLevelCalls = 0;

        public static bool BatteryLevelCall(int batteryLevel = -1, bool? isCharging = null)
        {
            if (batteryLevel == -1)
                batteryLevel = BatteryLevel;
            if (!isCharging.HasValue)
                isCharging = IsCharging;
            if (isCharging.Value)
            {
                Charging();
                return false;
            }
            else
                return Discharging(batteryLevel);
        }

        private static bool Discharging(int batteryLevel)
        {
            var res = false;
            while (batteryLevel <= BatteryLevelCalls[idxBatteryLevelCalls])
                if (idxBatteryLevelCalls < BatteryLevelCalls.Length - 1)
                {
                    idxBatteryLevelCalls++;
                    res = true;
                }
                else
                    break;
            return res;
            //if (batteryLevel <= BatteryLevelCalls[idxBatteryLevelCalls])
            //{
            //    idxBatteryLevelCalls++;
            //    return true;
            //}
            //else
            //    return false;
        }

        private static void Charging(int batteryLevel = -1)
        {
            //TODO...
        }

        public static void Init(int[] batteryLevelCalls)
        {
            BatteryLevelCalls = batteryLevelCalls;
            //TODO check if elements of (non empty) array are [0..100] in descending order
            idxBatteryLevelCalls = 0;
        }
    }
}
