using UnityEngine;

namespace _CardGame.Scripts.CardSystem.Cards
{
    [CreateAssetMenu(menuName = "Card Game/Action Card")]
    public class ActionCard : CardData
    {
        [SerializeField] private string _actionName = "Action";
        [SerializeField] private string _actionDescription = "Description";

        public override void RenderCard(CardController controller)
        {
            controller.SetAction(_actionName, _actionDescription);
        }
    }
}