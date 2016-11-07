namespace Workflow
{
    public abstract class SprintPrototype
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public abstract SprintPrototype Clone();
    }
}