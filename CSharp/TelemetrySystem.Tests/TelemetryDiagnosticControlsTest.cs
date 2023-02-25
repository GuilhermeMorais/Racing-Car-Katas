using System;
using Moq;
using Shouldly;
using Xunit;

namespace TDDMicroExercises.TelemetrySystem.Tests
{
    public class TelemetryDiagnosticControlsTest
    {
        private readonly TelemetryDiagnosticControls _sut;
        private readonly Mock<ITelemetryClient> _client;

        public TelemetryDiagnosticControlsTest()
        {
            _client = new Mock<ITelemetryClient>();
            _sut = new TelemetryDiagnosticControls(_client.Object);

        }

        [Fact]
        public void CheckTransmission_should_send_a_diagnostic_message_and_receive_a_status_message_response()
        {
            // Arrange
            var telemetryControl = new TelemetryDiagnosticControls();

            // Act
            telemetryControl.CheckTransmission();

            // Assert
            telemetryControl.DiagnosticInfo.ShouldNotBeNullOrEmpty();
            telemetryControl.DiagnosticInfo.ShouldContain("WORD LEN");
        }

        [Fact]
        public void CheckTransmission_WithDiagnosticCommand_ShouldReturnDiagnosticInfo()
        {
            // Arrange
            var expected = "expected return";
            _client.SetupSequence(x => x.OnlineStatus)
                .Returns(false)
                .Returns(true)
                .Returns(true);
            _client.Setup(x => x.Receive())
                .Returns(expected);

            // Act
            _sut.CheckTransmission();


            // Assert
            _sut.DiagnosticInfo.ShouldContain(expected);
        }

        [Fact]
        public void CheckTransmission_WithFailToConnect_ShouldThrow()
        {
            // Arrange
            var expectedMessage = "Unable to connect.";
            _client.SetupSequence(x => x.OnlineStatus)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false);
            
            // Act
            // Assert
            Should.Throw<Exception>(() => _sut.CheckTransmission(), expectedMessage);
        }
    }
}
