namespace Exercise2
{
    internal class Tick
    {
        private double price;
        private int volumn;

        public Tick(double price, int volumn)
        {
            this.price = price;
            this.volumn = volumn;
        }

        public double Price { get { return price; } }

        public int Volumn { get { return volumn; } }
    }
}