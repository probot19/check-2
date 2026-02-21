[System.Serializable]
public class SavedCardState
{
    public int cardIndex;
    public bool isMatched;
    
    public SavedCardState()
    {
        cardIndex = 0;
        isMatched = false;
    }
    
    public SavedCardState(int index, bool matched)
    {
        cardIndex = index;
        isMatched = matched;
    }
}

[System.Serializable]
public class CardStateWrapper
{
    public SavedCardState[] items;
    
    public CardStateWrapper()
    {
        items = new SavedCardState[0];
    }
    
    public CardStateWrapper(SavedCardState[] states)
    {
        items = states;
    }
}

[System.Serializable]
public class SaveData
{
    public int score;
    public int turns;
    public int gridRows;
    public int gridColumns;
    public CardStateWrapper cardStatesWrapper;
    public long timestamp;

    public SaveData()
    {
        score = 0;
        turns = 0;
        gridRows = 0;
        gridColumns = 0;
        cardStatesWrapper = new CardStateWrapper();
        timestamp = System.DateTime.Now.Ticks;
    }

    public SaveData(int score, int turns, int rows, int columns, SavedCardState[] states)
    {
        this.score = score;
        this.turns = turns;
        gridRows = rows;
        gridColumns = columns;
        cardStatesWrapper = new CardStateWrapper(states);
        timestamp = System.DateTime.Now.Ticks;
    }
    
    public SavedCardState[] GetCardStates()
    {
        return cardStatesWrapper != null ? cardStatesWrapper.items : new SavedCardState[0];
    }
}
