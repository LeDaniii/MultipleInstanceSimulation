using Simulator.Models;

namespace Simulator.Services
{
    public class SimulationEngine
    {
        private readonly Conveyor _connveyor;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(100);
        private CancellationTokenSource _cts = new CancellationTokenSource();
        public event Action<Conveyor> OnSimulationUpdate;


        public SimulationEngine()
        {
            _connveyor = new Conveyor { id = 1 };
            _connveyor.Wpcs.Add(new Wpc { Id = 1, Position = 0 });
        }

        public void Start()
        {
            _cts = new CancellationTokenSource();
            Task.Run(async () => await RunSimulator(_cts.Token));
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        private async Task RunSimulator(CancellationToken token)
        {
            double speed = 1.0;

            while (!token.IsCancellationRequested)
            {
                foreach (var wpc in _connveyor.Wpcs)
                {
                    wpc.Position += speed;
                    if (wpc.Position >= _connveyor.Length)
                    {
                        wpc.Position = 0;
                    }
                }

                // Notify subscribers (e.g., SignalR hub)
                OnSimulationUpdate?.Invoke(_connveyor);

                await Task.Delay(_updateInterval);

            }
        }

        public Conveyor GetConveyorState()
        {
            return _connveyor;
        }

    }
}