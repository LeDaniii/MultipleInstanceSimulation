namespace Simulator.Models
{
    public class Models
    {
    }

    public class Wpc
    {
        public int Id { get; set; }

        public double Position { get; set; }
    }


    public class Conveyor
    {
        public int id { get; set; }

        public double Length { get; set; } = 100;

        public List<Wpc> Wpcs { get; set; } = new List<Wpc>();
    }
}
