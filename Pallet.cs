namespace WarehouseApp
{
    public class Pallet : WarehouseItem
    {
        public Pallet()
        {
            Width = 1.5;
            Height = 0.2;
            Depth = 1.5;
        }

        private const double PalletWeight = 30;

        public double PalletVolume => (Width * Height * Depth);

        public List<Box> Boxes { get; set; } = new();
        
        public override double Weight => PalletWeight + Boxes.Sum(b => b.Weight);

        public override double Volume => PalletVolume + Boxes.Sum(b => b.Volume);

        public DateTime? ExpirationDate =>
            Boxes.Count > 0 ? Boxes.Min(b => b.EffectiveExpirationDate) : null;
    }
}
