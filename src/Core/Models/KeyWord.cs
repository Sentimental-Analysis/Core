namespace Core.Models
{
    public class KeyWord
    {
        public KeyWord(string key, int quantity)
        {
            Key = key;
            Quantity = quantity;
        }

        public string Key { get; }
        public int Quantity { get; }
    }
}