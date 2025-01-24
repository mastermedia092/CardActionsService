namespace CardActionsService;

public enum CardType
{
    Prepaid,
    Debit,
    Credit
}

public enum CardStatus
{
    Ordered,
    Inactive,
    Active,
    Restricted,
    Blocked,
    Expired,
    Closed
}

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);

public class CardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();

    public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(cardNumber))
        {
            throw new ArgumentNullException(nameof(cardNumber), "Card number cannot be null or empty.");
        }
        
        await Task.Delay(1000);
        if (!_userCards.TryGetValue(userId, out var cards) ||
            !cards.TryGetValue(cardNumber, out var cardDetails))
        {
            return null;
        }
        return cardDetails;
    }

    public Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards()
    {
        return _userCards;
    }
    
    private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
    {
        var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
        for (var i = 1; i <= 3; i++)
        {
            var cards = new Dictionary<string, CardDetails>();
            var cardIndex = 1;
            foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                {
                    var cardNumber = $"Card{i}{cardIndex}";
                    cards.Add(cardNumber,
                        new CardDetails(
                            CardNumber: cardNumber,
                            CardType: cardType,
                            CardStatus: cardStatus,
                            IsPinSet: cardIndex % 2 == 0));
                    cardIndex++;
                }
            }
            var userId = $"User{i}";
            userCards.Add(userId, cards);
        }
        return userCards;
    }
}