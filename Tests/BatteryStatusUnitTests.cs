using BvWinFormsLib;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Tests
{
    public class BatteryStatusUnitTests
    {
        int[] calls { get; set; } = [90, 85, 80, 75, 70, 65, 60];

        [Theory]
        [InlineData(100, false)]
        [InlineData(99, false)]
        [InlineData(91, false)]
        [InlineData(90, true)]
        [InlineData(87, true)]
        [InlineData(85, true)]
        [InlineData(70, true)]
        public void BatteryLevelCall_Call1(int level, bool expected)
        {
            BatteryStatus.Init(calls);
            var res = BatteryStatus.BatteryLevelCall(level, false);
            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData(100, false, 99, false)]
        [InlineData(100, false, 90, true)]
        [InlineData(90, true, 89, false)]
        [InlineData(80, true, 70, true)]
        [InlineData(80, true, 78, false)]
        [InlineData(60, true, 55, false)]
        [InlineData(40, true, 30, false)]
        public void BatteryLevelCall_Call2(int level1, bool expected1, int level2, bool expected2)
        {
            BatteryStatus.Init(calls);
            var res = BatteryStatus.BatteryLevelCall(level1, false);
            Assert.Equal(expected1, res);
            res = BatteryStatus.BatteryLevelCall(level2, false);
            Assert.Equal(expected2, res);
        }

        [Fact]
        public void BatteryLevelCall_Charging()
        {
            //TODO...
            BatteryStatus.Init(calls);
            // discharge
            BatteryStatus.BatteryLevelCall(80, false);
            // charge
            BatteryStatus.BatteryLevelCall(100, true);
            // discharge
            var res = BatteryStatus.BatteryLevelCall(90, false);
            Assert.Equal(true, res);

        }
    }
}
