namespace WarehouseApp
{
    public abstract class WarehouseItem
    {
        public Guid Id { get; } = Guid.NewGuid();
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
    }
}
