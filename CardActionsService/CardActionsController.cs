using Microsoft.AspNetCore.Mvc;

namespace CardActionsService;
/// <summary>
/// Controller for handling card actions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CardActionsController(CardService cardService) : ControllerBase
{
    /// <summary>
    /// Gets the allowed actions for a specific card.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cardNumber">The card number.</param>
    /// <returns>A list of allowed actions for the card.</returns>
    public async Task<IActionResult> GetAllowedActions([FromQuery] string userId, [FromQuery] string cardNumber)
    {
        var cardDetails = await cardService.GetCardDetails(userId, cardNumber);
        if (cardDetails == null)
        {
            return NotFound(new { Message = "Card not found." });
        }
        var allowedActions = DetermineAllowedActions(cardDetails);
        return Ok(new { AllowedActions = allowedActions });
    }

    /// <summary>
    /// Gets all user cards.
    /// </summary>
    /// <returns>A dictionary of all user cards.</returns>
    public IActionResult GetAllUserCards()
    {
        var userCards = cardService.GetAllUserCards();
        return Ok(userCards);
    }
    
    private static List<string> DetermineAllowedActions(CardDetails cardDetails)
    {
        var actions = new List<string>();
        switch (cardDetails.CardType)
        {
            case CardType.Prepaid:
                if (cardDetails.CardStatus == CardStatus.Closed)
                {
                    actions.AddRange(["ACTION3", "ACTION4", "ACTION9"]);
                }
                break;
            case CardType.Credit:
                if (cardDetails.CardStatus == CardStatus.Blocked)
                {
                    actions.AddRange(["ACTION3", "ACTION4", "ACTION5", "ACTION8", "ACTION9"]);
                    if (cardDetails.IsPinSet)
                    {
                        actions.AddRange(["ACTION6", "ACTION7"]);
                    }
                }
                break;
        }
        return actions;
    }
}