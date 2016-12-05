
namespace PetriNets
{
    public sealed class PetriNetsEvaluator
    {
        public static PetriNetsEvaluator Instance { get; } = new PetriNetsEvaluator();

        static PetriNetsEvaluator() { }
        private PetriNetsEvaluator() {}

    }
}
