using Assets.Scripts.UI;
using Assets.Scripts.UI.Inventory;

namespace Assets.Scripts.Signals
{
    public enum EndGameType
    {
        Won,
        Lost
    }

    public class EndGameSignal
    {
        public EndGameType EndGameType;

        public EndGameSignal(EndGameType endGameType)
        {
            EndGameType = endGameType;
        }
    }
}
