namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class EventFilterRESPONSE : BaseFilterRESPONSE
    {
        public List<EventGetRESPONSE> Events { get; set; } = new List<EventGetRESPONSE>();
    }
}
