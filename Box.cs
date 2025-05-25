namespace WarehouseApp
{
    public class Box : WarehouseItem
    {
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ManufactureDate { get; set; }

        public double Volume => Width * Height * Depth;

        public DateTime EffectiveExpirationDate
        {
            get
            {
                if (ExpirationDate.HasValue)
                    return ExpirationDate.Value;
                if (ManufactureDate.HasValue)
                    return ManufactureDate.Value.AddDays(100);
                throw new InvalidOperationException("У коробки должна быть указана либо дата срока годности, либо дата производства");
            }
        }
    }
}
