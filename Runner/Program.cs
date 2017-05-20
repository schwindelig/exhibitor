using ExhibitorLib;

namespace Runner
{
    class Something
    {
        private string privateString;

        private int privateInt { get; set; }

        private static string privateStaticString;

        static Something()
        {
            privateStaticString = "static string";
        }

        public Something()
        {
            this.privateString = "Private hello!";
            this.privateInt = 42;
        }

        public void PublicMethod()
        {
            var message = this.GetMessage();
            System.Console.WriteLine($"Message is: {message}");
            PrivateStaticMethod(3);
        }

        private string GetMessage()
        {
            return $"{this.privateString} {this.privateInt} ({privateStaticString})";
        }

        private static void PrivateStaticMethod(int a)
        {
            System.Console.WriteLine($"Private static method called with {a}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var something = new Something();
            something.PublicMethod();

            var proxy = Exhibitor.Load(something);

            proxy.SetFieldValue("privateString", "changed ...");
            proxy.SetPropertyValue("privateInt", 1234);
            proxy.SetFieldValue("privateStaticString", "zzzz");

            var message = proxy.InvokeMethod<string>("GetMessage");
            System.Console.WriteLine(message);

            proxy.InvokeMethod("PrivateStaticMethod", 111);
        }
    }
}