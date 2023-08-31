namespace ValetAcounting.Model
{
    public class Record
    {
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public int Workers { get; set; }
        public double Income { get; set; }
        public double Tip { get; set; }
        public double DailyExp { get; set; }
        public double DailyNet { get; set; }
    }
}
