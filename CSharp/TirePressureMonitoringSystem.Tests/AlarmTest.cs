using Moq;
using Xunit;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class AlarmTest
    {
        private readonly Mock<ISensor> _sensor;

        public AlarmTest()
        {
            _sensor = new Mock<ISensor>();
        }

        [Fact]
        public void Alarm_AfterStart_ShouldReturnFalse()
        {
            _sensor.Setup(x => x.PopNextPressurePsiValue()).Returns(Alarm.LowPressureThreshold + 1);
            var alarm = new Alarm(_sensor.Object);
            alarm.Check();
            Assert.False(alarm.AlarmOn);
        }

        [Theory]
        [InlineData(Alarm.LowPressureThreshold - 1)]
        [InlineData(Alarm.HighPressureThreshold + 1)]
        public void Alarm_OutsideValues_ShouldReturnTrue(double pressure)
        {
            // Arrange
            _sensor.Setup(x => x.PopNextPressurePsiValue()).Returns(pressure);
            var alarm = new Alarm(_sensor.Object);
            
            // Act
            alarm.Check();
            
            // Assert
            Assert.True(alarm.AlarmOn);
        }

        [Fact]
        public void Alarm_AfterTriggeringTheAlarmOnce_ShouldKeepReturningTrue()
        {
            // Arrange
            _sensor.SetupSequence(x => x.PopNextPressurePsiValue())
                .Returns(Alarm.LowPressureThreshold - 1)
                .Returns(Alarm.HighPressureThreshold - 1)
                .Returns(Alarm.LowPressureThreshold + 1);
                
            var alarm = new Alarm(_sensor.Object);
            
            // Act
            alarm.Check();
            alarm.Check();
            alarm.Check();

            // Assert
            Assert.True(alarm.AlarmOn);
        }
    }
}