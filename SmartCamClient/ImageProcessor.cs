using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCamClient
{
    public class ImageProcessor
    {
        public string TerminalId { get; set; }
        public string TerminalIp { get; set; }
        public string TerminalType { get; set; }
        public string CardEntered { get; set; }
        public string PinEntered { get; set; }
        public string CashPresented { get; set; }
        public string CashTaken { get; set; }
        public string CardEjected { get; set; }

    }
}
