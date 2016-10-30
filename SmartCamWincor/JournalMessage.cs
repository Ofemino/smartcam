namespace SmartCamHyosung
{
    public class JournalMessage
    {
        public bool NewTransaction { get; set; }
        public bool NoteBills { get; set; }
        public bool CashTaken { get; set; }
        public bool CashPresented { get; set; }
        public bool CardTaken { get; set; }
        public bool CardEntered { get; set; }
        public bool CardEjected { get; set; }
        public string JournalPart { get; set; }

    }
}