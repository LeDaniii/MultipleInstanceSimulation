using Microsoft.AspNetCore.SignalR;
using Simulator.Services;

namespace Simulator.Hubs
{
    public class SimulationHub : Hub
    {
        private readonly SimulationEngine _simulationEngine;

        public SimulationHub(SimulationEngine simulationEngine)
        {
            _simulationEngine = simulationEngine;
        }


        public override async Task OnConnectedAsync()
        {
            // Send the Initial state to the Client
            var conveyor = _simulationEngine.GetConveyorState();
            await Clients.Caller.SendAsync("UpdateConveyorState", conveyor);

            await base.OnConnectedAsync();
        }
    }
}
