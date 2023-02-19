using Moq;
using Shouldly;
using Xunit;

namespace TDDMicroExercises.TurnTicketDispenser.Tests;

public class TicketDispenserTest
{
    [Fact]
    public void TicketDispenser_EveryCall_ShouldReturnANewTicket()
    {
        // Arrange
        var ticketDispenser = new TicketDispenser();

        // Act
        var firstTicket = ticketDispenser.GetTurnTicket();
        var secondTicket = ticketDispenser.GetTurnTicket();
        var thirdTicket = ticketDispenser.GetTurnTicket();

        // Assert
        firstTicket.TurnNumber.ShouldBe(0);
        secondTicket.TurnNumber.ShouldBe(1);
        thirdTicket.TurnNumber.ShouldBe(2);
    }
    
    /// <summary>
    /// There may be more than one ticket dispenser but the same
    /// ticket should not be issued to two different customers.
    /// </summary>
    /// <remarks>This should be in a Acceptance test class</remarks>
    [Fact]
    public void TicketDispenser_WithMultipleInstance_ShouldNotDuplicateNumbers()
    {
        // Arrange
        var turnNumberSequence = new TurnNumberSequence();
        var ticketDispenser1 = new TicketDispenser(turnNumberSequence);
        var ticketDispenser2 = new TicketDispenser(turnNumberSequence);

        // Act
        var firstTicketFromDispenser1 = ticketDispenser1.GetTurnTicket();
        var firstTicketFromDispenser2 = ticketDispenser2.GetTurnTicket();
        var secondTicketFromDispenser1 = ticketDispenser1.GetTurnTicket();
    
        // Assert
        firstTicketFromDispenser1.TurnNumber.ShouldBe(0);
        firstTicketFromDispenser2.TurnNumber.ShouldBe(1);
        secondTicketFromDispenser1.TurnNumber.ShouldBe(2);
    }
    
    [Fact]
    public void TicketDispenser_LastTicketWas26_ShouldReturn27()
    {
        // Arrange
        var turnNumberMock = new Mock<ITurnNumberSequence>();
        turnNumberMock.Setup(x => x.GetNextTurnNumber()).Returns(27);
        
        var ticketDispenser = new TicketDispenser(turnNumberMock.Object);

        // Act
        var nextTicket = ticketDispenser.GetTurnTicket();

        // Assert
        nextTicket.TurnNumber.ShouldBe(27);
    }
}