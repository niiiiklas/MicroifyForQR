# MicroifyForQR

Make your beautiful longproperynamed data contracts into short string perfect for QR, AZTEK codes.

Turn 
public class TestData
    {
        [MinifyAs("tsd")]
        public string TheNameOfSubject { get; set; }

        public string SomethingElse { get; set; }

        [IgnoreProperty]
        public string IsWhat { get; set; }

        public int ThisIstheNumber { get; set; }

        public double Whatty { get; set; }

        public decimal D { get; set; }
    }
    
    var td = new TestData() { TheNameOfSubject = "Niklas Andersson", SomethingElse = "12", IsWhat="dsadsadsa", ThisIstheNumber=12, Whatty =22, D = 9292 };
    Into: "tsd:Niklas Andersson-Som:12-Thi:12-Wha:22-D:9292-"
