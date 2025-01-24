namespace CardActionsService.Tests;

public class CardServiceTests
{
    [Fact]
    public async Task GetCardDetails_ReturnsCardDetails_WhenCardExists()
    {
        var cardService = new CardService();
        var cardDetails = await cardService.GetCardDetails("User1", "Card11");

        Assert.NotNull(cardDetails);
        Assert.Equal("Card11", cardDetails.CardNumber);
        Assert.Equal(CardType.Prepaid, cardDetails.CardType);
        Assert.Equal(CardStatus.Ordered, cardDetails.CardStatus);
        Assert.False(cardDetails.IsPinSet);
    }

    [Fact]
    public async Task GetCardDetails_ReturnsNull_WhenUserDoesNotExist()
    {
        var cardService = new CardService();
        var cardDetails = await cardService.GetCardDetails("NonExistentUser", "Card11");

        Assert.Null(cardDetails);
    }

    [Fact]
    public async Task GetCardDetails_ReturnsNull_WhenCardDoesNotExist()
    {
        var cardService = new CardService();
        var cardDetails = await cardService.GetCardDetails("User1", "NonExistentCard");

        Assert.Null(cardDetails);
    }

    [Fact]
    public void GetAllUserCards_ReturnsAllUserCards()
    {
        var cardService = new CardService();
        var allUserCards = cardService.GetAllUserCards();

        Assert.NotNull(allUserCards);
        Assert.Equal(3, allUserCards.Count);
    }
    
    [Fact]
    public async Task GetCardDetails_ThrowsArgumentNullException_WhenCardNumberIsEmpty()
    {
        var cardService = new CardService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => cardService.GetCardDetails("User1", string.Empty));
    }

    [Fact]
    public async Task GetCardDetails_ThrowsArgumentNullException_WhenCardNumberIsNull()
    {
        var cardService = new CardService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => cardService.GetCardDetails("User1", null!));
    }
}