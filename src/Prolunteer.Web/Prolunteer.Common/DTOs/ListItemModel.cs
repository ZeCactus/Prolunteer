namespace Prolunteer.Common.DTOs
{
    public class ListItemModel<TValue, TText>
    {
        public TText Text { get; set; }
        public TValue Value { get; set; }
        public bool Selected { get; set; }
    }
}