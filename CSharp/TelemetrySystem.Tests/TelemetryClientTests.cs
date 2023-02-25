using System;
using Moq;
using Shouldly;
using Xunit;

namespace TDDMicroExercises.TelemetrySystem.Tests;

public class TelemetryClientTests
{
    private readonly TelemetryClient _sut;
    private readonly Mock<IEventsSimulator> _eventsSimulator;

    public TelemetryClientTests()
    {
        _eventsSimulator = new Mock<IEventsSimulator>();
        _sut = new TelemetryClient(_eventsSimulator.Object);
        //_sut = new TelemetryClient();
    }
        
    [Fact]
    public void Connect_WithValidConnectionString_ShouldSuccessfullyConnect()
    {   
        // Act
        _eventsSimulator.Setup(x => x.CanConnect()).Returns(true);
        _sut.Connect("anyConnection");

        // Assert
        _sut.OnlineStatus.ShouldBeTrue();
    }  
    
    [Fact]
    public void Connect_WithInValidConnectionString_ShouldThrow()
    {   
        // Act & Assert
        Should.Throw<ArgumentNullException>(() => _sut.Connect(string.Empty));
    }
        
    [Fact]
    public void Disconnect_ShouldSuccessfullyConnect()
    {   
        // Arrange
        _eventsSimulator.Setup(x => x.CanConnect()).Returns(true);
        _sut.Connect("anyConnection");
        _sut.OnlineStatus.ShouldBeTrue();
            
        // Act
        _sut.Disconnect();

        // Assert
        _sut.OnlineStatus.ShouldBeFalse();
    }

    [Fact]
    public void Send_WithValidComment_ShouldSuccessfullyCreateAMessage()
    {   
        // Arrange
        _eventsSimulator.Setup(x => x.CanConnect()).Returns(true);
        _sut.Connect("anyConnection");
        _sut.OnlineStatus.ShouldBeTrue();
        var expectedMessage = ExpectedDiagnosticMessage();
            
        // Act
        _sut.Send(TelemetryClient.DiagnosticMessage);
        var result = _sut.Receive();
        
        // Assert
        result.ShouldNotBeNullOrEmpty();
        result.ShouldBe(expectedMessage);
    }
    
    [Fact]
    public void Send_WithInValidConnectionString_ShouldThrow()
    {   
        // Act & Assert
        Should.Throw<ArgumentNullException>(() => _sut.Send(string.Empty));
    }
    

    [Fact]
    public void Receive_SendWithDifferentMessage_ShouldReturnRandomMessage()
    {   
        // Arrange
        var expectedMessage = "Believe_And_Work_Hard";
        _eventsSimulator.Setup(x => x.Message()).Returns(expectedMessage);

            
        // Act
        _sut.Send("random");
        var result = _sut.Receive();
        
        // Assert
        result.ShouldNotBeNullOrEmpty();
        result.ShouldBe(expectedMessage);
    }

    private static string ExpectedDiagnosticMessage()
    {
        var expectedMessage =
            "LAST TX rate................ 100 MBPS\r\n"
            + "HIGHEST TX rate............. 100 MBPS\r\n"
            + "LAST RX rate................ 100 MBPS\r\n"
            + "HIGHEST RX rate............. 100 MBPS\r\n"
            + "BIT RATE.................... 100000000\r\n"
            + "WORD LEN.................... 16\r\n"
            + "WORD/FRAME.................. 511\r\n"
            + "BITS/FRAME.................. 8192\r\n"
            + "MODULATION TYPE............. PCM/FM\r\n"
            + "TX Digital Los.............. 0.75\r\n"
            + "RX Digital Los.............. 0.10\r\n"
            + "BEP Test.................... -5\r\n"
            + "Local Rtrn Count............ 00\r\n"
            + "Remote Rtrn Count........... 00";
        return expectedMessage;
    }
}