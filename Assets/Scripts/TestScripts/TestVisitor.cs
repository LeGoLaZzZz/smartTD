namespace DefaultNamespace
{
    public interface IVisitor<TVisitorClient> where TVisitorClient : IVisitorClient
    {
        void Visit(TVisitorClient client);
    }

    public interface IVisitorClient
    {
        void Accept(IVisitor<IVisitorClient> visitor);
    }


    public class HealthPointsVisitor : IVisitor<IHasHealthPoints>
    {
        public void Visit(IHasHealthPoints client)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IHasHealthPoints : IVisitorClient
    {
    }
}