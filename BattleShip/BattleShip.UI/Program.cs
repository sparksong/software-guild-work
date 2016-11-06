namespace BattleShip.UI
{
    internal class Program
    {
        //Include as little as possible, start method will call others.
        private static void Main(string[] args)
        {
            WorkFlow workFlowVariable = new WorkFlow();
            workFlowVariable.Start();
        }
    }
}
