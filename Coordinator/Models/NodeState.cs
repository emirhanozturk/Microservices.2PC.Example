using Coordinator.Enums;

namespace Coordinator.Models
{
    public record NodeState(Guid TransactionId)
    {
        public Guid Id { get; set; }
        /// <summary>
        /// result of prepare phase 
        /// </summary>
        public ReadyType IsReady { get; set; }

        /// <summary>
        /// result of commit phase
        /// </summary>
        public TransactionState TransactionState { get; set; }
        public Node Node { get; set; }
    }
}